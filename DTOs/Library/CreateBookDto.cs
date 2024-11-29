﻿namespace LibraryManagementAPI.DTOs.Library
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public int CopiesAvailable { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}