using DevFreela.Application.Commands.CreateProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Tamanho máximo de Descricão é de 255 caracteres");

            RuleFor(x => x.Title)
               .MaximumLength(30)
               .WithMessage("Tamanho máximo de Title é de 30 caracteres");
        }
        
    }
}
