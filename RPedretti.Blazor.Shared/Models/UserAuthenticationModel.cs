using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RPedretti.Blazor.Shared.Models
{
    /// <summary>
    /// Represents a User authentication
    /// </summary>
    public sealed class UserAuthenticationModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [JsonProperty(PropertyName = "password")]
        [Required]
        [Display(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [JsonProperty(PropertyName = "username")]
        [Required]
        [Display(Name = "username")]
        public string Username { get; set; }

        #endregion Properties
    }
}
