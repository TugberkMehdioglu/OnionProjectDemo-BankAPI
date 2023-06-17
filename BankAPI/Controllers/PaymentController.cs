using BankAPI.DTOClasses;
using BankAPI.Models.Context;
using BankAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        /*
         * 200 => Onay var, hiçbir sorun yok, herşey yolunda.
         * 400 => Client'dan kaynaklanan hatalar (Action ismini yanlış yazıp request atarsan vs.).
         * 500 => Server'dan kaynaklanan sorunlar (Db çökerse vs.).
         */

        private readonly MyContext _context;

        public PaymentController(MyContext context)
        {
            _context = context;
        }


        [HttpGet]
        //For testing
        public async Task<ObjectResult> GetAll()
        {
            //I trigger the db by EnsureCreated because i dont want use the migrations
            //_context.Database.EnsureCreated();


            PaymentDTO[] cards = await _context.Cards!.Select(x => new PaymentDTO()
            {
                ID = x.ID,
                CardUserName = x.CardUserName,
                SecurityNumber = x.SecurityNumber,
                CardNumber = x.CardNumber,
                CardExpiryYear = x.CardExpiryYear,
                CardExpiryMounth = x.CardExpiryMounth
            }).ToArrayAsync();

            return Ok(cards);
        }

        //Buraya gelicek request, bana bilgi göndereceği için POST ile request yapıcak.
        [HttpPost]
        public ObjectResult ReceivePayment(PaymentDTO item)
        {
            return Ok(new { an = "anan" });
        }
    }
}
