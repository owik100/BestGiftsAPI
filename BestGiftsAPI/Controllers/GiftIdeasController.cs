using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BestGiftsAPI.DAL;
using BestGiftsAPI.Models_DTO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using AutoMapper;
using BestGiftsAPI.Entities;

namespace BestGiftsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftIdeasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public GiftIdeasController(ApplicationDbContext context, ILogger<GiftIdeasController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/GiftIdeas/GetAll
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<GiftIdeaDTO>>> GetAll()
        {
            var output = new List<GiftIdeaDTO>();
            var entities = new List<GiftIdea>();
            try
            {
                entities = await _context.GiftIdeas
                    .OrderByDescending(x => x.GiftIdeaId)
                    .ToListAsync();

                foreach (var item in entities)
                {
                    output.Add(_mapper.Map<GiftIdeaDTO>(item));
                }

                
                if (output == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetAll]");
            }

            return output;
        }

        // GET: api/GiftIdeas/Get/1244
        [Consumes("application/json")]
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<GiftIdeaDTO>> Get(int id)
        {
            var output = new GiftIdeaDTO();
            var entitie = new GiftIdea();
            try
            {
                const int pageSizeComments = 10;

                entitie = await _context.GiftIdeas
                    .Include(comments => comments.Comments.OrderByDescending(x => x.CreationTime).Take(pageSizeComments))
                    .Include(cat => cat.GiftIdeaCategory)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.GiftIdeaId == id);

                output = _mapper.Map<GiftIdeaDTO>(entitie);

                if (output == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Get]");
            }

            return output;
        }

        // POST: api/GiftIdeas/PostGift
        [Route("PostGift")]
        [HttpPost]
        public async Task<ActionResult> PostGift(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    byte[] imageArr;
                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        imageArr = target.ToArray();
                    }

                    var giftIdeaToUpload = new GiftIdea()
                    {
                        ImageContent = imageArr,
                    };



                    _context.GiftIdeas.Add(giftIdeaToUpload);
                  await _context.SaveChangesAsync();

                }
            }
            return Ok();
        }
    }
}
