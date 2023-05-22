using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 

namespace Library
{
    internal class Library
    {
        private List<Book> books;
        public string Filename { get; private set; }
        public Library(string filename)
        {
            this.books = new List<Book>();
            Filename = filename;
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
        
        public async Task SaveLibraryDataAsync()
        {
            var jsonData = JsonConvert.SerializeObject(this.books);
            await File.WriteAllTextAsync(Filename, jsonData);
        }

        public static async Task<Library> LoadLibraryDataAsync(string filename)
        {
            Library library = new Library(filename);
            if (!File.Exists(filename))
            {
                return library;
            }

            var jsonData = await File.ReadAllTextAsync(filename);
            
            try
            {
                library.books = JsonConvert.DeserializeObject<List<Book>>(jsonData);    
            }
            catch(JsonException e)
            {
                Console.WriteLine("Error parsing data: " + e.Message);
                
            }
            return library;
        }

    }
}
