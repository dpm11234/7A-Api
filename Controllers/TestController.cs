using Microsoft.AspNetCore.Mvc;

namespace _7A_Api.Controllers
{
  public class TestController : Controller
  {
    public IActionResult Index()
    {
      return Ok(new { msg = "Test Controller" });
    }
  }
}
