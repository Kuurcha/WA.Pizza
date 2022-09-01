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


        private void CheckIfDTOIsNull(CreateAdvertisementClientDTO advertisementClientDTO)
        {
            if (advertisementClientDTO == null)
                throw new WrongDataFormatException("Specify data for advertisement client");
        }
        public async Task<string> CreateAdvertisementClientAsync(CreateAdvertisementClientDTO advertisementClientDTO)
        {
            CheckIfDTOIsNull(advertisementClientDTO);
            AdvertisementClient advertisementClient = advertisementClientDTO.Adapt<AdvertisementClient>();
            advertisementClient.ApiKey = _apiKeyService.GenerateApiKey(keyLength);
            _context.AdvertisementClients.Add(advertisementClient);
            await _context.SaveChangesAsync();
            return advertisementClient.ApiKey;
        }


        public async Task<AdvertisementClient> getOriginalClient(AdvertisementClientDTO advertisementClientDTO)
        {
           AdvertisementClient originalAdvertisementClient = await _context.AdvertisementClients.Include(ac => ac.Advertisements)
                                                           .FirstOrDefaultAsync(x => x.Id == advertisementClientDTO.Id);
            if (originalAdvertisementClient == null)
                throw new EntityNotFoundException("Invalid object id");
            if (originalAdvertisementClient.ApiKey != advertisementClientDTO.ApiKey)
                throw new EntityNotFoundException("Invalid api key");
            return originalAdvertisementClient;
        }

        public async Task UpdateAdvertisementClient(int id, UpdateDeleteAdvertisementClientDTO updateDeletAdvertisementClientDTO)
        {
            CheckIfDTOIsNull(updateDeletAdvertisementClientDTO);
            AdvertisementClientDTO advertisementClientDTO = updateDeletAdvertisementClientDTO.Adapt<AdvertisementClientDTO>();
            advertisementClientDTO.Id = id;
            var originalAdvertisementClient =  await getOriginalClient(advertisementClientDTO);

            originalAdvertisementClient.Website = updateDeletAdvertisementClientDTO.Website;
            originalAdvertisementClient.Name = updateDeletAdvertisementClientDTO.Name;
            await _context.SaveChangesAsync();

        }
        

        public async Task DeleteAdvertisementClient(int id, UpdateDeleteAdvertisementClientDTO updateDeletAdvertisementClientDTO)
        {
            CheckIfDTOIsNull(updateDeletAdvertisementClientDTO);
            AdvertisementClientDTO advertisementClientDTO = updateDeletAdvertisementClientDTO.Adapt<AdvertisementClientDTO>();
            advertisementClientDTO.Id = id;
            var originalAdvertisementClient = await getOriginalClient(advertisementClientDTO);
            _context.AdvertisementClients.Remove(originalAdvertisementClient);
         await _context.SaveChangesAsync();
        }
        
        public async Task<AdvertisementClientDTO> GetAdvertisementClient(string desiredApiKey)
        {
           var advertisementClientDTO = await _context.AdvertisementClients
                   .AsNoTracking()
                   .Include(ac => ac.Advertisements)
                   .ProjectToType<AdvertisementClientDTO>()
                   .FirstOrDefaultAsync(ac => ac.ApiKey == desiredApiKey);
          if (advertisementClientDTO == null)
                throw new EntityNotFoundException("Invalid Api key");
          return advertisementClientDTO;
        }

        public async Task<AdvertisementClientDTO> GetAdvertisementClientById(int id, string desiredApiKey)
        {
            var advertisementClientDTO = await _context.AdvertisementClients
                    .AsNoTracking()
                    .Include(ac => ac.Advertisements)
                    .ProjectToType<AdvertisementClientDTO>()
                    .FirstOrDefaultAsync(ac => ac.Id == id);
            if (advertisementClientDTO == null)
                throw new EntityNotFoundException("Invalid id");
            if (advertisementClientDTO.ApiKey != desiredApiKey)
                throw new WrongDataFormatException("Invalid api key");
            return advertisementClientDTO;
        }

        public async Task<string> RegenerateApiKey(int id, string apiKeyToRegenerate)
        {
            AdvertisementClient originalAdvertisementClient = await _context.AdvertisementClients.Include(ac => ac.Advertisements)
                                                           .FirstOrDefaultAsync(x => x.Id == id);
            if (originalAdvertisementClient == null)
                throw new EntityNotFoundException("Invalid object id");
            if (originalAdvertisementClient.ApiKey != apiKeyToRegenerate)
                throw new WrongDataFormatException("Invalid api key");
            originalAdvertisementClient.ApiKey = _apiKeyService.GenerateApiKey(keyLength);

            await _context.SaveChangesAsync();
            return originalAdvertisementClient.ApiKey;
        }

    }
}
