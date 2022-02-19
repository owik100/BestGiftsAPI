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
using BestGiftsAPI.Helpers;

namespace BestGiftsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftIdeasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IPaginationHelper _paginationHelper;

        public GiftIdeasController(ApplicationDbContext context, ILogger<GiftIdeasController> logger, IMapper mapper, IPaginationHelper paginationHelper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _paginationHelper = paginationHelper;
        }

        // GET: api/GiftIdeas/GetAll
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<PagedListDTO<GiftIdeaDTO>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var output = new PagedListDTO<GiftIdeaDTO>();
            var entities = new List<GiftIdea>();
            try
            {
                if (pageNumber <= 0)
                    pageNumber = 1;

                int skip = (pageNumber - 1) * pageSize;
                int take = pageSize;
                int count = _context.GiftIdeas
                    .Count();

                int totalPages = _paginationHelper.CalculateTotalPages(count, pageSize);

                entities = await _context.GiftIdeas
                    .OrderByDescending(x => x.GiftIdeaId)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                foreach (var item in entities)
                {
                    output.Items.Add(_mapper.Map<GiftIdeaDTO>(item));
                }

                if (output == null)
                {
                    return NotFound();
                }


                output.CurrentPage = pageNumber;
                output.TotalItems = count;
                output.PageSize = pageSize;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetAll]");
            }

            return output;
        }

        // GET: api/GiftIdeas/Get/1244?pageNumber=2
        [Consumes("application/json")]
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<GiftIdeaDTO>> Get(int id, [FromQuery] int pageNumber = 1)
        {
            var output = new GiftIdeaDTO();
            var entitie = new GiftIdea();
            try
            {
                const int pageSizeComments = 5;
                int totalComments = _context.Comments.Where(x => x.GiftIdeaId == id).Count();

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

                output.CategoriesDTO = new List<CategoryDTO>();
                _mapper.Map(entitie.GiftIdeaCategory, output.CategoriesDTO);

                output.CommentsDTO.PageSize = pageSizeComments;
                output.CommentsDTO.CurrentPage = 1;
                output.CommentsDTO.TotalItems = totalComments;
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
        public async Task<ActionResult> PostGift(GiftIdeaDTO giftIdeaDTO)
        {
            GiftIdea giftIdea = new GiftIdea();
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                giftIdeaDTO.CreationTime = DateTime.Now;

                var giftIdeaEntity = _mapper.Map<GiftIdea>(giftIdeaDTO);

                var contextAddRes = await _context.GiftIdeas.AddAsync(giftIdeaEntity);
                giftIdea = contextAddRes.Entity;

                await _context.SaveChangesAsync();

                List<Category> catToAdd = new List<Category>();

                List<CategoryDTO> categoriesNoDuplicates = giftIdeaDTO.CategoriesDTO.GroupBy(x => x.Name).Select(x => x.First()).ToList();
                giftIdeaDTO.CategoriesDTO = categoriesNoDuplicates;

                foreach (CategoryDTO item in giftIdeaDTO.CategoriesDTO)
                {
                    if (item.CategoryId <= -1)
                    {
                        Category newCategory = new Category()
                        {
                            Name = item.Name
                        };
                        await _context.Categories.AddAsync(newCategory);
                        await _context.SaveChangesAsync();
                        catToAdd.Add(newCategory);
                    }
                    else
                    {
                        catToAdd.Add(_mapper.Map<Category>(item));
                    }
                }

                foreach (var item in catToAdd)
                {
                    GiftIdeaCategory giftIdeaCategoryToAdd = new GiftIdeaCategory()
                    {
                        GiftIdeaId = giftIdea.GiftIdeaId,
                        CategoryId = item.CategoryId
                    };
                    await _context.GiftIdeaCategories.AddAsync(giftIdeaCategoryToAdd);
                    await _context.SaveChangesAsync();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PostGift]");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
          
            return CreatedAtAction(nameof(Get), new { id = giftIdea.GiftIdeaId }, giftIdea);
        }
    }
}
