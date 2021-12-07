using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Models
{
    public record GiftIdea
    {
        public int GiftIdeaId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Author { get; init; }
        public int LikesCounter { get; init; }

        public ICollection<Comment> Comments { get; init; }
        public ICollection<GiftIdeaCategory> GiftIdeaCategory { get; init; }
    }
}
