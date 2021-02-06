using Microsoft.EntityFrameworkCore;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAssignment.DAL.DBContext
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }


        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentState> PaymentState { get; set; }

    }
}
