using AjmeraAssignment.Models;
using AjmeraAssignment.Models.DTO;
using AjmeraAssignment.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AjmeraAssignment.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookRepository<Book, BookDTO> _bookRepository;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookRepository<Book, BookDTO> bookRepository, ILogger<BookController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        [HttpGet("books")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var books = await _bookRepository.GetAll();
                if (books == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(books);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("book/{id}")]
        public  IActionResult Get(string id)
        {
            try
            {
                var book = _bookRepository.GetDto(id);
                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(book);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                 book.Id= System.Guid.NewGuid().ToString();
                _bookRepository.Add(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
            return Ok();
        }
    }
}
