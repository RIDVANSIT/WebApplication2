using System;
using WebApplication2.Entities;

namespace WebApplication2.DTOs;

public class AddOrderDto
{
    public Guid UserId { get; set; }
    public IList<ProductTransaction> ProductTransactions { get; set; }
    public AddOrderDto()
    {
        ProductTransactions = new List<ProductTransaction>();
    }
}

