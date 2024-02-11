using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hotel_Bluebird.Pages.Login
{
    public class HomeModel : PageModel
    {
        // Define the UserName property
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
