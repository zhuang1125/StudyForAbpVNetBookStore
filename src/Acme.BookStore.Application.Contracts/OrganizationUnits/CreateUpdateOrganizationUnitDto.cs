using System;
using System.Collections.Generic;
using Volo.Abp.Data;

namespace Acme.BookStore.OrganizationUnits
{
    public class CreateUpdateOrganizationUnitDto : IHasExtraProperties
    {
        public Guid? TenantId { get; set; }//: Tenant's Id of this OU. Can be null for host OUs.
        public Guid? ParentId { get; set; }//: Parent OU's Id. Can be null if this is a root OU.
        public string Code { get; set; }//: A hierarchical string code that is unique for a tenant.
        public string DisplayName { get; set; }//: Shown name of the OU.
        public Dictionary<string, object> ExtraProperties { get; set; } = new Dictionary<string, object>();
    }
}