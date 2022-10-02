using AjmeraAssignment.Models;
using AjmeraAssignment.Models.DTO;
using AjmeraAssignment.Models.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjmeraAssignment.Service
{
    public class BookService : IBookRepository<Book, BookDTO>
    {
        EmployeeContext _employeeContext;
        IMapper _mapper;
        public BookService(EmployeeContext employeeContext, IMapper mapper)
        {
            _employeeContext = employeeContext;
            _mapper = mapper;
        }
        void IBookRepository<Book, BookDTO>.Add(Book entity)
        {
            _employeeContext.Books.Add(entity);
            _employeeContext.SaveChanges();
        }

        async Task<IEnumerable<Book>> IBookRepository<Book, BookDTO>.GetAll()
        {
            return await Task.FromResult(_employeeContext.Books.ToList());
        }

        BookDTO IBookRepository<Book, BookDTO>.GetDto(string id)
        {
            var book = _employeeContext.Books
                .SingleOrDefault(b => b.Id == id);
            return _mapper.Map<BookDTO>(book);
        }
    }
}
