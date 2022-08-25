namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class AdvertisementService
    {
        private ApiKeyService _apiKeService;
        private readonly ApplicationDbContext _context;
        public AdvertisementService(ApiKeyService apiKeService, ApplicationDbContext context)
        {
            _apiKeService = apiKeService;
            _context = context;
        }

        



    }
}
