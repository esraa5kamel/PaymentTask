using PaymentAssignment.BAL.DTO;
using PaymentAssignment.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentAssignment.BAL.GateWay
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {

        // here we should have the business logic of the payment Processes based on some of the conditions but I 
        //will use a Fake condition only for generate different Results
        public PaymentStateEnum PaymentProcessCheap(PaymentDto payment)
        {
            //Fake Condition 
            if (payment.Amount % 5 == 0)
            {
              return PaymentStateEnum.Failed;
            }
            else
            {
                return  PaymentStateEnum.Processed;
            }                     
        }
    }
}
