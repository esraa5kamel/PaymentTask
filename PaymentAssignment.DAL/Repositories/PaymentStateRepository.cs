using Microsoft.EntityFrameworkCore;
using PaymentAssignment.DAL.DBContext;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAssignment.DAL.Repositories
{
    public class PaymentStateRepository : Repository<PaymentState>, IPaymentStateRepository
    {

        public PaymentStateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PaymentState> GetPaymentStateById(long id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.PaymentStateId == id);
        }
    }
}
