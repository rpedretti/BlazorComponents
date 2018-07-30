namespace RPedretti.Blazor.Shared.Domain
{
    /// <summary>
    /// Represents a User at the Database
    /// </summary>
    public class User
    {
        #region Properties

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public virtual string Password { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public virtual string Username { get; set; }

        #endregion Properties
    }
}
