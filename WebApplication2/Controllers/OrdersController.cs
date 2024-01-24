using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Entities;
using WebApplication2.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repositories.Concretes;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers;

[Route("api/[controller]")] 
public class OrdersController : Controller
{
    private IOrderRepository _orderRepository;
    private IProductRepository _productRepository;

    public OrdersController(IOrderRepository userRepository)
    {
        _orderRepository = userRepository;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_orderRepository.GetAll());
    }

    [HttpGet("GetById/{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_orderRepository.Get(order => order.Id == id));
    }

    [HttpPost("Add")]
    public IActionResult Add([FromBody] Order order)
    {
       
        var product = _productRepository.Get(p => p.Id == order.ProductId);

        if (product == null)
        {
            return BadRequest("Product not found");
        }

        
        if (product.StockAmount < order.Quantity)
        {
            return BadRequest("Insufficient product quantity");
        }

        
        product.StockAmount -= order.Quantity;
        _productRepository.Update(product);

       
        _orderRepository.Add(order);

        return Ok(order);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] Order order)
    {
        return Ok(_orderRepository.Update(order));
    }

    [HttpDelete("DeleteById/{id}")]
    public IActionResult Delete(Guid id)
    {
        var order = _orderRepository.Get(order => order.Id == id);
        if (order == null) return BadRequest("User not found");
        return Ok(_orderRepository.Delete(order));
    }
}
