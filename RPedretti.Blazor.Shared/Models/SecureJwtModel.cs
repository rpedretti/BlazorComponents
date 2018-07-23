using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RPedretti.Blazor.Shared.Models
{
    /// <summary>
    /// Represents a JWT
    /// </summary>
    public sealed class SecureJwtModel
    {
        /// <summary>
        /// Gets or sets the origin identifier.
        /// </summary>
        /// <value>
        /// The origin identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        [Required]
        [Display(Name = "id")]
        public int OriginId { get; set; }

        /// <summary>
        /// Gets or sets the token model.
        /// </summary>
        /// <value>
        /// The token model.
        /// </value>
        [JsonProperty(PropertyName = "token")]
        [Required]
        [Display(Name = "token")]
        public TokenModel TokenModel { get; set; }
    }
}
