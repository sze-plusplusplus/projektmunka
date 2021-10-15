namespace MeetHut.Services.Configurations
{
    /// <summary>
    /// JWT Configuration
    /// </summary>
    public class JWTConfiguration
    {
        /// <summary>
        /// JWT key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// JWT issuer
        /// </summary>
        public string Issuer { get; set; }
    }
}
