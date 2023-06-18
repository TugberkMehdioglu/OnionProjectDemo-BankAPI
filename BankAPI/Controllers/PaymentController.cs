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
        public async Task<IActionResult> ReceivePayment(PaymentDTO item)
        {
            //Todo: 2021-05-10 , 2.56.46

            CardInfo? cardInfo = await _context.Cards!.FirstOrDefaultAsync(x => x.CardUserName == item.CardUserName && x.SecurityNumber == item.SecurityNumber && x.CardNumber == item.CardNumber && x.CardExpiryYear == item.CardExpiryYear && x.CardExpiryMounth == item.CardExpiryMounth);

            if (cardInfo != null)
            {
                if (cardInfo.CardExpiryYear < DateTime.Now.Year) return BadRequest("Expired Card");

                else if (cardInfo.CardExpiryYear == DateTime.Now.Year)
                {
                    if (cardInfo.CardExpiryMounth < DateTime.Now.Month) return BadRequest("Expired Card");

                    if (cardInfo.Balance >= item.ShoppingPrice) 
                    {
                        cardInfo.Balance -= item.ShoppingPrice;
                        await _context.SaveChangesAsync();

                        return Ok();
                    } 
                    else return BadRequest("Balance Exceeded");
                }

                if (cardInfo.Balance >= item.ShoppingPrice) 
                {
                    cardInfo.Balance -= item.ShoppingPrice;
                    await _context.SaveChangesAsync();

                    return Ok();
                } 
                else return BadRequest("Balance Exceeded");
            }
            else return BadRequest("Card Not Found");
        }

        //Middleware'de builder.Services.AddCors(); ve app.UseCors(builder => { builder.AllowAnyOrigin(); builder.AllowAnyMethod(); builder.AllowAnyHeader(); }); koymayı unutma.
        //BackEnd'den API'a request atmak için: WebApiRestService.WebApiClient kütüphanesini indir diyo .NetFramework'te hoca. Fakat .NetCore'da buna ihtiyaç yok.
    }
}
