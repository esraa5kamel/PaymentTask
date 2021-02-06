using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentAssignment.BAL.DTO;
using PaymentAssignment.BAL.Services;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult ProcessPayment(PaymentDto entite)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var PaymentResult = _paymentService.AddPayment(entite);

                    if (PaymentResult.Result.PaymentStates.FirstOrDefault().State != PaymentStateEnum.Processed.ToString())
                    {
                        return StatusCode(500, new { error = "This Payment can not be processed" });

                    }
                    return Ok(PaymentResult);
                }
                else
                    return StatusCode(400);
            }
            catch 
            {
                return StatusCode(500);
            }

        }

            
    }
}
