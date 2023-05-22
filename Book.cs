using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public enum Genre
    {
        Fiction,
        Horror,
        Mystery,
        Thriller,
    }
    public class Book
    {
        private static int _idCounter = 0;
        private int Id { get; }
        private string Title{ get; }
        private string Author { get; }
        private string? Description { get; }
        private Genre BookGenre { get; set; }
        private int Quantity { get; set; }

        public Book()
        {
            //Deserialize method needs default constructor
        }

        public Book(string title, string author, Genre bookGenre)
        {
            this.Title = title;
            this.Author = author;
            this.BookGenre = bookGenre;
            this.Quantity = 1;
            this.Id = ++_idCounter;
        }
        public Book(string title, string author, Genre bookGenre, int quantity)
        {
            this.Title = title;
            this.Author = author;
            this.BookGenre = bookGenre;
            this.Quantity = quantity;
            this.Id = ++_idCounter;
        }

        public void IncreaseQuantity(int amount = 1)
        {
            Quantity += amount;
        }

        public void DecreaseQuantity(int amount = 1)
        {
            if (Quantity - amount < 0) throw new InvalidOperationException("Can't decrease quantity - It is already 0");
            else
            {
                Quantity -= amount;
                if (Quantity == 0)
                {
                    Console.WriteLine($"Book {Title} is out of Stock");
                }
            };
        }

        public bool IsOutOfStock ()
        {
            return Quantity == 0;   
        }

        public Book FindByTitle(string title)
        {
            if (title == Title) return this;
            else return null;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Book)) return false;
            var toCompare = obj as Book;
            return toCompare.Title == this.Title && toCompare.Author == Author;
        }

        public override string? ToString()
        {
            return $"ID: {Id}, Title: {Title}, Author: {Author}, Genre: {BookGenre}, Quantity: {Quantity}"; 
        }

        public string getTitle()
        {
            return Title;
        }

        public int getID()
        {
            return Id;
        }
    }
}
