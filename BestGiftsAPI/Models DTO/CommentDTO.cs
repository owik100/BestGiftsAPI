using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Models_DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public string CommentAuthor { get; set; }

        public int GiftIdeaId { get; set; }
        public GiftIdeaDTO GiftIdeaDTO { get; set; }
    }
}
