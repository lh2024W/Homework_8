using Homework_8.Data;
using Homework_8.Helpers;
using Homework_8.Interfaces;
using Homework_8.Repository;

namespace Homework_8
{
    public partial class Program
    {
        public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
        private static IBook _books;
        private static IAuthor _authors;
        private static ICategory _categories;
        private static IOrder _orders;

        enum ShopMenu
        {
            Books, Authors, Categories, Orders, SearchAuthors, SearchBooks, SearchCategories, SearchOrders, AddBook, AddAuthor, AddCategory, AddOrder, Exit
        }

        static async Task Main()
        {
            Initialize();

            int input = ConsoleHelper.MultipleChoice(true, new ShopMenu());

            do
            {
                input = ConsoleHelper.MultipleChoice(true, new ShopMenu());

                switch ((ShopMenu)input)
                {
                    case ShopMenu.Books:
                        await ReviewBooks();
                        break;
                    case ShopMenu.Authors:
                        await ReviewAuthors();
                        break;
                    case ShopMenu.Categories:
                        await ReviewCategories();
                        break;
                    case ShopMenu.Orders:
                        await ReviewOrders();
                        break;
                    case ShopMenu.SearchAuthors:
                        await SearchAuthors();
                        break;
                    case ShopMenu.SearchBooks:
                        await SearchBooks();
                        break;
                    case ShopMenu.SearchCategories:
                        await SearchCategory();
                        break;
                    case ShopMenu.SearchOrders:
                        await SearchOrder();
                        break;
                    case ShopMenu.AddBook:
                        await AddBook();
                        break;
                    case ShopMenu.AddAuthor:
                        await AddAuthor();
                        break;
                    case ShopMenu.AddCategory:
                        await AddCategory();
                        break;
                    case ShopMenu.AddOrder:
                        await AddOrder();
                        break;
                    case ShopMenu.Exit:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadLine();
            } while (ShopMenu.Exit != (ShopMenu)input);
        }

        static void Initialize()
        {
            new DbInit().Init(DbContext());
            _books = new BookRepository();
            _authors = new AuthorRepository();
            _categories = new CategoryRepository();
            _orders = new OrderRepository();
        }
    }
}
