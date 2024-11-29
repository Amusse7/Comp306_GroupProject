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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<List<TransactionDto>>>> GetAll()
        {
            var transactions = await _repository.GetAllAsync();
            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);
            return Ok(new ApiResult<List<TransactionDto>> { Data = transactionDtos, Message = "Transactions retrieved successfully" });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<TransactionDto>>> GetById(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null)
                return NotFound(new ApiResult<TransactionDto> { Message = "Transaction not found" });

            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(new ApiResult<TransactionDto> { Data = transactionDto, Message = "Transaction retrieved successfully" });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<TransactionDto>>> Create([FromBody] CreateTransactionDto createTransactionDto)
        {
            var transaction = _mapper.Map<Transaction>(createTransactionDto);
            await _repository.AddAsync(transaction);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);

            return CreatedAtAction(nameof(GetById),
                new { id = transaction.Id },
                new ApiResult<TransactionDto> { Data = transactionDto, Message = "Transaction created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult<TransactionDto>>> Update(int id, [FromBody] UpdateTransactionDto updateTransactionDto)
        {
            var existingTransaction = await _repository.GetByIdAsync(id);
            if (existingTransaction == null)
                return NotFound(new ApiResult<TransactionDto> { Message = "Transaction not found" });

            var transaction = _mapper.Map<Transaction>(updateTransactionDto);
            transaction.Id = id;
            await _repository.UpdateAsync(transaction);

            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(new ApiResult<TransactionDto> { Data = transactionDto, Message = "Transaction updated successfully" });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ApiResult<TransactionDto>>> Patch(int id, [FromBody] JsonPatchDocument<TransactionDto> patchDoc)
        {
            var existingTransaction = await _repository.GetByIdAsync(id);
            if (existingTransaction == null)
                return NotFound(new ApiResult<TransactionDto> { Message = "Transaction not found" });

            var transactionDto = _mapper.Map<TransactionDto>(existingTransaction);
            patchDoc.ApplyTo(transactionDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _mapper.Map(transactionDto, existingTransaction);
            await _repository.UpdateAsync(existingTransaction);

            return Ok(new ApiResult<TransactionDto> { Data = transactionDto, Message = "Transaction patched successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResult<bool>>> Delete(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null)
                return NotFound(new ApiResult<bool> { Message = "Transaction not found" });

            await _repository.DeleteAsync(id);
            return Ok(new ApiResult<bool> { Data = true, Message = "Transaction deleted successfully" });
        }
    }
}
