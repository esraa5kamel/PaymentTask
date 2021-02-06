using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAssignment.DAL.Repositories
{
    public interface IPaymentRepository: IRepository<Payment>
    {
        //If we have any customes Function/Transaction related to Payment entity we shoud put it here
        Task<Payment> GetPaymentById(long id);
    }
}
