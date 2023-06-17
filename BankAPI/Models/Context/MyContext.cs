using BankAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Models.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CardInfo cardInfo = new CardInfo()
            {
                ID = 1,
                CardUserName = "Tuğberk Mehdioğlu",
                CardNumber = "1111 1111 1111 1111",
                CardExpiryYear = 2023,
                CardExpiryMounth = 12,
                SecurityNumber = "222",
                Limit = 500000m,
                Balance = 500000m
            };

            modelBuilder.Entity<CardInfo>().HasData(cardInfo);
        }

        public DbSet<CardInfo>? Cards { get; set; }
    }
}
