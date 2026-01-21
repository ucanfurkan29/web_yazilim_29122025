using _12_fluentvalidation.Models;
using FluentValidation;

namespace _12_fluentvalidation.Validators
{
    public class OgrenciValidator : AbstractValidator<Ogrenci>
    {
        public OgrenciValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Ad alanı boş olamaz.")
                .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Yas)
                .InclusiveBetween(18, 60).WithMessage("Yaş 18 ile 60 arasında olmalıdır.");
        }
    }
}
