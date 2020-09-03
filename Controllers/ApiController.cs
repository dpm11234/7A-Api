using H7A_Api.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace H7A_Api.Controllers
{
    using System.Collections.Generic;
    [Route("/api")]
    public class ApiController : Controller
    {

        private readonly AppDbContext _context;

        public ApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("categories")]
        public async Task<ActionResult> GetAllCategories()
        {
            var techs = await GetCategoriesByType("ky-thuat");
            var services = await GetCategoriesByType("dich-vu");
            var abouts = await GetCategoriesByType("gioi-thieu");

            return Ok(new { techs, services, abouts });

        }

        [HttpGet("categories/techs")]
        public async Task<ActionResult> GetTechCategories()
        {

            var results = await GetCategoriesByType("ky-thuat");

            return Ok(results);
        }

        [HttpGet("categories/abouts")]
        public async Task<ActionResult> GetAboutCategories()
        {

            var results = await GetCategoriesByType("gioi-thieu");
            return Ok(results);
        }

        [HttpGet("categories/services")]
        public async Task<ActionResult> GetServicesCategories()
        {

            var results = await GetCategoriesByType("dich-vu");

            return Ok(results);
        }

        private Task<List<CategoriesDTO>> GetCategoriesByType(string type)
        {
            // const _context = new AppDbContext();
            var results = _context.TableTintucLists.Where(ttl => ttl.Type == type && ttl.Hienthi == true).Select(ttl => new CategoriesDTO
            {
                id = ttl.Id,
                name = ttl.Ten_Vi,
                slug = ttl.Tenkhongdau_Vi,
                children = _context.TableTintuc.Where(tt => tt.Id_Lv0 == ttl.Id && tt.Hienthi == true).Select(tt => new CategoryChildDTO
                {
                    id = tt.Id,
                    name = tt.Ten_Vi,
                    slug = tt.Tenkhongdau_Vi,
                }).ToList(),
            }).ToListAsync();

            return results;
        }

        public class CategoriesDTO
        {
            public int id { get; set; }

            public string name { get; set; }
            public string slug { get; set; }
            public List<CategoryChildDTO> children { get; set; }

        }
        public class CategoryChildDTO
        {

            public uint id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
        }

        public class ArticleDTO
        {
            public uint id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string imgUrl { get; set; }
            public int createdAt { get; set; }
        }

        [HttpGet("news")]
        public async Task<ActionResult> GetAllNews()
        {

            var result = await GetArticlesByType("tin-tuc");
            return Ok(result);
        }

        [HttpGet("media")]
        public async Task<ActionResult> GetAllMedia()
        {
            var result = await GetArticlesByType("thong-tin-truyen-thong");

            return Ok(result);
        }

        public Task<List<ArticleDTO>> GetArticlesByType(string type)
        {
            return _context.TableTintuc.Where(tt => tt.Hienthi == true && tt.Type == type)
                                              .OrderByDescending((tt) => tt.Noibat)
                                              .ThenByDescending((tt) => tt.Ngaysua)
                                              .Select(tt => new ArticleDTO
                                              {
                                                  id = tt.Id,
                                                  name = tt.Ten_Vi,
                                                  slug = tt.Tenkhongdau_Vi,
                                                  createdAt = tt.Ngaytao,
                                                  imgUrl = tt.Thumb,
                                              }).ToListAsync();
        }

        [HttpGet("article/{id}")]
        public async Task<ActionResult> GetArticleDetail(uint id)
        {
            var result = await _context.TableTintuc.FindAsync(id);
            if (result == null || result.Hienthi == false)
            {
                return NotFound(new { message = "article not found" });
            }

            return Ok(result);

        }
    }
}
