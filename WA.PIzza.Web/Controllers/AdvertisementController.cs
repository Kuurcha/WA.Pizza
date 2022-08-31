using Microsoft.AspNetCore.Mvc;
using Wa.Pizza.Infrasctructure.Data.Services;

namespace WA.PIzza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController
    {
        private readonly ILogger<BasketController> _log;
        private readonly AdvertisementService _advertisementService;

        public AdvertisementController(AdvertisementService advertisementService, ILogger<BasketController> log)
        {
            _advertisementService = advertisementService;
            _log = log;
        }
    }
}