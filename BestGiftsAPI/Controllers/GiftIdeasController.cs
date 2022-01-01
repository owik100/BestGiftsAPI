using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BestGiftsAPI.DAL;
using BestGiftsAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BestGiftsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftIdeasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GiftIdeasController(ApplicationDbContext context, ILogger<GiftIdeasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/GiftIdeas/Online
        [Route("Online")]
        [HttpGet]
        public ActionResult Online()
        {
            return Ok();
        }

        // GET: api/GiftIdeas/GetAll
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<GiftIdea>>> GetAll()
        {
            var output = new List<GiftIdea>();
            try
            {
                output = await _context.GiftIdeas
                    .Include(cat => cat.GiftIdeaCategory)
                    .ThenInclude(x => x.Category)
                    .ToListAsync();
                if (output == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[All]");
            }

            return output;
        }

        // GET: api/GiftIdeas/Get/1244
        [Consumes("application/json")]
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<GiftIdea>> Get(int id)
        {
            var output = new GiftIdea();
            try
            {
                output = await _context.GiftIdeas
                    .Include(comments => comments.Comments)
                    .Include(cat => cat.GiftIdeaCategory)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.GiftIdeaId == id);


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
