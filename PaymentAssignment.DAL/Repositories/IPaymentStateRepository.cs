using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAssignment.DAL.Repositories
{
   public interface IPaymentStateRepository :IRepository<PaymentState>
    {

        Task<PaymentState> GetPaymentStateById(long id);

    }
}
