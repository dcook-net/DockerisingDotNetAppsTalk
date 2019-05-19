using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly MembersApiService _apiService;

        public HomeController(MembersApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var members = await _apiService.GetMembersAsync();

            return View(members);
        }
    }
}