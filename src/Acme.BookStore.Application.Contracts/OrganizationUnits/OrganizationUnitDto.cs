using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.OrganizationUnits
{
    public class OrganizationUnitDto : ExtensibleAuditedEntityDto<Guid>
    {      
        public Guid?  TenantId { get; set; }//: Tenant's Id of this OU. Can be null for host OUs.
        public Guid? ParentId { get; set; }//: Parent OU's Id. Can be null if this is a root OU.
        public string Code { get; set; }//: A hierarchical string code that is unique for a tenant.
        public string DisplayName { get; set; }//: Shown name of the OU.
    }
}