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
      return Ok(new
      {
        news = from news in
        _context.TableTintuc.ToList()
               join newsList in _context.TableTintucLists.ToList()
               on news.Id_Lv0 equals newsList.Id
               select new
               {

               }
      });
    }
  }
}
