using PaymentAssignment.BAL.DTO;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAssignment.BAL.GateWay
{
   public interface ICheapPaymentGateway
    {
        PaymentStateEnum PaymentProcessCheap(PaymentDto payment);
    }
}
