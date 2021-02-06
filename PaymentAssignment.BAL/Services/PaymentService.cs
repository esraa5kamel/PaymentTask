using AutoMapper;
using PaymentAssignment.BAL.DTO;
using PaymentAssignment.BAL.GateWay;
using PaymentAssignment.DAL.Entity;
using PaymentAssignment.DAL.Repositories;
using PaymentAssignment.DAL.UOW;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAssignment.BAL.Services
{
   public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentStateRepository _paymentStateRepository;
        private readonly IUnitOfWork _UOW;
        private readonly IMapper _mapper;
        private readonly IPaymentGateway _paymentGateway;


        public PaymentService(IPaymentRepository paymentRepository, IPaymentStateRepository paymentStateRepository, IUnitOfWork UWO , IMapper mapper, IPaymentGateway paymentGateway)
        {
            _paymentRepository = paymentRepository;
            _paymentStateRepository = paymentStateRepository;
            _UOW = UWO;
            _mapper = mapper;
            _paymentGateway = paymentGateway;
        }

        public IQueryable<Payment> GetAllPayments()
        {
            return  _paymentRepository.GetAll();
        }
        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepository.GetPaymentById(id);
        }

        public async Task<Payment> AddPayment(PaymentDto payment)
        {
            try
            {
                var paymentEntity = _mapper.Map<PaymentDto, Payment>(payment);
                paymentEntity = _paymentRepository.AddAsync(paymentEntity).Result;
                PaymentState paymentStateData = new PaymentState() { Payment = paymentEntity, PaymentId = paymentEntity.PaymentId, CreatedDate = DateTime.Now, State = PaymentStateEnum.Pending.ToString() };
                var paymentState = await _paymentStateRepository.AddAsync(paymentStateData);
                _UOW.Complete();

                _paymentGateway.PaymentProcess(payment, paymentState);
                return paymentEntity;
            }
            catch (Exception )
            {
                throw new Exception("This payment can not be processed");
            }
        }

        public void UpdatePayment(Payment payment)
        {
             _paymentRepository.Update(payment);

        }
    }
}