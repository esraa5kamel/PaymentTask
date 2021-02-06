using PaymentAssignment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAssignment.DAL.UOW
{
   public interface IUnitOfWork : IDisposable
    {
        IPaymentRepository payments { get; }
        int Complete();
    }
}
