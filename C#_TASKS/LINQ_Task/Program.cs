using LibraryManagementSystem;
using LINQ_DATA;
using System.ComponentModel;
namespace DEPI_LinQ_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Find All Available Books
            var availableBooks = LibraryData.Books.Where(book => book.IsAvailable == true);
            //availableBooks.ToConsoleTable();

            /* 2. Get All Book Titles#
            Write a LINQ query to get a list of all book titles.*/
            var bookTitles = LibraryData.Books.Select(b => new { Title = b.Title });
            //bookTitles.ToConsoleTable("Book Titles");

            /* 3. Find Books by Genre
            Write a LINQ query to find all books in the "Programming" genre.
            */
            var programmingBooks = LibraryData.Books.Where(b => b.Genre == "Programming");
            // programmingBooks.ToConsoleTable("Programming Books");

            /* 4. Sort Books by Title
              Write a LINQ query to sort all books alphabetically by title.*/
            var sortedbooks = LibraryData.Books.OrderBy(b => b.Title);
            //sortedbooks.ToConsoleTable("Sorted Books by Title");

            /* 5. Find Expensive Books#
            Write a LINQ query to find all books that cost more than $30.
            */
            var expensiveBooks = LibraryData.Books.Where(b => b.Price > 30);
            //expensiveBooks.ToConsoleTable("Expensive Books");

            /* 6. Get Unique Genres
            Write a LINQ query to get a list of all unique genres in the library.*/
            var uniqueGenres = LibraryData.Books.Select(b => new {Gener = b.Genre}).Distinct();
            //uniqueGenres.ToConsoleTable("Unique Genres");

            /* 7. Count Books by Genre
            Write a LINQ query to count how many books are in *each* genre.
            */
            var booksByGenre = LibraryData.Books.GroupBy(b => b.Genre)
                .Select(g => new { Genre = g.Key, Count = g.Count() });
            //booksByGenre.ToConsoleTable("Books by Genre");

            /* 8. Find Recent Books
            Write a LINQ query to find all books published after 2010.*/
            var recentBooks = LibraryData.Books.Where(b => b.PublishedYear > 2010);
            //recentBooks.ToConsoleTable("Recent Books");

            /* 9. Get First 5 Books
            Write a LINQ query to get the first 5 books from the collection.*/
            var first5Books = LibraryData.Books.Take(5);
            //first5Books.ToConsoleTable("First 5 Books");

            /* 10. Check if Any Expensive Books Exist
            Write a LINQ query to check if there are any books priced over $50.*/
            var anyExpensiveBooks = LibraryData.Books.Any(b => b.Price > 50);
            //Console.WriteLine($"Are there any books priced over $50 ? {anyExpensiveBooks}");

            /* 11. Books with Author Information
            Write a LINQ query to join books with their authors and return book title, author name, and genre.*/
            var booksWithAuthors = from book in LibraryData.Books
                                   join author in LibraryData.Authors
                                   on book.AuthorId equals author.Id
                                   select new
                                   {
                                       BookTitle = book.Title,
                                       AuthorName = author.Name,
                                       Genre = book.Genre
                                   };
            //booksWithAuthors.ToConsoleTable("Books with Author Information");

            /* 12. Average Price by Genre
            Write a LINQ query to calculate the average price of books for each genre.*/
            var avgPriceByGenre = LibraryData.Books
                .GroupBy(b => b.Genre)
                .Select(g => new { Genre = g.Key, AveragePrice = g.Average(b => b.Price) });
            //avgPriceByGenre.ToConsoleTable("Average Price by Genre");

            /* 13. Most Expensive Book
            Write a LINQ query to find the most expensive book in the library.*/
            var mostExpensiveBook = LibraryData.Books
                .OrderByDescending(b => b.Price).FirstOrDefault();
               
            //Console.WriteLine("Most Expensive Book:");

            /* 14. Group Books by Published Decade
            Write a LINQ query to group books by the decade they were published (1990s, 2000s, 2010s, etc.).*/

            var booksByDecade = LibraryData.Books
                .GroupBy(b => (b.PublishedYear / 10) * 10)
                .Select(g => new { Decade = g.Key, Books = g.ToList() });
            //foreach (var group in booksByDecade)
            //{
            //    Console.WriteLine($"Decade: {group.Decade}s");
            //    group.Books.ToConsoleTable();
            //}


            /* 15. Members with Active Loans
            Write a LINQ query to find all members who have active loans (books not yet returned).*/

            var membersWithActiveLoans = from member in LibraryData.Members
                                         join loan in LibraryData.Loans
                                         on member.Id equals loan.MemberId
                                         where loan.ReturnDate == null
                                         select new
                                         {
                                             MemberName = member.FullName,
                                             LoanedBookId = loan.BookId,
                                             LoanDate = loan.LoanDate
                                         };
            //membersWithActiveLoans.ToConsoleTable("Members with Active Loans");


            /* 16. Books Borrowed More Than Once
            Write a LINQ query to find books that have been borrowed more than once.*/

            var booksBorrowedMoreThanOnce = LibraryData.Loans
                .GroupBy(loan => loan.BookId)
                .Where(g => g.Count() > 1)
                .Select(g => new { BookId = g.Key, BorrowCount = g.Count() });
            //booksBorrowedMoreThanOnce.ToConsoleTable("Books Borrowed More Than Once");

            /* 17. Overdue Books
            Write a LINQ query to find all overdue books (books with due dates in the past that haven't been returned).*/
            var overdueBooks = from loan in LibraryData.Loans
                               join book in LibraryData.Books
                               on loan.BookId equals book.Id
                               where loan.DueDate < DateTime.Now && loan.ReturnDate == null
                               select new
                               {
                                   BookTitle = book.Title,
                                   DueDate = loan.DueDate,
                                   LoanDate = loan.LoanDate
                               };
            //overdueBooks.ToConsoleTable("Overdue Books");


            /* 18. Author Book Counts
            Write a LINQ query to find how many books each author has written, sorted by book count descending.*/

            var authorBookCounts = LibraryData.Books
                .GroupBy(b => b.AuthorId)
                .Join(LibraryData.Authors,
                        g => g.Key,
                        a => a.Id,
                        (g, a) => new { Author = a.Name, BookCount = g.Count() })
                .OrderByDescending(x => x.BookCount);
                                              

            //authorBookCounts.ToConsoleTable("Author Book Counts");

            /* 19. Price Range Analysis#
            Write a LINQ query to categorize books into price ranges (Cheap: $20, Medium: $20-$40, Expensive: $40) 
            and count books in each range.*/

            var priceRangeAnalysis = LibraryData.Books
                .GroupBy(b => b.Price < 20 ? "Cheap" : b.Price <= 40 ? "Medium" : "Expensive")
                .Select(g => new { PriceRange = g.Key, BookCount = g.Count() });
            //priceRangeAnalysis.ToConsoleTable("Price Range Analysis");

            /* 20. Member Loan Statistics#
            Write a LINQ query to calculate loan statistics for each member: total loans, active loans, 
            and average days borrowed.*/
            var memberLoanStatistics = LibraryData.Members
                .GroupJoin(LibraryData.Loans,
                           m => m.Id,
                           l => l.MemberId,
                           (m, loans) => new
                           {
                               MemberName = m.FullName,
                               TotalLoans = loans.Count(),
                               ActiveLoans = loans.Count(l => l.ReturnDate == null),
                               AverageDaysBorrowed = loans.Where(l => l.ReturnDate != null)
                                                          .Select(l => (l.ReturnDate.Value - l.LoanDate).TotalDays)
                                                          .DefaultIfEmpty(0)
                                                          .Average()
                           });
            //memberLoanStatistics.ToConsoleTable("Member Loan Statistics");





        }
    }
}
