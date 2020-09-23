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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace Acme.BookStore.Web.Pages.OrganizationUnits
{
    public class EditModalModel : BookStorePageModel
    {
        [BindProperty]
        public EditOrganizationUnitViewModel OrganizationUnit { get; set; }


        public List<SelectListItem> OrganizationUnits { get; set; }

        public List<SelectListItem> Tenants { get; set; }
        
        //private readonly IOrganizationUnitAppService _bookAppService;

        // public EditModalModel(IOrganizationUnitAppService bookAppService)
        // {
        //    _bookAppService = bookAppService;
        // }



        private readonly IOrganizationUnitAppService _bookAppService;
        private readonly OrganizationUnitManager _organizationUnitManager;



        protected ITenantAppService _tenantAppService { get; }
        protected ICurrentTenant _currentTenant { get; }
        //public EditModalModel(ITenantAppService tenantAppService)
        //{
        //   
        //}

        public EditModalModel(
            IOrganizationUnitAppService bookAppService, OrganizationUnitManager organizationUnitManager,ITenantAppService tenantAppService, ICurrentTenant currentTenant)
        {
            _bookAppService = bookAppService;
            _tenantAppService = tenantAppService;
            _organizationUnitManager = organizationUnitManager;
            _currentTenant =currentTenant;
        }

        public async Task OnGetAsync(Guid id)
        {
            var bookDto = await _bookAppService.GetAsync(id);
            OrganizationUnit = ObjectMapper.Map<OrganizationUnitDto, EditOrganizationUnitViewModel>(bookDto);

            var childrens = await _organizationUnitManager.FindChildrenAsync(null, true);

            OrganizationUnits = childrens.OrderBy(t => t.Code).Select(x => new SelectListItem($"{x.Code} {x.DisplayName}", x.Id.ToString())).ToList();
            OrganizationUnits.Insert(0, new SelectListItem() { Text = "Root", Value = null });
            if (_currentTenant.Id == null)
            {
                Tenants = new List<SelectListItem>() { new SelectListItem() { Text = "Host", Value = null } };
            }
            else
            {
                var temptenants = await _tenantAppService.GetListAsync(new GetTenantsInput() { });
                Tenants = temptenants.Items.OrderBy(t => t.Name).Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                Tenants.Insert(0, new SelectListItem() { Text = "Host", Value = null });
            }


        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.UpdateAsync(
                 OrganizationUnit.Id,
                 ObjectMapper.Map<EditOrganizationUnitViewModel, CreateUpdateOrganizationUnitDto>(OrganizationUnit)
             );
            return NoContent();
        }

        public class EditOrganizationUnitViewModel
        {
            //[HiddenInput]
            //public Guid Id { get; set; }

            //[SelectItems(nameof(Authors))]
            //[DisplayName("Author")]
            //public Guid AuthorId { get; set; }

            //[Required]
            //[StringLength(128)]
            //public string Name { get; set; }

            //[Required]
            //public BookType Type { get; set; } = BookType.Undefined;

            //[Required]
            //[DataType(DataType.Date)]
            //public DateTime PublishDate { get; set; } = DateTime.Now;

            //[Required]
            //public float Price { get; set; }
            [HiddenInput]
            public Guid Id { get; set; }

           
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

        }
    }
}
