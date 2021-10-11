using AngularCrud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            return await _context.PaymentDetails.FirstOrDefaultAsync(x => x.PaymentDetailId == id);
        }

        // PUT: api/PaymentDetail/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
            var paymentDetailFromDb = await _context.PaymentDetails.FirstOrDefaultAsync(x => x.PaymentDetailId == id);

            if (paymentDetailFromDb == null)
                return NotFound("Not Found");

            paymentDetailFromDb.CardNumber = paymentDetail.CardNumber;
            paymentDetailFromDb.CardOwnerName = paymentDetail.CardOwnerName;
            paymentDetailFromDb.ExpirationDate = paymentDetail.ExpirationDate;
            paymentDetailFromDb.SecurityCode = paymentDetail.SecurityCode;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/PaymentDetail
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FirstOrDefaultAsync(x => x.PaymentDetailId == id);

            if (paymentDetail == null)
                return NotFound();

            _context.PaymentDetails.Remove(paymentDetail);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PaymentDetailExists(int id)
        { 
          var paymentDetail= _context.PaymentDetails.FirstOrDefault(x => x.PaymentDetailId == id);

            if (paymentDetail == null)
                return false;
            else
                return true;
        }
    }
}
