using System.ComponentModel;
using Class03_Homework.Models;

namespace Class03_Homework
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book
            {
                Author = "Leo Tolstoy",
                Title = "War and Peace"
            },
            new Book
            {
                Author = "Mark Twain",
                Title = "Adventures of Huckleberry Finn"
            },
            new Book
            {
                Author = "F. Scott Fitzgerald",
                Title = "The Great Gatsby"
            }
        };
    }
}
