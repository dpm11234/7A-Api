using H7A_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace _7A_Api.Controllers
{
  public class TestController : Controller
  {
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
      _context = context;
    }

    [Route("/api/test")]
    public IActionResult Index()
    {

      var tintuc = from tt in _context.TableTintuc select new { tt.Id_Lv0, name = tt.Ten_Vi, slug = tt.Tenkhongdau_Vi };
      var tinTucList = from ttl in _context.TableTintucLists select new { ttl.Id, name = ttl.Ten_Vi, slug = ttl.Tenkhongdau_Vi };

      var query = from ttl in tinTucList.ToList()
                  join tt in tintuc.ToList() on ttl.Id equals tt.Id_Lv0
                  into grouped
                  select new
                  {
                    Name = ttl.name,
                    Slug = ttl.slug,
                    News = from item in grouped select new { item.name, item.slug }
                  };

      return Ok(new
      {
        news = query.ToList()
      });
    }
  }
}
