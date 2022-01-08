using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Models_DTO
{
    public class GiftIdeaDTO
    {
        public int GiftIdeaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int LikesCounter { get; set; }
        public string ImageContentB64 { get; set; }
        public string ExternalUrl { get; set; }
        public DateTime CreationTime { get; set; }

        public ICollection<CommentDTO> CommentsDTO { get; set; }
        public ICollection<GiftIdeaCategoryDTO> GiftIdeaCategoryDTO { get; set; }
    }
}
