
using AutoMapper;
using Moq;
using NUnit.Framework;
using PaymentAssignment.BAL.DTO;
using PaymentAssignment.BAL.GateWay;
using PaymentAssignment.DAL.Entity;
using PaymentAssignment.DAL.Repositories;
using PaymentAssignment.DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAssignment.UnitTest
{
    public class GateWayTest
    {
        IPaymentGateway _paymentGateWay;
        Mock<IUnitOfWork> _Uow;
        Mock<IPaymentStateRepository> _paymentStateRepository;
        Mock<IExpensivePaymentGateway> _expensivePaymentGateway;
        Mock<ICheapPaymentGateway> _cheapPaymentGateway;

        [SetUp]
        public void Setup()
        {
            _cheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            _expensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            _Uow = new Mock<IUnitOfWork>();
            _paymentStateRepository = new Mock<IPaymentStateRepository>();
            _paymentGateWay = new PaymentGateWay( _paymentStateRepository.Object, _Uow.Object, _cheapPaymentGateway.Object, _expensivePaymentGateway.Object);
        }


        [Test]
        [TestCaseSource(typeof(GateWayTestCases), nameof(GateWayTestCases.TestCheap))]
        public void PaymentProcessCheapGateWay(PaymentDto paymentDto, PaymentState paymentState, PaymentStateEnum state)
        {    
            //arrange
            _cheapPaymentGateway.Setup(x => x.PaymentProcessCheap(paymentDto)).Returns(state);

            //act
            var paymentStateResult = _paymentGateWay.PaymentProcess(paymentDto, paymentState);

            //assert
            _cheapPaymentGateway.Verify(x => x.PaymentProcessCheap(paymentDto), Times.Once());
            Assert.AreEqual(paymentStateResult.State, state.ToString());

        }


        [Test]
        [TestCaseSource(typeof(GateWayTestCases), nameof(GateWayTestCases.TestExpensive))]
        public void ExpensiveProcessCheapGateWay(PaymentDto paymentDto, PaymentState paymentState, PaymentStateEnum state,int callExpensive,int callCheap, PaymentStateEnum CheapState, PaymentStateEnum Expacted)
        {
            //arrange
            _expensivePaymentGateway.Setup(x => x.PaymentProcessExpensive(paymentDto)).Returns(state);
            _cheapPaymentGateway.Setup(s => s.PaymentProcessCheap(paymentDto)).Returns(CheapState);

            //act
            var paymentStateResult = _paymentGateWay.PaymentProcess(paymentDto, paymentState);

            //assert
            _expensivePaymentGateway.Verify(x => x.PaymentProcessExpensive(paymentDto), Times.Exactly(callExpensive));
            _cheapPaymentGateway.Verify(x => x.PaymentProcessCheap(paymentDto), Times.Exactly(callCheap));
            Assert.AreEqual(paymentStateResult.State, Expacted.ToString());

        }


        [Test]
        [TestCaseSource(typeof(GateWayTestCases), nameof(GateWayTestCases.TestPremium))]
        public void PaymentProcessPremium(PaymentDto paymentDto, PaymentState paymentState, PaymentStateEnum state)
        {
            //arrange
            _expensivePaymentGateway.Setup(x => x.PaymentProcessExpensive(paymentDto)).Returns(state);

            //act
            var paymentStateResult = _paymentGateWay.PaymentProcess(paymentDto, paymentState);

            //assert
            if(state == PaymentStateEnum.Failed)
            {
                _expensivePaymentGateway.Verify(x => x.PaymentProcessExpensive(paymentDto), Times.Exactly(3));
            }
            else
            {
                _expensivePaymentGateway.Verify(x => x.PaymentProcessExpensive(paymentDto), Times.Once);
            }
            Assert.AreEqual(paymentStateResult.State,state.ToString());

        }
    }






        public static class GateWayTestCases
        {
            public static PaymentState paymentState = new PaymentState() { State = PaymentStateEnum.Pending.ToString(), CreatedDate = DateTime.Now };

            public static PaymentDto PaymentLessThanTwentyDto = new PaymentDto() { Amount = 12, CardHolder = "Esraa Kamel", CreditCardNumber = "4111111111111111", ExpirationDate = DateTime.Now, SecurityCode = "123" };
            public static PaymentDto PaymentMoreThanTwentyDto = new PaymentDto() { Amount = 122, CardHolder = "Esraa Kamel", CreditCardNumber = "4111111111111111", ExpirationDate = DateTime.Now, SecurityCode = "123" };
            public static PaymentDto PaymentMoreThanFiveHundredPoundsDto = new PaymentDto() { Amount = 520, CardHolder = "Esraa Kamel", CreditCardNumber = "4111111111111111", ExpirationDate = DateTime.Now, SecurityCode = "123" };

            public static PaymentStateEnum FailedPaymentState = PaymentStateEnum.Failed;
            public static PaymentStateEnum ProcessedPaymentState = PaymentStateEnum.Processed;

            public static IEnumerable<TestCaseData> TestCheap
            {
                get
                {
                    yield return new TestCaseData(PaymentLessThanTwentyDto, paymentState, ProcessedPaymentState).SetName("Cheap_Processed");
                    yield return new TestCaseData(PaymentLessThanTwentyDto, paymentState, FailedPaymentState).SetName("Cheap_Failed");
                }
            }

            public static IEnumerable<TestCaseData> TestExpensive
            {
                get
                {
                    yield return new TestCaseData(PaymentMoreThanTwentyDto, paymentState, ProcessedPaymentState,1,0, ProcessedPaymentState, ProcessedPaymentState).SetName("Expensive_Processed");
                    yield return new TestCaseData(PaymentMoreThanTwentyDto, paymentState, FailedPaymentState,1,1, FailedPaymentState, FailedPaymentState).SetName("ExpensiveFailed_CheapFailed");
                    yield return new TestCaseData(PaymentMoreThanTwentyDto, paymentState, FailedPaymentState, 1, 1, ProcessedPaymentState, ProcessedPaymentState).SetName("ExpensiveFailed_CheapProcessed");
                }
            }

            public static IEnumerable<TestCaseData> TestPremium
            {
                get
                {
                    yield return new TestCaseData(PaymentMoreThanFiveHundredPoundsDto, paymentState, ProcessedPaymentState).SetName("Premium_Processed");
                    yield return new TestCaseData(PaymentMoreThanFiveHundredPoundsDto, paymentState, FailedPaymentState).SetName(" Premium_Failed");
                }
            }
    }
 }


