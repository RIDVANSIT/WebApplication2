using WebApplication2.Contexts;
using WebApplication2.Core;
using WebApplication2.Entities;
using WebApplication2.Repositories.Abstracts;

namespace WebApplication2.Repositories.Concretes;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ExampleDbContext context) : base(context)
    {
    }
}

