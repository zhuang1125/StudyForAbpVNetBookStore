using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.OrganizationUnits;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace Acme.BookStore.Web.Pages.OrganizationUnits
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreatOrganizationUnitViewModel OrganizationUnit { get; set; }

        public List<SelectListItem> OrganizationUnits { get; set; }

        public List<SelectListItem> Tenants { get; set; }

        private readonly IOrganizationUnitAppService _bookAppService;
        private readonly OrganizationUnitManager _organizationUnitManager;
        protected ITenantAppService _tenantAppService { get; }

        protected ICurrentTenant _currentTenant { get; }

        //public DefaultTencentCloudBlobNameCalculator(ICurrentTenant currentTenant)
        // {

        public CreateModalModel(
        IOrganizationUnitAppService bookAppService, OrganizationUnitManager organizationUnitManager, ITenantAppService tenantAppService, ICurrentTenant currentTenant)
        {
            _bookAppService = bookAppService;
            _tenantAppService = tenantAppService;
            _currentTenant = currentTenant;
            _organizationUnitManager = organizationUnitManager;
        }

        public async Task OnGetAsync()
        {
            OrganizationUnit = new CreatOrganizationUnitViewModel();
            var childrens = await _organizationUnitManager.FindChildrenAsync(null, true);

            OrganizationUnits = childrens.OrderBy(t=>t.Code).Select(x => new SelectListItem($"{x.Code} {x.DisplayName}", x.Id.ToString())).ToList();
            OrganizationUnits.Insert(0,new SelectListItem() { Text = "Root", Value = null });
            if (_currentTenant.Id == null)
            {
                Tenants = new List<SelectListItem>() { new SelectListItem() { Text = "Host", Value = null } };
            }
            else
            {
                var temptenants = await _tenantAppService.GetListAsync(new GetTenantsInput() { });
                Tenants = temptenants.Items.OrderBy(t=>t.Name).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                Tenants.Insert(0, new SelectListItem() { Text = "Host", Value = null });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {


                var dd = ObjectMapper.Map<CreatOrganizationUnitViewModel, CreateUpdateOrganizationUnitDto>(OrganizationUnit);
                // dd.Code=_organizationUnitManager.GuidGenerator.Create();
                await _bookAppService.CreateAsync(
                   dd
                    );
                return NoContent();
            }
            catch (Exception ee)
            {

                throw;
            }
        }

        public class CreatOrganizationUnitViewModel
        {
            [DisplayName("TenantId")]
            [SelectItems(nameof(Tenants))]
            public Guid? TenantId { get; set; }


            [DisplayName("ParentId")]
            [SelectItems(nameof(OrganizationUnits))]
            public Guid? ParentId { get; set; }

            //[StringLength(128)]
            //public string Code { get; set; }


            [StringLength(128)]
            public string DisplayName { get; set; }



            /*
              public string TenantId { get; set; }//: Tenant's Id of this OU. Can be null for host OUs.
        public string ParentId { get; set; }//: Parent OU's Id. Can be null if this is a root OU.
        public string Code { get; set; }//: A hierarchical string code that is unique for a tenant.
        public string DisplayName { get; set; }//: Shown name of the OU.
             */

        }
    }
}
