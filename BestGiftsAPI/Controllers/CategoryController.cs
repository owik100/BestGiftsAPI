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
using System.Threading.Tasks;

namespace BestGiftsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CategoryController(ApplicationDbContext context, ILogger<GiftIdeasController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }


     // GET: api/Category/GetAll
    [Route("GetAll")]
    [HttpGet]
    public async Task<ActionResult<List<CategoryDTO>>> GetAll()
    {
        var output = new List<CategoryDTO>();
        var entities = new List<Category>();
        try
        {

            entities = await _context.Categories
                .ToListAsync();

            foreach (var item in entities)
            {
                output.Add(_mapper.Map<CategoryDTO>(item));
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
}
}
