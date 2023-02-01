
using Acme.BookStore.Books;

using Acme.BookStore.Email;
using Acme.BookStore.Emailsend;
using Acme.BookStore.Order;
using Acme.BookStore.OrderModul;
using Acme.BookStore.ProductInterface;
using Acme.BookStore.ProductModule;
using AutoMapper;
using BOOKSTore.Email;
using BOOKSTore.Email_send;
using BOOKSTore.Employee;
using BOOKSTore.Employee.DTO;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<InputEmailConfigration, EmailSettings>().ReverseMap();
        CreateMap<EmailSettings, EmailSettingsDTO>().ReverseMap();

        CreateMap<EmployeeDetails, EmployeeDTO>().ReverseMap();


        CreateMap<Product, ProductDto>();
        CreateMap<CreadeProductDto, Product>();
        CreateMap<UpdareProductDto, Product>();


        CreateMap<Orders, OrderDTO>();
        //CreateMap<CreadeProductDto, Product>();
        CreateMap<OrderLine, OrderLineDto>();


        CreateMap<Productlist, productlistDTO>();
        CreateMap<OrderName, OrderNameDTO>();


        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
