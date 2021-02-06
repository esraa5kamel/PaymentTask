using PaymentAssignment.BAL.DTO;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAssignment.BAL.GateWay
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {

        // here we should have the business logic of the payment Processes based on some of the conditions but I 
       // will use a Fake condition only for generate different Results

        public PaymentStateEnum PaymentProcessExpensive(PaymentDto payment)
        {
            //Fake Condtions
            if (payment.Amount%5 ==0)
            {
                 return PaymentStateEnum.Failed;
            }
            else
            {
                return PaymentStateEnum.Processed;
            }

        }
    }
}
