using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers
    {
        [ApiController]
        [Route("api/[controller]s")]
        public class bookController : ControllerBase
        {
            private readonly BookStoreDbContext _context;
            private readonly IMapper _mapper;
        public bookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
            public IActionResult GetBooks() //Get All Books
            {
                GetBooksQuery query = new GetBooksQuery(_context,_mapper);
                var result = query.Handle();
                return Ok(result);
            }

           
            [HttpGet("{id}")] //Get Book By Id
            public IActionResult GetBookById(int id)
            {
                BookDetailViewModel result;
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId =id;
                result =query.Handle();
                return Ok(result);
            }

            [HttpPost]// Add Books
            public IActionResult AddBook([FromBody] CreateBookModel newBook)
            {
                CreateBookCommand command = new CreateBookCommand(_context,_mapper);
                command.Model = newBook;
                command.Handle();
                return Ok();
            }

            [HttpPut("{id}")]//Update Books
            public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook;
                command.Handle();
                return Ok();
                
            }

            [HttpDelete("{id}")]//Delete Books
            public IActionResult DeleteBook(int id)
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId=id;
                command.Handle();
                return Ok();
            }

        }
    }