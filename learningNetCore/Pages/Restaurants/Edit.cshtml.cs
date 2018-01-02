using learningNetCore.Models;
using learningNetCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace learningNetCore.Pages.Restaurants
{
    [Authorize]
    public class EditModel : PageModel
    {
        private IRestuarantData _restuarantData;

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public EditModel(IRestuarantData restuarantData)
        {
            _restuarantData = restuarantData;
        }

        public IActionResult OnGet(int id)
        {
            Restaurant =_restuarantData.Get(id);
            if (Restaurant == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Page();
        }

        //Uses the ID to update the approiate Restuarant
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _restuarantData.Update(Restaurant);
            return RedirectToAction("Details", "Home", new {id = Restaurant.Id});
        }
    }
}