using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Model.Advertisement
{
    public class Advertisement
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string RedicrectURL { get; set; }

        public string ImageURL { get; set; }

        public int AdvertisementClientId { get; set; }

        public AdvertisementClient AdvertisementClient { get; set; }

    }
}
