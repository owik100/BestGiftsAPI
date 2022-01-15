using AutoMapper;
using BestGiftsAPI.DAL;
using BestGiftsAPI.Entities;
using BestGiftsAPI.Models_DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestGiftsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CommentController(ApplicationDbContext context, ILogger<GiftIdeasController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/GiftIdeas/Get/1222
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> Get(int id)
        {
            var output = new List<CommentDTO>();
            var entities = new List<Comment>();
            try
            {
                entities = await _context.Comments
                    .Where(x =>x.GiftIdeaId == id)
                    .OrderByDescending(x => x.CreationTime)
                    .ToListAsync();

                foreach (var item in entities)
                {
                    output.Add(_mapper.Map<CommentDTO>(item));
                }


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

        // POST: api/Comment/Create
        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Create(CommentDTO commentDTO)
        {
            try
            {
                commentDTO.CreationTime = DateTime.Now;
                Comment comment = _mapper.Map<Comment>(commentDTO);
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Create]");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

            return Ok();
        }
    }
}
