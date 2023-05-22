using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Program
    {
        private const string FileName = "LibraryData.json";
        static async Task Main(string[] args)
        {

            Library library = await Library.LoadLibraryDataAsync(FileName);
            

            Console.WriteLine("\nWelcome to the Library");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Remove a book");
            Console.WriteLine("3. Find a book");
            Console.WriteLine("4. Display all books");
            Console.WriteLine("5. Instructions");
            Console.WriteLine("6. Exit");

            library.SaveLibraryDataAsync();
            while (true)
            {
                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter book details:");
                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Author: ");
                        string author = Console.ReadLine();
                        Console.Write("Genre (1 = Fantasy, 2 = Horror, 3 = Mystery, 4 = Thriller): ");
                        string genreInput = Console.ReadLine();
                        Genre genre;

                        bool parsed = Enum.TryParse(genreInput, out genre);
                        if (!parsed)
                        {
                            Console.WriteLine("Invalid input for genre. Please try again.");
                            continue;
                        }
                        Book newBook = new Book(title, author, genre);
                        library.AddBook(newBook);
                        Console.WriteLine($"Book {title} added successfully.");
                        await library.SaveLibraryDataAsync();
                        break;
                    case 2:
                        Console.WriteLine("Enter book details to remove:");
                        Console.Write("Title: ");
                        string titleToRemove = Console.ReadLine();
                        Console.Write("Author: ");
                        string authorToRemove = Console.ReadLine();
                        Console.Write("Genre (1 = Fantasy, 2 = Horror, 3 = Mystery, 4 = Thriller): ");
                        Genre genreToRemove = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine());

                        Book bookToRemove = new Book(titleToRemove, authorToRemove, genreToRemove);
                        library.RemoveBook(bookToRemove);
                        await library.SaveLibraryDataAsync();
                        break;
                    case 3:
                        Console.WriteLine("Enter 1 to find by title, 2 to find by ID: ");
                        int findOption;
                        bool success = int.TryParse(Console.ReadLine(), out findOption);
                        if (!success)
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                            break;
                        }

                        if (findOption == 1)
                        {
                            Console.Write("Enter the title of the book to find: ");
                            string titleToFind = Console.ReadLine();
                            Book foundBook = library.FindBookByTitle(titleToFind);
                            if (foundBook != null)
                            {
                                Console.WriteLine($"Book found: {foundBook.ToString()}");
                            }
                            else
                            {
                                Console.WriteLine($"Book with title {titleToFind} not found.");
                            }
                        }
                        else if (findOption == 2)
                        {
                            Console.Write("Enter the ID of the book to find: ");
                            int idToFind;
                            success = int.TryParse(Console.ReadLine(), out idToFind);
                            if (!success)
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                                break;
                            }
                            Book foundBook = library.FindBookById(idToFind);
                            if (foundBook != null)
                            {
                                Console.WriteLine($"Book found: {foundBook.ToString()}");
                            }
                            else
                            {
                                Console.WriteLine($"Book with ID {idToFind} not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                        break;
                    case 4:

                        library.DisplayBooks();
                        break;
                    case 5:
                        Console.WriteLine("Please select an option:");
                        Console.WriteLine("1. Add a book");
                        Console.WriteLine("2. Remove a book");
                        Console.WriteLine("3. Find a book");
                        Console.WriteLine("4. Display all books");
                        Console.WriteLine("5. Instructions");
                        Console.WriteLine("6. Exit");
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        return;
                        break;
                }
            }
        }
    }
}
