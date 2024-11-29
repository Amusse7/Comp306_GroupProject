namespace LibraryManagementAPI.DTOs.Transaction
{
    public class CreateTransactionDto
    {
        public int BookId { get; set; }
        public string MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}