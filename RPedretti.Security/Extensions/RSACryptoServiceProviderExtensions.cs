using System;
using System.Security.Cryptography;
using System.Xml;

namespace RPedretti.Security.Extensions
{
    /// <summary>
    /// Extensions for <see cref="RSA"/>
    /// </summary>
    public static class RSACryptoServiceProviderExtensions
    {
        /// <summary>
        /// Converts a <see cref="RSA"/> key from XML string.
        /// </summary>
        /// <param name="rsa">The RSA instance.</param>
        /// <param name="xmlString">The XML string.</param>
        /// <exception cref="Exception">Invalid XML RSA key.</exception>
        public static void CustomFromXmlString(this RSA rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus": parameters.Modulus = Convert.FromBase64String(node.InnerText); break;
                        case "Exponent": parameters.Exponent = Convert.FromBase64String(node.InnerText); break;
                        case "P": parameters.P = Convert.FromBase64String(node.InnerText); break;
                        case "Q": parameters.Q = Convert.FromBase64String(node.InnerText); break;
                        case "DP": parameters.DP = Convert.FromBase64String(node.InnerText); break;
                        case "DQ": parameters.DQ = Convert.FromBase64String(node.InnerText); break;
                        case "InverseQ": parameters.InverseQ = Convert.FromBase64String(node.InnerText); break;
                        case "D": parameters.D = Convert.FromBase64String(node.InnerText); break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        /// <summary>
        /// Converts a <see cref="RSA"/> key to a XML string.
        /// </summary>
        /// <param name="rsa">The RSA instance.</param>
        /// <param name="includePrivateParameters">if set to <c>true</c> also export the private part.</param>
        /// <returns></returns>
        public static string CustomToXmlString(this RSA rsa, bool includePrivateParameters = false)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);
            Console.WriteLine($"rsa params exported");

            if (includePrivateParameters)
            {
                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                    Convert.ToBase64String(parameters.Modulus),
                    Convert.ToBase64String(parameters.Exponent),
                    Convert.ToBase64String(parameters.P),
                    Convert.ToBase64String(parameters.Q),
                    Convert.ToBase64String(parameters.DP),
                    Convert.ToBase64String(parameters.DQ),
                    Convert.ToBase64String(parameters.InverseQ),
                    Convert.ToBase64String(parameters.D));
            }

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                Convert.ToBase64String(parameters.Modulus),
                Convert.ToBase64String(parameters.Exponent));
        }
    }
}
