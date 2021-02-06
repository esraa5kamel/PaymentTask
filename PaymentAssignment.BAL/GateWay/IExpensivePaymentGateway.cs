using PaymentAssignment.BAL.DTO;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAssignment.BAL.GateWay
{
    public interface IExpensivePaymentGateway
    {
        PaymentStateEnum PaymentProcessExpensive(PaymentDto payment);
    }
}
