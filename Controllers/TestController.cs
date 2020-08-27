using H7A_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace _7A_Api.Controllers
{
  public class TestController : Controller
  {
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      return Ok(new { news = _context.Table_Tintuc });
    }
  }
}
