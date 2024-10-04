﻿namespace OdataBookStore.Models
{
    public class BookRequest
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }

    }
}
