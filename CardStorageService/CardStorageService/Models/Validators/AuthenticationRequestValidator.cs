using CardStorageService.Models.Requests;
using FluentValidation;

namespace CardStorageService.Models.Validators
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Login)
                .NotNull()
                    .WithMessage("Логин не может быть пустым!")
                .Length(5, 255)
                    .WithMessage("Не верная длина логина! Логин должен содержать от 5 до 255 символов")
                .EmailAddress()
                    .WithMessage("Введенный логин не является email-адресом!");


            RuleFor(x => x.Password)
                .NotNull()
                    .WithMessage("Пароль не может быть пустым!")
                .Length(5, 50)
                    .WithMessage("Не верная длина пароля! Пароль должен содержать от 5 до 50 символов");

        }
    }
}
