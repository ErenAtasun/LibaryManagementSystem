using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LibaryManagement
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Copies { get; set; }
        public int Borrowed { get; set; }

        

    }
    public class Library
    {
        private List<Book> books;

        public Library()
        {
            books = new List<Book>();
        }
        public void BookAdd(Book book)
        {
            books.Add(book);
            
        }
        public void AllBooksList()
        {
            Console.WriteLine("All books in library");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Title} by {book.Author} - ISBN: {book.ISBN} - Copies Available: {book.Copies - book.Borrowed}");

            }
        }
        public void BookSearch(string key)
        {
            var searchResult = books.Where(book => book.Title.Contains(key) || book.Author.Contains(key)).ToList();
            if (searchResult.Count > 0)
            {
                Console.WriteLine("Search Results:");
                foreach (var book in searchResult)
                {
                    Console.WriteLine($"{book.Title} by {book.Author} - ISBN: {book.ISBN} - Copies Available: {book.Copies - book.Borrowed}");
                }
            }
            else
            {
                Console.WriteLine("No matching books found.");
            }
        }
        public void BookBorrow(string isbn)
        {
            var book = books.FirstOrDefault(b => b.ISBN == isbn && b.Copies > b.Borrowed);
            if (book != null && book.Copies > 0 && book.Borrowed < book.Copies)
            {
                book.Copies--;
                Console.WriteLine($"Successfully borrowed '{book.Title}' by {book.Author}.");
            }
            else
            {
                Console.WriteLine("The book could not be borrowed. There are not enough copies in stock.");
            }
        }
        public void ReturnBook(string isbn)
        {
            var book = books.FirstOrDefault(b => b.ISBN == isbn && b.Borrowed > 0);
            if (book != null)
            {
                book.Borrowed++;
                Console.WriteLine($"Successfully returned '{book.Title}' by {book.Author}.");
            }
            else
            {
                Console.WriteLine("Invalid ISBN or the book is not borrowed.");
            }
        }
        public void ExpiredBooks()
        {
            Console.WriteLine("ExpiredBooks");
            foreach(var book in books)
            {
                if (book.Borrowed > 0)
                {
                    Console.WriteLine($"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}, Copies: {book.Borrowed}, Borrowed ");
                }
            }
        }
        public void SaveDataToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Title},{book.Author},{book.ISBN},{book.Copies},{book.Borrowed}");
                }
            }
        }
        public void LoadDataFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                books.Clear();
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var data = line.Split(',');
                        var book = new Book
                        {
                            Title = data[0],
                            Author = data[1],
                            ISBN = data[2],
                            Copies = int.Parse(data[3]),
                            Borrowed = int.Parse(data[4])
                        };
                        books.Add(book);
                    }
                }
            }
        }

    }

    class Program
    {
        static void Main()
        {
            Library library = new Library();
            string dataFileName = "library_data.txt";


            library.LoadDataFromFile(dataFileName);
            while (true)
            {
                Console.WriteLine("\nLibrary Control System");
                Console.WriteLine("1. Book Add");
                Console.WriteLine("2. All Books List");
                Console.WriteLine("3. Book Search");
                Console.WriteLine("4. Borrow a Book");
                Console.WriteLine("5. Return Book ");
                Console.WriteLine("6. List Expired Books");
                Console.WriteLine("7. Exit");

                Console.WriteLine("Make Your Choice");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        Console.Write("Author");
                        string author = Console.ReadLine();
                        Console.Write("ISBN: ");
                        string isbn = Console.ReadLine();
                        Console.Write("Copy: ");
                        int copies = int.Parse(Console.ReadLine());

                        Book newBook = new Book
                        {
                            Title = title,
                            Author = author,
                            ISBN = isbn,
                            Copies = copies,
                            Borrowed = 0
                        };

                        library.BookAdd(newBook);
                        Console.WriteLine("Book added successfully.");
                        break;

                    case "2":
                        library.AllBooksList();
                        break;


                    case "3":
                        Console.Write("Search key (title and Author):");
                        string key = Console.ReadLine();
                        library.BookSearch(key);
                        break;
                    case "4":
                        Console.Write("ISBN: ");
                        string barrowIsbn = Console.ReadLine();
                        library.BookBorrow(barrowIsbn);
                        break;
                    case "5":
                        Console.Write("ISBN: ");
                        string returnIsbn = Console.ReadLine();
                        library.ReturnBook(returnIsbn);
                        break;
                    case "6":
                        library.ExpiredBooks();
                        break;
                    case "7":
                        library.SaveDataToFile(dataFileName);
                        Console.WriteLine("Signing out");
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;

                }



            }
        }
    }


}