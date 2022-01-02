using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Entities
{
    public record Category
    {
        public int CategoryId { get; init; }
        public string Name { get; init; }

        public ICollection<GiftIdeaCategory> GiftIdeaCategory { get; init; }
    }
}
