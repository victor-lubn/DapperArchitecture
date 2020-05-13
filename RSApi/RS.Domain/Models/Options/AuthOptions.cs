using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RS.Domain.Models.Options
{
    /// <summary>
    /// The AuthOptions
    /// </summary>
    public static class AuthOptions
    {
        /// <summary>
        /// The issuer
        /// </summary>
        public const string Issuer = "TestAuthServer";
        /// <summary>
        /// The audience
        /// </summary>
        public const string Audience = "http://localhost:5000/";
        /// <summary>
        /// The key
        /// </summary>
        private const string Key = "Test_secret4key!=-";
        /// <summary>
        /// The lifetime
        /// </summary>
        public const int Lifetime = 100000;
        /// <summary>
        /// Gets the symmetric security key.
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
