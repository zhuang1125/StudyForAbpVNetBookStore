using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace Acme.BookStore.OrganizationUnits
{
   
    public  class OrganizationUnitAppService : CrudAppService<
            OrganizationUnit, //The OrganizationUnit entity
            OrganizationUnitDto, //Used to show OrganizationUnits
            Guid, //Primary key of the OrganizationUnit entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateOrganizationUnitDto>, IOrganizationUnitAppService

    {
        private OrganizationUnitManager _organizationUnitManager;

        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        public OrganizationUnitAppService(IRepository<OrganizationUnit, Guid> repository,
            IOrganizationUnitRepository organizationUnitRepository, OrganizationUnitManager organizationUnitManager,IGuidGenerator guidGenerator,ICurrentTenant currentTenant)
            : base(repository)
        {
            _organizationUnitRepository = organizationUnitRepository;
            _organizationUnitManager = organizationUnitManager;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            GetPolicyName = BookStorePermissions.OrganizationUnits.Default;
            GetListPolicyName = BookStorePermissions.OrganizationUnits.Default;
            CreatePolicyName = BookStorePermissions.OrganizationUnits.Create;
            UpdatePolicyName = BookStorePermissions.OrganizationUnits.Edit;
            DeletePolicyName = BookStorePermissions.OrganizationUnits.Delete;

        }

        [Authorize(BookStorePermissions.OrganizationUnits.Edit)]
        public override async Task<OrganizationUnitDto> CreateAsync(CreateUpdateOrganizationUnitDto input)
        {
            var _guid = _guidGenerator.Create();
            
            if(input.ParentId!=null)
            {
                var parent = await _organizationUnitRepository.FindAsync(input.ParentId.Value);
                if (parent == null)
                {
                    throw new BusinessException($"Parent OrganizationUnit with Id:{input.ParentId} not found!");
                }
            }
            await _organizationUnitManager.CreateAsync(new OrganizationUnit(_guid, input.DisplayName,input.ParentId, _currentTenant.Id==null?input.TenantId: _currentTenant.Id));

            var root1 = await _organizationUnitRepository.GetAsync(input.DisplayName);

            return ObjectMapper.Map<OrganizationUnit, OrganizationUnitDto>(root1);

           // return base.CreateAsync(input);
        }

        public override async Task<OrganizationUnitDto> UpdateAsync(Guid id, CreateUpdateOrganizationUnitDto input)
        {
            if (input.ParentId != null)
            {
                var parent = await _organizationUnitRepository.FindAsync(input.ParentId.Value);
                if (parent == null)
                {
                    throw new BusinessException($"Parent OrganizationUnit with Id:{input.ParentId} not found!");
                }
            }
            input.TenantId = _currentTenant.Id == null ? input.TenantId : _currentTenant.Id;

            return await base.UpdateAsync(id, input); 
        }

        //[Authorize(BookStorePermissions.OrganizationUnits.Edit)]
        //public async Task UpdateAsync(Guid id, CreateUpdateOrganizationUnitDto input)
        //{
        //   var author = await _organizationUnitRepository.GetAsync(id);

        ////    //if (author.do != input.Name)
        ////    //{
        //       await _organizationUnitManager.UpdateAsync(input);
        ////   // }

        ////    //author.BirthDate = input.BirthDate;
        ////    author.ShortBio = input.ShortBio;

        ////    await _authorRepository.UpdateAsync(author);
        //}
    }
}
