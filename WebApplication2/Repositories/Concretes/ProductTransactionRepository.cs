using System;
using WebApplication2.Contexts;
using WebApplication2.Core;
using WebApplication2.Entities;
using WebApplication2.Repositories.Abstracts;

namespace WebApplication2.Repositories.Concretes;

public class ProductTransactionRepository : BaseRepository<ProductTransaction>, IProductTransactionRepository
{
    public ProductTransactionRepository(ExampleDbContext context) : base(context)
    {
    }
}

