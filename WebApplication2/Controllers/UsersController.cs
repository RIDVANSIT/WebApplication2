using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Entities;
using WebApplication2.Repositories.Abstracts;
using WebApplication2.Repositories.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KimlikDogrulama;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers;

[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountTransactionRepository _accountTransactionRepository;

    public UsersController(
        IUserRepository userRepository,
        IAccountTransactionRepository accountTransactionRepository)
    {
        _userRepository = userRepository;
        _accountTransactionRepository = accountTransactionRepository;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_userRepository.GetAll());
    }
    [HttpGet("GetAllWithBalanceTransactions")]
    public IActionResult GetAllWithBalanceTransactions()
    {
        return Ok(_userRepository.GetAll(
            include:user=>user.Include(u=>u.AccountTransactions)
            ));
    }
    [HttpGet("GetAllWithOrders")]
    public IActionResult GetAllWithOrders()
    {
        return Ok(_userRepository.GetAll(
            include: user => user
                    .Include(u => u.Orders).ThenInclude(o=>o.OrderDetails).ThenInclude(od=>od.ProductTransaction)
                    .Include(u=>u.Orders).ThenInclude(o=>o.OrderDetails).ThenInclude(od=>od.Product).ThenInclude(p=>p.Category)
            ));
    }
    [HttpGet("GetAllWithAllDetails")]
    public IActionResult GetAllWithAllDetails()
    {
        return Ok(_userRepository.GetAll(
            include: user => user
                    .Include(u => u.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(od => od.ProductTransaction)
                    .Include(u => u.Orders).ThenInclude(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Category)
                    .Include(u=>u.AccountTransactions)
            ));
    }

    [HttpGet("GetById/{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_userRepository.Get(user=>user.Id==id));
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] User user) 
    {
        using (KPSPublicSoapClient soapClient = new KPSPublicSoapClient(
            KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap12))
        {
            long tcno = long.Parse(user.IdentificationNumber) ;
            var result = await soapClient.TCKimlikNoDogrulaAsync(
                tcno, user.FirstName, user.LastName, user.BirthYear);
                if(result.Body.TCKimlikNoDogrulaResult) {
                return Ok(_userRepository.Add(user));
            }
            else
            {
                throw new Exception("hatalı kimlik bilgileri");
            }
        }
        
            
    }
    [HttpPost("AddBalance")]
    public IActionResult Add([FromBody] AccountTransaction accountTransaction)
    {
        return Ok(_accountTransactionRepository.Add(accountTransaction));
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] User user)
    {
        return Ok(_userRepository.Update(user));
    }

    [HttpDelete("DeleteById/{id}")]
    public IActionResult Delete(Guid id)
    {
        var user = _userRepository.Get(user => user.Id == id);
        if (user == null) return BadRequest("User not found");
        return Ok(_userRepository.Delete(user));
    }
}

