using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa.Pizza.Core.Model.Advertisement
{
    public class AdvertisementClient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public string ApiKey { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }


    }
}
