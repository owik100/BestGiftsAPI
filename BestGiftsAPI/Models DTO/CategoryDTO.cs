using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Models_DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<GiftIdeaCategoryDTO> GiftIdeaCategoryDTO { get; set; }
    }
}
