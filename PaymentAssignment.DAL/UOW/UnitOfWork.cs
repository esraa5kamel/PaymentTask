using PaymentAssignment.DAL.DBContext;
using PaymentAssignment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAssignment.DAL.UOW
{
   public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext Db_context;
        public IPaymentRepository payments { get; }

        public UnitOfWork(ApplicationDbContext db_contect,
            IPaymentRepository payments)
        {
            this.Db_context = db_contect;
            this.payments = payments;
        }
        public int Complete()
        {
            return Db_context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db_context.Dispose();
            }
        }
    }
}
