using Mapster;
using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Core.Exceptions;
using Wa.Pizza.Core.Model.Advertisement;
using Wa.Pizza.Infrasctructure.DTO.AdvertisementDTO;

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


        public async Task<int> CreateAdvertisement(AdvertisementDTO advertisementDTO, string apiKey)
        {

            if (advertisementDTO == null)
                throw new WrongDataFormatException("Advertisement should not be null");
            var updatedTestAdvertisementClient = await _context.AdvertisementClients
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync(ac => ac.ApiKey == apiKey);
            if (updatedTestAdvertisementClient == null)
                throw new EntityNotFoundException("No client found with specified api key");
            var advertisement = advertisementDTO.Adapt<Advertisement>();
            advertisement.AdvertisementClientId = updatedTestAdvertisementClient.Id;
            _context.Advertisements.Add(advertisement);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAdvertisement(AdvertisementDTO advertisementDTO, string apiKey)
        {
            return 0;
        }
       
        public async Task<int> UpdateAdvertisement(AdvertisementDTO advertisementDTO, string apiKey)
        {
            var originalAdvertisement = _context.Advertisements.Include(a=> a.AdvertisementClient).FirstOrDefault(a => a.Id == advertisementDTO.Id);
            if (originalAdvertisement == null)
                throw new EntityNotFoundException("Can't update advertisement with id: " + advertisementDTO.Id + " id does not exists");
            if (originalAdvertisement.AdvertisementClient.ApiKey != apiKey)
                throw new WrongDataFormatException("Can't update advertisement with id: " + advertisementDTO.Id + " api key is invalid");
            var advertisementToUpdate = advertisementDTO.Adapt<Advertisement>();
            _context.Advertisements.Update(advertisementToUpdate);
            return await _context.SaveChangesAsync();
        }

        public async Task<AdvertisementDTO> GetAdvertisement(string ApiKey, int id)
        {
            return null;
        }
        



    }
}
