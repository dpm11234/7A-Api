using H7A_Api.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
// using System.Data.Entity;

namespace H7A_Api.Controllers
{
    [Route("/api")]
    public class ApiController : Controller
    {
        private readonly AppDbContext _context;

        public ApiController(AppDbContext context)
        {
            _context = context;

            // _context.Database. = s => System.Diagnostics.Debug.WriteLine(s);

        }

        [HttpGet("categories")]
        public IActionResult Index()
        {

            var tintuc = (from tt in _context.TableTintuc
                          where tt.Hienthi == true && tt.Ten_Vi != "" && tt.Tenkhongdau_Vi.Length >0
                          select new { tt.Id_Lv0, name = tt.Ten_Vi, slug = tt.Tenkhongdau_Vi })
                         .ToList();
            var tinTucList = (from ttl in _context.TableTintucLists
                              select new { ttl.Id, name = ttl.Ten_Vi, slug = ttl.Tenkhongdau_Vi, ttl.Type })
                             .ToList();

            var query = from ttl in tinTucList
                        join tt in tintuc on ttl.Id equals tt.Id_Lv0
                        into joined
                        where ttl.Type == "ky-thuat"
                        select new
                        {
                            Name = ttl.name,
                            Slug = ttl.slug,
                            children = from item in joined select new { item.name, item.slug }
                        };

            return Ok(new
            {
                news = query.ToList()
            });
        }


        [HttpGet("news")]
        public IActionResult GetAllNews()
        {

            var result = _context.TableTintuc.Where(tt => tt.Hienthi == true && tt.Type == "tin-tuc")
                                             .OrderByDescending((tt) => tt.Noibat)
                                             .ThenByDescending((tt) => tt.Ngaysua)
                                             .Select(tt => new { id = tt.Id, name = tt.Ten_Vi, slug = tt.Tenkhongdau_Vi, createdAt = tt.Ngaytao, pop = tt.Noibat })
                                             .ToList();

            return Ok(result);
        }

        [HttpGet("media")]
        public IActionResult GetAllMedia()
        {
            return Ok("Media");
        }

        [HttpGet("article/{id}")]
        public async Task<ActionResult> GetArticleDetail(uint id)
        {
            // System.Console.WriteLine(slug);
            var result = await _context.TableTintuc.FindAsync(id);
            // return Ok("Article");
            if (result == null || result.Hienthi == false)
            {
                return NotFound(new { message = "article not found" });
            }

            return Ok(result);

        }
    }
}
