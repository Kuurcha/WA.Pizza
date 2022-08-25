using Mapster;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Core.Model.Advertisement;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

namespace Wa.Pizza.Infrasctructure.Data.Services
{
    public class AdvertisementClientService
    {
        private readonly ApiKeyService _apiKeyService;
        private readonly ApplicationDbContext _context;
        private readonly int keyLength = 32;

        public AdvertisementClientService(ApiKeyService apiKeService, ApplicationDbContext context)
        {
            _apiKeyService = apiKeService;
            _context = context;
        }

        public async Task<string> CreateAdvertisementClientAsync(AdvertisementClientDTO advertisementClientDTO)
        {
            if (advertisementClientDTO == null)
                throw new WrongDataFormatException("Specify data for advertisement client");
            AdvertisementClient advertisementClient = advertisementClientDTO.Adapt<AdvertisementClient>();
            advertisementClient.ApiKey = _apiKeyService.GenerateApiKey(keyLength);
            _context.AdvertisementClients.Add(advertisementClient);
            await _context.SaveChangesAsync();
            return advertisementClient.ApiKey;
        }
        public async Task<AdvertisementClient> getOriginalClient(AdvertisementClientDTO advertisementClientDTO)
        {
            if (advertisementClientDTO == null)
                throw new WrongDataFormatException("Specify data for advertisement client");
            AdvertisementClient originalAdvertisementClient = await _context.AdvertisementClients.Include(ac => ac.Advertisements)
                                                           .FirstOrDefaultAsync(x => x.ApiKey == advertisementClientDTO.ApiKey);
            if (originalAdvertisementClient == null)
                throw new EntityNotFoundException("Invalid ApiKey");
            return originalAdvertisementClient;
        }

        public async Task<int> UpdateAdvertisementClient(AdvertisementClientDTO advertisementClientDTO)
        {
            var originalAdvertisementClient =  await getOriginalClient(advertisementClientDTO);

            originalAdvertisementClient.Adapt(advertisementClientDTO);

            return await _context.SaveChangesAsync(); 
        }
        

        public async Task<int> DeleteAdvertisementClient(AdvertisementClientDTO advertisementClientDTO)
        {
            var originalAdvertisementClient = await getOriginalClient(advertisementClientDTO);
            _context.AdvertisementClients.Remove(originalAdvertisementClient);
            return await _context.SaveChangesAsync();
        }
        
        public async Task<AdvertisementClientDTO?> GetAdvertisementClientAsync(string desiredApiKey)
        {
           var advertisementClientDTO = await _context.AdvertisementClients
                   .AsNoTracking()
                   .Include(ac => ac.Advertisements)
                   .ProjectToType<AdvertisementClientDTO>()
                   .FirstOrDefaultAsync(ac => ac.ApiKey == desiredApiKey);
          if (advertisementClientDTO == null)
                throw new EntityNotFoundException("Invalid ApiKey");
          return advertisementClientDTO;
        } 

        public async Task<string> RegenerateApiKey(string apiKeyToRegenerate)
        {
            var originalAdvertisementClient = await getOriginalClient(new AdvertisementClientDTO {  ApiKey = apiKeyToRegenerate });
            originalAdvertisementClient.ApiKey = _apiKeyService.GenerateApiKey(keyLength);
            await _context.SaveChangesAsync();
            return originalAdvertisementClient.ApiKey;
        }

    }
}
