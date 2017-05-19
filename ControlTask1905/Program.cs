using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTask1905
{
    class HighOrderComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x / 10 > y / 10)
                return 1;
            if (x / 10 < y / 10)
                return -1;
            else
                return 0;
        }
    }

    class LowOrderComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x % 10 > y % 10)
                return 1;
            if (x % 10 < y % 10)
                return -1;
            else
                return 0;
        }
    }

    static class GetUniqueBooks
    {
        public static List<T> Unique<T>(this List<T> books)
        {
            //books.Distinct(new UniqueBooksEqualityComparer());
            return books;
        }
    }

    class UniqueBooksEqualityComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            return (x.Name == y.Name && x.Year == y.Year);
        }

        public int GetHashCode(Book book)
        {
            return book.Name.GetHashCode() + book.Year.GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Control task 1905\n");
            List<Author> authors = new List<Author>
            {
                new Author { Name = "Author1", AuthorID =1 },
                new Author { Name = "Author2", AuthorID =2 },
                new Author { Name = "Author3", AuthorID =3 },
                new Author { Name = "Author4", AuthorID =4 },
            };
            List<Book> books = new List<Book>()
            {
                new Book { Name = "Book1", Year = 1996, AuthorID =1 },
                new Book { Name = "Book2", Year = 1977, AuthorID =2 },
                new Book { Name = "Book3", Year = 2015, AuthorID =4 },
                new Book { Name = "LINQ", Year = 1992, AuthorID =2 },
                new Book { Name = "Book5", Year = 1941, AuthorID =2 },
            };

            //------------------------------- TASK 1--------------------------
            var query1 = books.Where(x =>
                                    x.Name == "LINQ" &&
                                    (x.Year % 4 == 0 && x.Year % 100 != 0) ||
                                    (x.Year % 400 == 0))
                                    .Select(x => $"Book: {x.Name}; year: {x.Year.ToString()}");

            Console.WriteLine("\n----------------  TASK 1 -------------------");
            Console.WriteLine(string.Join("\n", query1));


            //------------------------------- TASK 2--------------------------
            var query2 = authors.GroupJoin(books,
                                            a => a.AuthorID,
                                            b => b.AuthorID,
                                            (a, b) => $"Author {a.Name} has written {b.Count().ToString()} books");

            Console.WriteLine("\n----------------  TASK 2 -------------------");
            Console.WriteLine(string.Join("\n", query2));


            //------------------------------- TASK 3--------------------------
            int[] randomArr = { 22, 17, 99, 46, 11, 36, 65, 78, 63, 81, 24 };
            var query3 = randomArr.OrderBy(x => x, new HighOrderComparer()).OrderByDescending(x => x, new LowOrderComparer());
            Console.WriteLine("\n----------------  TASK 3 -------------------");
            Console.WriteLine(string.Join(", ", randomArr));
            Console.WriteLine(string.Join(", ", query3));


            //------------------------------- TASK 4--------------------------
            List<Book> booksUnique = new List<Book>()
            {
                new Book { Name = "Book1", Year = 1996, AuthorID =1 },
                new Book { Name = "Book2", Year = 1977, AuthorID =2 },
                new Book { Name = "Book3", Year = 2015, AuthorID =4 },
                new Book { Name = "LINQ", Year = 1992, AuthorID =2 },
                new Book { Name = "Book2", Year = 1977, AuthorID =2 },
            };
            Console.WriteLine("\n----------------  TASK 4 -------------------");
            var query4 = booksUnique.Distinct(new UniqueBooksEqualityComparer()).Select(x => $"{x.Name} - {x.Year}");
            Console.WriteLine(string.Join("\n", query4));

            Console.ReadLine();
        }
    }
}
