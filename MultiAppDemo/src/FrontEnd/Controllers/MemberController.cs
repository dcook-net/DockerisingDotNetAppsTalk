using System.Threading.Tasks;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class MemberController : Controller
    {
        private readonly MembersApiService _apiService;

        public MemberController(MembersApiService apiService)
        {
            _apiService = apiService;
        }
        
        public async Task<IActionResult> Index(string id)
        {
            var member = await _apiService.GetMember(id);

            return View(member);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _apiService.DeleteMember(id);

            return Redirect("~/Home/");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Member updatedMember)
        {
            await _apiService.UpdateMemberDetails(updatedMember);

            return Redirect("~/Home/");
        }
    }
}