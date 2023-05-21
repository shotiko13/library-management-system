using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Library
    {
        private List<Book> books;

        public Library(List<Book> books)
        {
            this.books = books ?? new List<Book>();
        }

        public void AddBook(Book book)
        {
            var bookExists = books.Find(b => b.Equals(book));

            if (bookExists != null) bookExists.IncreaseQuantity();
            else books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            var existingBook = books.Find(b => b.Equals(book));

            if (existingBook != null)
            {
                try
                {
                    existingBook.DecreaseQuantity();
                    if (existingBook.IsOutOfStock())
                    {
                        books.Remove(existingBook);
                    }
                }
                catch (InvalidOperationException e)
                {
                    books.Remove(existingBook);
                }

            } else
            {
                Console.WriteLine($"Book not found in the Library");
            }
        }
        public Book FindBookById(int id)
        {
            return books.SingleOrDefault(book => book.getID() == id);
        }

        public Book FindBookByTitle(string title)
        {
            return books.SingleOrDefault(book => book.getTitle().Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public void DisplayBooks()
        {
            if (books.Count == 0) 
            {
                Console.WriteLine("No books");
                return;
            } 
            foreach (var book in books) 
            {
                Console.WriteLine(book.ToString());
            }
        }
    }
}
