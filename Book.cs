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
        NonFiction,
        Mystery,
        Fantasy,
        Romance,
        ScienceFiction,
        Biography,
        Horror,
     
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


        public Book(string title, string author, string? description, Genre bookGenre)
        {
            Id = ++_idCounter;
            Title = title;
            Author = author;
            Description = description;
            BookGenre = bookGenre;
            Quantity = 1;
        }
        public void IncreaseQuantity(int amount = 1)
        {
            Quantity += amount;
        }

        public void DecreaseQuantity(int amount = 1)
        {
            if (Quantity - amount < 0) throw new InvalidOperationException("Can't decrease quantity - It is already 0");
            else Quantity -= amount;
        }
        public Book(string title, string author, string? description, Genre bookGenre, int quantity) : this(title, author, description, bookGenre)
        {
            Quantity = quantity;
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
    }
}
