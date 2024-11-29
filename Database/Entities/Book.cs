namespace LibraryManagementAPI.Database.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}