using PaymentAssignment.BAL.DTO;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAssignment.BAL.Services
{
    public interface IPaymentService
    {

        IQueryable<Payment>  GetAllPayments();
        Task<Payment> GetPaymentById(int id);
        Task<Payment> AddPayment(PaymentDto newPayment);
        void UpdatePayment(Payment payment);
    }
}
