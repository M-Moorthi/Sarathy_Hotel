using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel_Bluebird.Pages.Shared
{
    public class _UserLayout : PageModel
    {
        public string UserName { get; set; }

        public void OnGet()
        {
            // Retrieve the session data from TempData
            if (TempData.ContainsKey("UserName"))
            {
                UserName = TempData["UserName"].ToString();
            }
        }
    }
}
