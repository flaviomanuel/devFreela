using System.Text;
using System.Text.Json;
using DevFreela.Infrastructure.MessageBus;

namespace DevFreela.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
      
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }

        public void ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
         
           var paymentInfoJson= JsonSerializer.Serialize(paymentInfoDTO);

           var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

           _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
         
        }
    }


      public class PaymentInfoDTO
    {
        public PaymentInfoDTO()
        {
        }

        public PaymentInfoDTO(int idProject, string creditCardNumber, string cvv, string expiresAt, string fullName, decimal amount)
        {
            IdProject = idProject;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
            Amount = amount;
        }

        public int IdProject { get; private set; }
        public string CreditCardNumber { get; private set; }
        public string Cvv { get; private set; }
        public string ExpiresAt { get; private set; }
        public string FullName { get; private set; }
        public decimal Amount { get; private set; }
    }
}