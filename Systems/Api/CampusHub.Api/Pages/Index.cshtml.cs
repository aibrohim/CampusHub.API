using CampusHub.Common;
using CampusHub.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace CampusHub.Api.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public bool OpenApiEnabled => settings.Enabled;

        [BindProperty]
        public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion();

        [BindProperty]
        public string HelloMessage => "Hello guys";

        //[BindProperty]
        //public string IdentityServerUrl => identitySettings.Url;



        private readonly SwaggerSettings settings;
        //private readonly IdentitySettings identitySettings;

        public IndexModel(SwaggerSettings settings)
        {
            this.settings = settings;
            //this.identitySettings = identitySettings;
        }

        public void OnGet()
        {

        }
    }
}