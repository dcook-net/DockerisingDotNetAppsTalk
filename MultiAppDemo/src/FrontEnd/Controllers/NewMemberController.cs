using System.Threading.Tasks;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class NewMemberController : Controller
    {
        private readonly MembersApiService _apiService;

        public NewMemberController(MembersApiService apiService)
        {
            _apiService = apiService;
        }
        
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Member newMember)
        {
            var member = await _apiService.CreateMember(newMember);

            return Redirect("~/Home/");
        }
    }
}