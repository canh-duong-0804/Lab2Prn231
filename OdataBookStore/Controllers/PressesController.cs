using OdataBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OdataBookStore.Models;

namespace _26_BuiVanToan_OdataBookStore.Controllers
{
    public class BooksController : ODataController
    {
        private BookStoreContext db;
        public BooksController(BookStoreContext context)
        {
            db = context;
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            if (context.Books.Count() == 0)
            {
                foreach (var b in DataSource.GetBooks())
                {
                    context.Books.Add(b);
                    context.Presses.Add(b.Press);

                }
                context.SaveChanges();
            }
        }
        [EnableQuery(PageSize = 10)]
        public IActionResult Get()
        {
            return Ok(db.Books);
        }
        [EnableQuery]
        public IActionResult Get(int id, string version)
        {
            var book = db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound();
            return Ok(book);

        }
        [HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {

            var b = new Book
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Location = new Address
                {
                    City = book.Location.City,
                    Street = book.Location.Street,
                }

            };

            db.Books.Add(book);

            db.SaveChanges();
            return NoContent();
        }

        [EnableQuery]
        public IActionResult Put([FromODataUri] int key, [FromBody] Book book)
        {
            Book b = db.Books.FirstOrDefault(c => c.Id == key);
            if (b == null)
            {
                return NotFound();
            }

            b.ISBN = book.ISBN;
            b.Title = book.Title;
            b.Author = book.Author;
            b.Price = book.Price;
            b.Location.Street = book.Location.Street;
            b.Location.City = book.Location.City;
            db.Books.Update(b);

            db.SaveChanges();
            return NoContent();
        }


        [EnableQuery]
        public IActionResult Delete([FromODataUri] int key)
        {
            Book b = db.Books.FirstOrDefault(c => c.Id == key);
            if (b == null)
            {
                return NotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return Ok(db);
        }
    }
}
