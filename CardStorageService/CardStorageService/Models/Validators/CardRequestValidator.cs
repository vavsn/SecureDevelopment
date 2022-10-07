using CardStorageService.Models.Requests;
using FluentValidation;

namespace CardStorageService.Models.Validators
{
    public class CardRequestValidator : AbstractValidator<CreateCardRequest>
    {
        public CardRequestValidator()
        {
            RuleFor(x => x.ClientId)
                .NotNull()
                    .WithMessage("Идентификатор пользователя не может быть пустым!")
                .GreaterThan(0)
                    .WithMessage("Идентификатор пользователя не может быть меньше 0");

            RuleFor(x => x.CVV2)
                .NotNull()
                    .WithMessage("Код CVV не может быть пустым!")
                .Length(1, 3)
                    .WithMessage("Код CVV может содержать от 1 до 3 символов");
        }
    }
}
