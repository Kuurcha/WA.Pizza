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

        /// <summary>
        /// A method that checks if advertisement is null or if api key is invalid and throws expections
        /// </summary>
        /// <param name="advertisement"></param>
        /// <param name="apiKey"></param>
        /// <param name="id">id for the message</param>
        /// <param name="message">first part of the message corresponding to operation</param>
        /// <exception cref="EntityNotFoundException"></exception>
        /// <exception cref="WrongDataFormatException"></exception>
        private void CheckForApiAndNullability(Advertisement? advertisement, string apiKey, int id, string message)
        {
            if (advertisement == null)
                throw new EntityNotFoundException(message + id + " no such id found");
            if (advertisement.AdvertisementClient.ApiKey != apiKey)
                throw new WrongDataFormatException(message + id + " api key is invalid");
        }
        public async Task CreateAdvertisement(CUDAdvertisementDTO advertisementDTO, string apiKey)
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
            await _context.SaveChangesAsync();

        }

        public async Task RemoveAdvertisement(int id, string apiKey)
        {
            var originalAdvertisement = _context.Advertisements.Include(a => a.AdvertisementClient).FirstOrDefault(a => a.Id == id);
            CheckForApiAndNullability(originalAdvertisement, apiKey, id, "Can't remove advertisement with id: ");
            _context.Advertisements.Remove(originalAdvertisement);
            await _context.SaveChangesAsync();

        }
       
        public async Task UpdateAdvertisement(CUDAdvertisementDTO advertisementDTO, int id, string apiKey)
        {
            if (advertisementDTO == null)
                throw new WrongDataFormatException("Advertisement should not be null");

            var originalAdvertisement = _context.Advertisements.Include(a=> a.AdvertisementClient).FirstOrDefault(a => a.Id == id);
            CheckForApiAndNullability(originalAdvertisement, apiKey, id, "Can't update advertisement with id: ");
            originalAdvertisement.Description = advertisementDTO.Description;
            originalAdvertisement.ImageURL = advertisementDTO.ImageURL;
            originalAdvertisement.RedicrectURL = advertisementDTO.RedicrectURL;
            _context.Advertisements.Update(originalAdvertisement);
         await _context.SaveChangesAsync();
        }

        public async Task<CUDAdvertisementDTO> GetAdvertisementAnonymous(int id)
        {
            var originalAdvertisement = _context.Advertisements.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (originalAdvertisement == null)
                throw new EntityNotFoundException("Can't retrieve advertisement with id: " + id + " no such id found");
            return originalAdvertisement!.Adapt<CUDAdvertisementDTO>();
        }
        public async Task<AdvertisementDTO> GetAdvertisement(string apiKey, int id) 
        {
            var originalAdvertisement = _context.Advertisements.Include(a => a.AdvertisementClient).FirstOrDefault(a => a.Id == id);
            CheckForApiAndNullability(originalAdvertisement, apiKey,id, "Can't retrieve advertisement with id: ");
            return originalAdvertisement!.Adapt<AdvertisementDTO>();
        }
        



    }
}
