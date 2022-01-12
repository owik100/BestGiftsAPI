using AutoMapper;
using BestGiftsAPI.DAL;
using BestGiftsAPI.Entities;
using BestGiftsAPI.Models_DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
