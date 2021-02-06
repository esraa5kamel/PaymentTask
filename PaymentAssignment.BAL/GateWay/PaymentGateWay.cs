using AutoMapper;
using PaymentAssignment.BAL.DTO;
using PaymentAssignment.DAL.Entity;
using PaymentAssignment.DAL.Repositories;
using PaymentAssignment.DAL.UOW;


namespace PaymentAssignment.BAL.GateWay
{
    public class PaymentGateWay : IPaymentGateway
    {
        private readonly IPaymentStateRepository _paymentStateRepository;
        private readonly IUnitOfWork _UOW;
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        public PaymentGateWay(IPaymentStateRepository paymentStateRepository, IUnitOfWork UWO,
                              ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway)
        {
            _paymentStateRepository = paymentStateRepository;
            _UOW = UWO;
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
        }

        public PaymentState PaymentProcess(PaymentDto payment, PaymentState paymentState)
        {           
            if (payment.Amount <= 20)
                {
                    paymentState.State = _cheapPaymentGateway.PaymentProcessCheap(payment).ToString();
                }
            else if (payment.Amount > 20 && payment.Amount <= 500)
                {
                    paymentState.State = _expensivePaymentGateway.PaymentProcessExpensive(payment).ToString();
                    if (paymentState.State == PaymentStateEnum.Failed.ToString())
                    {
                        paymentState.State = _cheapPaymentGateway.PaymentProcessCheap(payment).ToString();                  
                    }
                }
            else
                {
                    int count = 0;
                    while (count < 3)
                    {
                        count++;
                        paymentState.State = _expensivePaymentGateway.PaymentProcessExpensive(payment).ToString();
                        if (paymentState.State == PaymentStateEnum.Processed.ToString())
                            {
                                break;
                            }
                    }
                }

            PaymentStateUpdate(paymentState);
            return paymentState;

        }

        public void PaymentStateUpdate(PaymentState paymentState)
        {
            _paymentStateRepository.Update(paymentState);
            _UOW.Complete();
        }

    }
}
