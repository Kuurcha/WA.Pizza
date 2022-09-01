    using System.Security.Cryptography;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class ApiKeyService
    {
        private readonly ApplicationDbContext _context;

        private const string _prefix = "AD-";

        public ApiKeyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateApiKey(int length)
        {
            var bytes = RandomNumberGenerator.GetBytes(length);
            return _prefix + Convert.ToBase64String(bytes)
                .Replace("/", "")
                .Replace("+", "")
                .Replace("=", "")
                .Substring(0, length+1);
        }

        public bool ValidateApiKey(int length)
        {
            return true;
        }

    }
}
