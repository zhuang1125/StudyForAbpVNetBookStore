using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.OrganizationUnits;
using AutoMapper;
using Volo.Abp.AutoMapper;

namespace Acme.BookStore.Web
{
    public class BookStoreWebAutoMapperProfile : Profile
    {
        public BookStoreWebAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Web project.
            // ADD a NEW MAPPING
            CreateMap<Pages.Authors.CreateModalModel.CreateAuthorViewModel,
                      CreateAuthorDto>();


            // ADD THESE NEW MAPPINGS
            CreateMap<AuthorDto, Pages.Authors.EditModalModel.EditAuthorViewModel>();
            CreateMap<Pages.Authors.EditModalModel.EditAuthorViewModel,
                      UpdateAuthorDto>();

            //CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>();

            CreateMap<Pages.Books.CreateModalModel.CreateBookViewModel, CreateUpdateBookDto>().ForMember(x => x.ExtraProperties, opt => opt.Ignore());

            CreateMap<BookDto, Pages.Books.EditModalModel.EditBookViewModel>();
            CreateMap<Pages.Books.EditModalModel.EditBookViewModel, CreateUpdateBookDto>();



            CreateMap<Pages.OrganizationUnits.CreateModalModel.CreatOrganizationUnitViewModel, CreateUpdateOrganizationUnitDto>().ForMember(x => x.ExtraProperties, opt => opt.Ignore());

            CreateMap<OrganizationUnitDto, Pages.OrganizationUnits.EditModalModel.EditOrganizationUnitViewModel>();
            CreateMap<Pages.OrganizationUnits.EditModalModel.EditOrganizationUnitViewModel, CreateUpdateOrganizationUnitDto>();
        }
    }
}
