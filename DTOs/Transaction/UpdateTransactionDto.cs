namespace LibraryManagementAPI.DTOs.Transaction
{
    public class UpdateTransactionDto
    {
        public DateTime? ReturnDate { get; set; }
        public decimal? FineAmount { get; set; }
        public string Status { get; set; }
    }
}