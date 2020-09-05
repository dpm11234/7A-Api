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


        [HttpGet("sub-categories/{id}")]
        public async Task<ActionResult> getSubCategories(uint id)
        {
            var parentId = await _context.TableTintuc.Where(tt => tt.Id == id).Select(tt => tt.Id_Lv0).SingleOrDefaultAsync();
            var results = await _context.TableTintucLists.Where(ttl => ttl.Id == parentId && ttl.Hienthi == true)
                                                         .Select(ttl => _context.TableTintuc.Where(tt => tt.Id_Lv0 == ttl.Id && tt.Hienthi == true).Select(tt => new CategoryChildDTO
                                                         {
                                                             id = tt.Id,
                                                             name = tt.Ten_Vi,
                                                             slug = tt.Tenkhongdau_Vi,
                                                         }).ToList()
                                                         ).SingleOrDefaultAsync();

            if (results == null)
            {
                return NotFound(new { message = "No subcategories" });
            }

            return Ok(results);
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
            var result = await _context.TableTintuc.Where(tt => tt.Hienthi == true && tt.Id == id)
                                                .Select(tt => new ArticleDetailsDTO
                                                {
                                                    id = tt.Id,
                                                    name = tt.Ten_Vi,
                                                    slug = tt.Tenkhongdau_Vi,
                                                    imgUrl = tt.Photo,
                                                    thumbUrl = tt.Thumb,
                                                    content = tt.Noidung_Vi,
                                                    updatedAt = tt.Ngaysua,
                                                    views = tt.Luotxem,
                                                })
                                                   .SingleOrDefaultAsync();
            if (result == null)
            {
                return NotFound(new { message = "article not found" });
            }
            return Ok(result);

        }

        [HttpGet("qa")]
        public async Task<ActionResult> GetQuestions()
        {
            var result = await _context.TableHoiDap.Where(hd => hd.HienThi == true)
                                                   .OrderByDescending(dd => dd.NgayDang)
                                                   .Select(hd => new QAndADTO
                                                   {
                                                       id = hd.Id,
                                                       answer = hd.NoiDung,
                                                       question = hd.Ten,
                                                       createdAt = hd.NgayDang,
                                                   })
                                                   .ToListAsync();
            return Ok(result);
        }

    }
}