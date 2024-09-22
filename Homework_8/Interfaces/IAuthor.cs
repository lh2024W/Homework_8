﻿using Homework_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_8.Interfaces
{
    public interface IAuthor
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorWhithBooksAsync(int id);
        Task<Author> GetAuthorAsync(int id);
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string name);

        Task AddAuthorAsync(Author author);
        Task DeleteAuthorAsync(Author author);
        Task EditAuthorAsync(Author author);
    }
}
