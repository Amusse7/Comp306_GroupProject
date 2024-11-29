namespace LibraryManagementAPI.DTOs.Transaction
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? FineAmount { get; set; }
        public string Status { get; set; }
    }
}