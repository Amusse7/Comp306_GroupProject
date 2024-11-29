using AutoMapper;
using LibraryManagementAPI.Database.Entities;
using LibraryManagementAPI.DTOs.Library;
using LibraryManagementAPI.DTOs.Transaction;

namespace LibraryManagementAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();
            CreateMap<Book, DeleteBookDto>().ReverseMap();

            // Transaction mappings
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
            CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
            CreateMap<Transaction, DeleteTransactionDto>().ReverseMap();
        }
    }
}
