using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Entities
{
    public record GiftIdeaCategory
    {
        public int GiftIdeaId { get; init; }
        public GiftIdea GiftIdea { get; init; }

        public int CategoryId { get; init; }
        public Category Category { get; init; }
    }
}
