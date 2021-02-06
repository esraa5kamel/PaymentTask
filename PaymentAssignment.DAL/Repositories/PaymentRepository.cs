using Microsoft.EntityFrameworkCore;
using PaymentAssignment.DAL.DBContext;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAssignment.DAL.Repositories
{
   public class PaymentRepository: Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Payment> GetPaymentById(long id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.PaymentId == id);
        }
    }
}