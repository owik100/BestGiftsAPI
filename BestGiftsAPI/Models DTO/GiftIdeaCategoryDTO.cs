using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Models_DTO
{
    public class GiftIdeaCategoryDTO
    {
        public int GiftIdeaId { get; set; }
        public GiftIdeaDTO GiftIdeaDTO { get; set; }

        public int CategoryId { get; set; }
        public CategoryDTO CategoryDTO { get; set; }
    }
}
