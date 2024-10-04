using System.Diagnostics;

namespace OdataBookStore.Models
{
    public static class DataSource
    {

        private static IList<Book> listBooks { get; set; }
        public static IList<Book> GetBooks()
        {
            if (listBooks != null)
            {
                return listBooks;
            }
            listBooks = new List<Book>();
            Book book = new Book
            {
                Id = 1,
                ISBN = "978-0-321-87758-1",
                Title = "Essential C#5.0",
                Author = "Mark Michaelis",
                Price = 59,
                Location = new Address
                {
                    City = "HCM City",
                    Street = "D2, Thu Duc District"
                },
                Press = new Press
                {
                    Id = 1,
                    Name = "Addison-Wesley",
                    Category = Category.Book
                }
            };

            Book book2 = new Book
            {
                Id = 2,
                ISBN = "978-1-61729-134-0",
                Title = "Pro ASP.NET Core 3",
                Author = "Adam Freeman",
                Price = 45,
                Location = new Address
                {
                    City = "New York",
                    Street = "Broadway"
                },
                Press = new Press
                {
                    Id = 2,
                    Name = "Apress",
                    Category = Category.Book
                }
            };
            listBooks.Add(book2);

            // Book 3
            Book book3 = new Book
            {
                Id = 3,
                ISBN = "978-3-16-148410-0",
                Title = "Introduction to Machine Learning with Python",
                Author = "Andreas C. Müller, Sarah Guido",
                Price = 29,
                Location = new Address
                {
                    City = "Berlin",
                    Street = "Friedrichstraße"
                },
                Press = new Press
                {
                    Id = 3,
                    Name = "O'Reilly Media",
                    Category = Category.Book
                }
            };
            listBooks.Add(book3);
            listBooks.Add(book);
            return listBooks;
        }
    }
}