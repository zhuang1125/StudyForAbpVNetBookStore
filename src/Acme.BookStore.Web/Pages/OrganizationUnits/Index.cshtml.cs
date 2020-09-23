using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.Identity;

namespace Acme.BookStore.Web.Pages.OrganizationUnits
{
    public class IndexModel : PageModel
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        public IndexModel(OrganizationUnitManager organizationUnitManager)
        {
            _organizationUnitManager = organizationUnitManager;
        }
        public void OnGet()
        {
            
        }
    }
}
