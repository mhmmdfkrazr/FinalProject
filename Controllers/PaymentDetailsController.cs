using System;
using System.Collections.Generic;
using System.Linq;
using PaymentApp.Data;
using PaymentApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaymentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PaymentDetailsController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentDetails()
        {
            var items = await _context.PaymentDetail.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentDetail(int id)
        {
            var item = await _context.PaymentDetail.FirstOrDefaultAsync(x => x.paymentDetailId == id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> PostPaymentDetails(PaymentItem paymentDetail)
        {
            if (!ModelState.IsValid) return new JsonResult("Something Went Wrong") { StatusCode = 500 };
            await _context.PaymentDetail.AddAsync(paymentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetails", new { paymentDetail.paymentDetailId }, paymentDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentDetail(int id, PaymentItem paymentDetail)
        {
            if (id != paymentDetail.paymentDetailId)
                return BadRequest();

            var existItem = await _context.PaymentDetail.FirstOrDefaultAsync(x => x.paymentDetailId == id);

            if (existItem == null)
                return NotFound();

            existItem.CardOwnerName = paymentDetail.CardOwnerName;
            existItem.CardNumber = paymentDetail.CardNumber;
            existItem.ExpirationDate = paymentDetail.ExpirationDate;
            existItem.SecurityCode = paymentDetail.SecurityCode;

            await _context.SaveChangesAsync();
            return new JsonResult("Update Success") { StatusCode = 200 };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentsDetails(int id)
        {
            var existItem = await _context.PaymentDetail.FirstOrDefaultAsync(x => x.paymentDetailId == id);
            if (existItem == null)
                return NotFound();
            _context.PaymentDetail.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}
