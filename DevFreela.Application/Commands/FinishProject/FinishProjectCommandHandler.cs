using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;
        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService; 
        }
        public  async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetDetailsByIdAsync(request.Id);

            var paymentInfoDTO = new PaymentInfoDTO(){
                Id = request.Id,
                CreditCardNumber = request.CreditCardNumber,
                Cvv = request.Cvv,
                ExpiresAt = request.ExpiresAt,
                FullName = request.FullName
            };

             _paymentService.ProcessPayment(paymentInfoDTO);

             project.SetPaymentPending();

            await _projectRepository.SaveChangesAsync();

            return true;
        }
    }
}
