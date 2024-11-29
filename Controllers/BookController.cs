using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LibraryManagementAPI.DTOs;
using LibraryManagementAPI.Repository;
using LibraryManagementAPI.Database.Entities;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<BookDto>>>> GetAll()
        {
            var books = await _repository.GetAllAsync();
            var bookDtos = _mapper.Map<List<BookDto>>(books);
            return Ok(new ApiResult<List<BookDto>> { Data = bookDtos, Message = "Books retrieved successfully" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<BookDto>>> GetById(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
                return NotFound(new ApiResult<BookDto> { Message = "Book not found" });

            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(new ApiResult<BookDto> { Data = bookDto, Message = "Book retrieved successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<BookDto>>> Create([FromBody] CreateBookDto createBookDto)
        {
            var book = _mapper.Map<Book>(createBookDto);
            await _repository.AddAsync(book);
            var bookDto = _mapper.Map<BookDto>(book);

            return CreatedAtAction(nameof(GetById),
                new { id = book.Id },
                new ApiResult<BookDto> { Data = bookDto, Message = "Book created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<BookDto>>> Update(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null)
                return NotFound(new ApiResult<BookDto> { Message = "Book not found" });

            var book = _mapper.Map<Book>(updateBookDto);
            book.Id = id;
            await _repository.UpdateAsync(book);

            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(new ApiResult<BookDto> { Data = bookDto, Message = "Book updated successfully" });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResult<BookDto>>> Patch(int id, [FromBody] JsonPatchDocument<BookDto> patchDoc)
        {
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null)
                return NotFound(new ApiResult<BookDto> { Message = "Book not found" });

            var bookDto = _mapper.Map<BookDto>(existingBook);
            patchDoc.ApplyTo(bookDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(bookDto, existingBook);
            await _repository.UpdateAsync(existingBook);

            return Ok(new ApiResult<BookDto> { Data = bookDto, Message = "Book patched successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
                return NotFound(new ApiResult<bool> { Message = "Book not found" });

            await _repository.DeleteAsync(id);
            return Ok(new ApiResult<bool> { Data = true, Message = "Book deleted successfully" });
        }
    }
}

