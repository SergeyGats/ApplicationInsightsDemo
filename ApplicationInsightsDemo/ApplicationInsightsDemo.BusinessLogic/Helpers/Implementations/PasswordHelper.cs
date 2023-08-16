using System.Security.Cryptography;
using System.Text;
using ApplicationInsightsDemo.BusinessLogic.Helpers.Interfaces;

namespace ApplicationInsightsDemo.BusinessLogic.Helpers.Implementations
{
    public class PasswordHelper : IPasswordHelper
    {
        public byte[] GeneratePasswordHash(string password, string salt)
        {
            var plainTextBytes = Encoding.Unicode.GetBytes(password);
            var saltBytes = Encoding.Unicode.GetBytes(salt);

            var combinedBytes = new byte[plainTextBytes.Length + salt.Length];
            Buffer.BlockCopy(plainTextBytes, 0, combinedBytes, 0, plainTextBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, plainTextBytes.Length, salt.Length);

            var hashAlgorithm = SHA256.Create();
            var passwordHash = hashAlgorithm.ComputeHash(combinedBytes);

            return passwordHash;
        }

        public bool CheckIsValidPassword(string providedPassword, byte[] passwordHash, string salt)
        {
            var providedPasswordHash = GeneratePasswordHash(providedPassword, salt);
            var isEquals = providedPasswordHash.SequenceEqual(passwordHash);

            return isEquals;
        }
    }
}