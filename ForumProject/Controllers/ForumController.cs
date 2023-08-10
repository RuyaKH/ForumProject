using ForumProject.Models;
using ForumProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ForumProject.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _service;
        //private readonly UserManager<AppUser> _userManager;

        public ForumController(IForumService service)
        {
            _service = service;
        }

        //private string GetRole()
        //{
        //    return HttpContext.User.IsInRole("User") ? "User" : "Admin";
        //}

        public async Task<IActionResult> Index(string searchString)
        {
            var response = await _service.GetForumItemsAsync(searchString);
            if (response.Success == false)
                return Problem(response.Message);
            return View(response.Data);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        public async Task<IActionResult> Details(int? id)
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var response = await _service.GetDetailsAsync(id);
            if (!response.Success)
                return NotFound();
            return View(response.Data);
        }

        // GET: Movies/Create
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Status,DatePosted,UpVotes")] ForumModel thread)
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                await _service.CreateThreadAsync(thread);
                return RedirectToAction(nameof(Index));
            }
            return View(thread);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var response = await _service.GetDetailsAsync(id);
            if (!response.Success)
                return NotFound();

            return View(response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,DatePosted,UpVotes")] ForumModel thread)
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (id != thread.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _service.EditThreadAsync(id, thread);
                if (response.Success)
                    return RedirectToAction(nameof(Index));
                return NotFound();
            }
            return View(thread);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var response = await _service.GetDetailsAsync(id);
            if (!response.Success) return NotFound();
            return View(response.Data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var response = await _service.DeleteThreadAsync(id);
            if (response == null)
            {
                return Problem("Entity Null");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
