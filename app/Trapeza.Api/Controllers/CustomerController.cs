using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Trapeza.Core.Consts;
using Trapeza.Core.Model;
using Trapeza.Core.Services;
using Trapeza.Core.Services.Options;
using Trapeza.Core.Services.Interfaces;

namespace Trapeza.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customer;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customer)
        {
            _logger = logger;
            _customer = customer;
        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetCustomerById(int id)
        //{
        //    var customer = await _customer.GetCustomerByIdAsync(id);

        //    return Json(customer);
        //}

        //[HttpPost]
        //public async Task<IActionResult> PostRegisterCustomer([FromBody] RegisterCustomerOptions options)
        //{
        //    var result = await _customer.RegisterCustomerAsync(options);

        //    if (result.Code != ResultCodes.Success)
        //    {
        //        return StatusCode(result.Code, result.Message);
        //    }

        //    return Json(result);
        //}
    }
}
