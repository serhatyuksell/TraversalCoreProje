using DTOLayer.DTOs.AnnouncementDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
	public class AnnouncementValidator:AbstractValidator<AnnouncementAddDTO>
	{
		public AnnouncementValidator()
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş geçilemez");
			RuleFor(x => x.Content).NotEmpty().WithMessage("Content boş geçilemez");
			RuleFor(x => x.Title).MinimumLength(5).WithMessage("Title en az 3 karakter olmalıdır");
			RuleFor(x => x.Content).MinimumLength(20).WithMessage("Content en az 20 karakter olmalıdır");
			RuleFor(x => x.Title).MaximumLength(50).WithMessage("Title en fazla 50 karakter olmalıdır");
			RuleFor(x => x.Content).MaximumLength(500).WithMessage("Title en fazla 500 karakter olmalıdır");
		}

	}
}
