using Homework_8.Helpers;
using Homework_8.Interfaces;
using Homework_8.Migrations;
using Homework_8.Models;
using Homework_8.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8
{
    public partial class Program
    {
        //Authors

        static async Task ReviewAuthors()
        {
            var allAuthors = await _authors.GetAllAuthorsAsync();
            var authors = allAuthors.Select(e => new ItemView { Id = e.Id, Value = e.Name}).ToList();
            int result = ItemsHelper.MultipleChoice(true, authors, true);
            if (result != 0)
            {
                var currentAuthor = await _authors.GetAuthorAsync(result);
                await AuthorInfo(currentAuthor);
            }
        }

        static async Task AuthorInfo(Author currentAuthor)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView {Id = 1,  Value = "Browse books"},
                new ItemView { Id = 2, Value = "Edit author"},
                new ItemView { Id = 3, Value = "Delete author"}
            },
            IsMenu: true, message: String.Format("{0}\n", currentAuthor), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    {
                        await AuthorWithBooks(currentAuthor);
                        break;
                    }
                case 2:
                    {
                        await EditAuthor(currentAuthor);
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        await RemoveAuthor(currentAuthor);
                        Console.ReadLine();
                        break;
                    }
            }
            await ReviewAuthors();
        }

        static async Task AddAuthor()
        {
            string authorName = InputHelper.GetString("author 'Name' with 'Surname'");
            await _authors.AddAuthorAsync(new Author
            {
                Name = authorName
            });
            Console.WriteLine("Author successfully added.");
        }

        static async Task EditAuthor(Author currentAuthor)
        {
            Console.WriteLine("Changing: {0}", currentAuthor.Name);
            currentAuthor.Name = InputHelper.GetString("author 'Name' with 'SurName'");
            await _authors.EditAuthorAsync(currentAuthor);
            Console.WriteLine("Author successfully changed.");
        }

        static async Task RemoveAuthor(Author currentAuthor)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes"},
                new ItemView { Id = 2, Value = "No"},
            }, message: String.Format("{Are you sure you want to delete the author {0} ?}\n", currentAuthor.Name), startY: 2);

            if (result == 1)
            {
                await _authors.DeleteAuthorAsync(currentAuthor);
                Console.WriteLine("The author has been successfully deleted.");
            }
            else
            {
                Console.WriteLine("Press any key to conrinue...");
            }
        }

        static async Task SearchAuthors()
        {
            string authorName = InputHelper.GetString("author name or surname");
            var currentAuthors = await _authors.GetAuthorsByNameAsync(authorName);
            if (currentAuthors.Count() > 0)
            {
                var authors = currentAuthors.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
                int result = ItemsHelper.MultipleChoice(true, authors, true);
                if (result != 0)
                {
                    var currentAuthor = await _authors.GetAuthorAsync(result);
                    await AuthorInfo(currentAuthor);
                }
            }
            else
            {
                Console.WriteLine("No authors were found by this attribute.");
                }
        }

        static async Task AuthorWithBooks(Author currentAuthor)
        {
            var authors = await _authors.GetAuthorWhithBooksAsync(currentAuthor.Id);
            var authorWithBooks = authors.Books.Select(e => new ItemView { Id = e.Id, Value = e.Title }).ToList();
            int result = ItemsHelper.MultipleChoice(true, authorWithBooks, true);
            if (result != 0)
            {
                var currentAuthor1 = await _authors.GetAuthorAsync(result);
                await AuthorInfo(currentAuthor1);
            }
        }


            //Books

        static async Task ReviewBooks()
        {
            var allBooks = await _books.GetAllBooksAsync();
            var books = allBooks.Select(e => new ItemView { Id = e.Id, Value = e.Title }).ToList();
            int result = ItemsHelper.MultipleChoice(true, books, true);
            if (result != 0)
            {
                var currentBook = await _books.GetBookAsync(result);
                await BookInfo(currentBook);
            }
        }

        static async Task BookInfo(Book currentBook)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView {Id = 1,  Value = "Edit book"},
                new ItemView { Id = 2, Value = "Delete book"}
            },
            IsMenu: true, message: String.Format("{0}\n", currentBook), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    {
                        await EditBook(currentBook);
                        Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        await RemoveBook(currentBook);
                        Console.ReadLine();
                        break;
                    }
            }
            await ReviewBooks();
        }

        static async Task AddBook()
        {
            string bookTitle = InputHelper.GetString("book 'Title'");
            string bookDescription = InputHelper.GetString("book 'Description'");
            DateTime bookPublishedOn = InputHelper.GetDateTime("book 'PublishedOn'");
            string bookPublisher = InputHelper.GetString("book 'Publisher'");
            decimal bookPrice = InputHelper.GetDecimal("book 'Price'");
            int bookCategoryId = InputHelper.GetInt("book 'CategoryId'");
            string bookAuthor = InputHelper.GetString("author 'Name' with 'Surname'");
            
            await _books.AddBookAsync(new Book
            {
                Title = bookTitle,
                Description = bookDescription,
                PublishedOn = bookPublishedOn,
                Publisher = bookPublisher,
                Price = bookPrice,
                CategoryId = bookCategoryId,
                Authors = new List<Author>
                {
                    new Author { Name = bookAuthor }
                }
            });
            Console.WriteLine("Book successfully added.");
        }

        static async Task EditBook(Book currentBook)
        {
            Console.WriteLine("Changing: {0}", currentBook.Price);
            currentBook.Price = InputHelper.GetDecimal("book 'Price'");
            await _books.EditBookAsync(currentBook);
            Console.WriteLine("Book successfully changed.");
        }

        static async Task RemoveBook(Book currentBook)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes"},
                new ItemView { Id = 2, Value = "No"},
            }, message: String.Format("{Are you sure you want to delete the book {0} ?}\n", currentBook.Title), startY: 2);

            if (result == 1)
            {
                await _books.DeleteBookAsync(currentBook);
                Console.WriteLine("The book has been successfully deleted.");
            }
            else
            {
                Console.WriteLine("Press any key to conrinue...");
            }
        }

        static async Task SearchBooks()
        {
            string bookTitle = InputHelper.GetString("book title");
            var currentBooks = await _books.GetBooksByNameAsync(bookTitle);
            if (currentBooks.Count() > 0)
            {
                var books = currentBooks.Select(e => new ItemView { Id = e.Id, Value = e.Title }).ToList();
                int result = ItemsHelper.MultipleChoice(true, books, true);
                if (result != 0)
                {
                    var currentBook = await _books.GetBookAsync(result);
                    await BookInfo(currentBook);
                }
            }
            else
            {
                Console.WriteLine("No books were found by this title.");
            }
        }


        //Categories

        static async Task ReviewCategories()
        {
            var allCategories = await _categories.GetAllCategoriesAsync();
            var categories = allCategories.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
            int result = ItemsHelper.MultipleChoice(true, categories, true);
            if (result != 0)
            {
                var currentCategory = await _categories.GetCategoryAsync(result);
                await CategoryInfo(currentCategory);
            }
        }

        static async Task CategoryInfo(Category currentCategory)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView {Id = 1,  Value = "Browse categories"},
                new ItemView {Id = 2,  Value = "Edit category"},
                new ItemView { Id = 3, Value = "Delete category"}
            },
            IsMenu: true, message: String.Format("{0}\n", currentCategory), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    {
                        await CategoryWithBooks(currentCategory);
                        Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        await EditCategory(currentCategory);
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        await RemoveCategory(currentCategory);
                        Console.ReadLine();
                        break;
                    }
            }
            await ReviewCategories();
        }

        static async Task AddCategory()
        {
            string categoryName = InputHelper.GetString("category 'Name'");
            string categoryDescription = InputHelper.GetString("category 'Description'");
            
            await _categories.AddCategoryAsync(new Category
            {
                Name = categoryName,
                Description = categoryDescription
            });
            Console.WriteLine("Category successfully added.");
        }

        static async Task EditCategory(Category currentCategory)
        {
            Console.WriteLine("Changing: {0}", currentCategory.Name);
            currentCategory.Name = InputHelper.GetString("category 'Name'");
            await _categories.UpdateCategoryAsync(currentCategory);
            Console.WriteLine("Category successfully changed.");
        }

        static async Task RemoveCategory(Category currentCategory)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes"},
                new ItemView { Id = 2, Value = "No"},
            }, message: String.Format("{Are you sure you want to delete the category {0} ?}\n", currentCategory.Name), startY: 2);

            if (result == 1)
            {
                await _categories.DeleteCategoryAsync(currentCategory);
                Console.WriteLine("The category has been successfully deleted.");
            }
            else
            {
                Console.WriteLine("Press any key to conrinue...");
            }
        }

        static async Task SearchCategory()
        {
            string categoryName = InputHelper.GetString("category name");
            var currentCategories = await _categories.GetCategoriesByNameAsync(categoryName);
            if (currentCategories.Count() > 0)
            {
                var categories = currentCategories.Select(e => new ItemView { Id = e.Id, Value = e.Name }).ToList();
                int result = ItemsHelper.MultipleChoice(true, categories, true);
                if (result != 0)
                {
                    var currentCategory = await _categories.GetCategoryAsync(result);
                    await CategoryInfo(currentCategory);
                }
            }
            else
            {
                Console.WriteLine("No categories were found by this name.");
            }
        }

        static async Task CategoryWithBooks(Category currentCategory)
        {
            var category = await _categories.GetCategoryWithBooksAsync(currentCategory.Id);
            var categoryWithBooks = category.Books.Select(e => new ItemView { Id = e.Id, Value = e.Title }).ToList();
            int result = ItemsHelper.MultipleChoice(true, categoryWithBooks, true);
            if (result != 0)
            {
                var currentCategory1 = await _categories.GetCategoryAsync(result);
                await CategoryInfo(currentCategory1);
            }
        }
        

        //Orders

        static async Task ReviewOrders()
        {
            var allOrders = await _orders.GetAllOrdersAsync();
            var orders = allOrders.Select(e => new ItemView { Id = e.Id, Value = e.CustomerName }).ToList();
            int result = ItemsHelper.MultipleChoice(true, orders, true);
            if (result != 0)
            {
                var currentOrder = await _orders.GetOrderAsync(result);
                await OrderInfo(currentOrder);
            }
        }

        static async Task OrderInfo(Order currentOrder)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView {Id = 1,  Value = "Edit order"},
                new ItemView { Id = 2, Value = "Delete order"}
            },
            IsMenu: true, message: String.Format("{0}\n", currentOrder), startY: 5, optionsPerLine: 1);

            switch (result)
            {
                case 1:
                    {
                        await EditOrder(currentOrder);
                        Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        await RemoveOrder(currentOrder);
                        Console.ReadLine();
                        break;
                    }
            }
            await ReviewOrders();
        }

        static async Task AddOrder()
        {
            string orderCustomerName = InputHelper.GetString("customer 'Name' with 'Surname'");
            string orderCity = InputHelper.GetString("order 'City'");
            string orderAddress = InputHelper.GetString("address 'Street' with 'Number house'");
            bool orderShipped = InputHelper.GetBoolean("order shipped 'true' or 'false'");
            int orderBookCount = InputHelper.GetInt("count");
            int orderBookId = InputHelper.GetInt("book 'Id'");

            await _orders.AddOrderAsync(new Order
            {
                CustomerName = orderCustomerName,
                City = orderCity,
                Address = orderAddress,
                Shipped = orderShipped,
                Lines = new List <OrderLine> { new OrderLine { Quantity = orderBookCount, BookId = orderBookId } }
            });
            Console.WriteLine("Order successfully added.");
        }

        static async Task EditOrder(Order currentOrder)
        {
            Console.WriteLine("Changing: {0}", currentOrder.CustomerName);
            currentOrder.CustomerName = InputHelper.GetString("customer 'Name' with 'Surname'");
            await _orders.UpdateOrderAsync(currentOrder);
            Console.WriteLine("Order successfully changed.");
        }

        static async Task RemoveOrder(Order currentOrder)
        {
            int result = ItemsHelper.MultipleChoice(true, new List<ItemView>
            {
                new ItemView { Id = 1, Value = "Yes"},
                new ItemView { Id = 2, Value = "No"},
            }, message: String.Format("{Are you sure you want to delete the order {0} ?}\n", currentOrder.CustomerName), startY: 2);

            if (result == 1)
            {
                await _orders.DeleteOrderAsync(currentOrder);
                Console.WriteLine("The order has been successfully deleted.");
            }
            else
            {
                Console.WriteLine("Press any key to conrinue...");
            }
        }

        static async Task SearchOrder()
        {
            string orderCustomerName = InputHelper.GetString("customer 'Name' with 'Surname'");
            var currentOrders = await _orders.GetAllOrdersByNameAsync(orderCustomerName);
            if (currentOrders.Count() > 0)
            {
                var orders = currentOrders.Select(e => new ItemView { Id = e.Id, Value = e.CustomerName }).ToList();
                int result = ItemsHelper.MultipleChoice(true, orders, true);
                if (result != 0)
                {
                    var currentOrder = await _orders.GetOrderAsync(result);
                    await OrderInfo(currentOrder);
                }
            }
            else
            {
                Console.WriteLine("No order were found by this name.");
            }
        }
    }
}
