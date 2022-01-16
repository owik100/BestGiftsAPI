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

        // GET: api/GiftIdeas/Get/1222?pageNumber=2
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<CommentDTO>>> Get(int id, [FromQuery] int pageNumber = 1)
        {
            var output = new List<CommentDTO>();
            var entities = new List<Comment>();
            try
            {
                const int pageSize = 10;
                if (pageNumber <= 0)
                    pageNumber = 1;

                int skip = (pageNumber - 1) * pageSize;
                int take = pageSize;
                int count = _context.Comments
                    .Where(x => x.GiftIdeaId == id)
                    .Count();

                int totalPages = CalculateTotalPages(count, pageSize);

                entities = await _context.Comments
                    .Where(x =>x.GiftIdeaId == id)
                    .OrderByDescending(x => x.CreationTime)
                    .Skip(skip)
                    .Take(take)
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

        private int CalculateTotalPages(int count, int pageSze)
        {
            return (int)Math.Ceiling(decimal.Divide(count, pageSze));
        }
    }
}
