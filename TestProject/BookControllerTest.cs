using AjmeraAssignment.Controllers;
using AjmeraAssignment.Models;
using AjmeraAssignment.Models.DTO;
using AjmeraAssignment.Models.Repository;
using AjmeraAssignment.Service;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class BookControllerTest
    {
        BookController _bookController;
        EmployeeContext _employeeContext;
        IMapper _mapper;
        IBookRepository<Book, BookDTO> _bookRepository;
        ILogger<BookController> _logger;

        [SetUp]
        public void SetUp()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _employeeContext = new EmployeeContext();
            _bookRepository = new BookService(_employeeContext,_mapper);
            _logger = new Logger<BookController>(new NullLoggerFactory());
            _bookController = new BookController(_bookRepository,_logger);
        }

        [Test]
        public async Task GetAllTest()
        {
            var books = await _bookController.Get();
            var noContentResult = books as NoContentResult;
            if (noContentResult != null)
            {
                Assert.IsNotNull(noContentResult);
                Assert.AreEqual(noContentResult.StatusCode,204);
            }
            else
            {
                var okResult = books as OkObjectResult;
                Assert.IsNotNull(okResult);
                var result = okResult.Value as List<Book>;
                Assert.IsNotNull(result);
            }
        }
        [TestCase("1a2b","")]
        [TestCase("198df271-8987-4ec8-8549-effe3c5facfb", "ABC")]
        public void GetTest(string id, string expected)
        {
            var actionResult = _bookController.Get(id);
            Assert.IsNotNull(actionResult);
            if (id == "1a2b")
            {
                var notFoundResult = actionResult as NotFoundResult;
                Assert.IsNotNull(notFoundResult);
                Assert.AreEqual(notFoundResult.StatusCode,404);
            }
            else
            {
                var okResult = actionResult as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.AreEqual(okResult.StatusCode,200);
                var book = okResult.Value as BookDTO;
                Assert.IsNotNull(book);
                Assert.IsNotNull(book.Id);
                Assert.IsNotNull(book.AuthorName);
                Assert.IsNotNull(book.Name);
                Assert.AreEqual(book.Name, expected);
            }
        }

        [TestCase("The Man","John", 200)]
        public void PostTest(string book, string author, int statusCode)
        {
            Book bookObj = new Book() { Name = book, AuthorName = author};
            var actionResult = _bookController.Post(bookObj);
            var response = actionResult as OkResult;
            if (response != null)
            {
                Assert.AreEqual(response.StatusCode, statusCode);
            }
            else
            {
                Assert.AreEqual(response.StatusCode,400);
            }
        }
    }
}
