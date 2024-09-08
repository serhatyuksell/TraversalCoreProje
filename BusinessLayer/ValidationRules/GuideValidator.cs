using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class GuideValidator:AbstractValidator<Guide>
    {
        public GuideValidator() { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Rehber adını giriniz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Rehber açıklamasını giriniz");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Rehber görselini giriniz");
            RuleFor(x => x.Name).MaximumLength(30).WithMessage("Rehber adı en fazla 30 karakter olmalıdır");
            RuleFor(x => x.Name).MinimumLength(8).WithMessage("Rehber adı en az 8 karakter olmalıdır");
            
        }
    }
}
