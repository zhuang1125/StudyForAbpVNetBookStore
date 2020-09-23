using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.OrganizationUnits
{


    public interface IOrganizationUnitAppService :
      ICrudAppService< //Defines CRUD methods
          OrganizationUnitDto, //Used to show books
          Guid, //Primary key of the book entity
          PagedAndSortedResultRequestDto, //Used for paging/sorting
          CreateUpdateOrganizationUnitDto> //Used to create/update a book
    {
      //  public interface IOrganizationUnitAppService : ICrudAppService< //Defines CRUD methods
    //         OrganizationUnitDto, //Used to show books
    //         Guid, //Primary key of the book entity
    //         PagedAndSortedResultRequestDto, //Used for paging/sorting
    //         CreateUpdateOrganizationUnitDto>
    //{
    }
}
