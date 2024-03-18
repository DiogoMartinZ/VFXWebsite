using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace VFXWebsite.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public string message="";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var success = false;

            if (Credential.UserId == "1" && Credential.ClientId == "1" && Credential.Password == "1")
            {
                success = true;
            }

            if (success)
            {
                Response.Redirect("MainPage");

            }
            else
            {
                message = "To login please type \"1\" on each input, thanks!";
            }


        }
    }

    public class Credential
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
