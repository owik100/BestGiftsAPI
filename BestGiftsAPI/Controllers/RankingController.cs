using AutoMapper;
using BestGiftsAPI.DAL;
using BestGiftsAPI.Entities;
using BestGiftsAPI.Models_DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestGiftsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public RankingController(ApplicationDbContext context, ILogger<RankingController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Zwiększa lub zmniejsza ranking pomysłu na prezent o 1 głos
        /// </summary>
        /// <param name="id">id pomysłu na prezent</param>
        /// <param name="increase">Określa czy zwiększyć czy zmniejszyć punkty w rankingu</param>
        /// <returns></returns>
        // Path: api/Ranking/ChangeGiftIdeaRanking/1224/true
        [Consumes("application/json")]
        [Route("ChangeGiftIdeaRanking/{id}/{increase}")]
        [HttpPatch]
        public async Task<ActionResult> ChangeGiftIdeaRanking(int id, bool increase)
        {
            GiftIdea entitie;
            GiftIdeaDTO giftIdeaDTO;
            try
            {
                entitie = await _context.GiftIdeas
                    .AsNoTracking()
                    .Where(x => x.GiftIdeaId == id)
                    .FirstOrDefaultAsync();

                if(entitie != null)
                {
                    giftIdeaDTO = _mapper.Map<GiftIdeaDTO>(entitie);
                    if (increase)
                    {
                        giftIdeaDTO.LikesCounter += 1;
                    }
                    else
                    {
                        giftIdeaDTO.LikesCounter -= 1;
                    }

                    entitie = _mapper.Map<GiftIdea>(giftIdeaDTO);


                    _context.Entry<GiftIdea>(entitie).State = EntityState.Modified;
                   // _context.Entry<GiftIdea>(entitie).Property(p => p.LikesCounter).IsModified = true;
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ChangeGiftIdeaRanking]");
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                throw new System.Web.Http.HttpResponseException(resp);

            }

            return Ok();
        }
    }
}
