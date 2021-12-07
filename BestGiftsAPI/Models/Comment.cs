using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Models
{
    public record Comment
    {
        public int CommentId { get; init; }
        public string CommentContent { get; init; }

        public int GiftIdeaId { get; init; }
        public GiftIdea GiftIdea { get; init; }
    }
}
