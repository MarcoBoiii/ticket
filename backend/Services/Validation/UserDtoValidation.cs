using FluentValidation;
using Services.Dto;

namespace Services.Validation;

public class UserDtoLoginValidator : AbstractValidator<UserDto>
{
    public UserDtoLoginValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email darf nicht leer sein");

        When(user => user.Email != string.Empty, () => {
            RuleFor(user => user.Email).EmailAddress().WithMessage("Ungültige Email-Adresse");
        });

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Passwort darf nicht leer sein");

        When(user => user.Password != string.Empty, () => {
            RuleFor(user => user.Password).MinimumLength(6).WithMessage("Passwort muss mindestens 6 Zeichen lang sein");
        });

    }
}

public class UserDtoRegisterValidator : AbstractValidator<UserDto>
{
    public UserDtoRegisterValidator()
    {
        RuleFor(user => user.FirstName)
                    .NotEmpty().WithMessage("Vorname darf nicht leer sein");

        RuleFor(user => user.LastName)
            .NotEmpty().WithMessage("Nachname darf nicht leer sein");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email darf nicht leer sein");

        When(user => user.Email != string.Empty, () => {
            RuleFor(user => user.Email).EmailAddress().WithMessage("Ungültige Email-Adresse");
        });

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Passwort darf nicht leer sein");

        When(user => user.Password != string.Empty, () => {
            RuleFor(user => user.Password).MinimumLength(6).WithMessage("Passwort muss mindestens 6 Zeichen lang sein");
        });

        RuleFor(user => user.ConfirmPassword)
            .NotEmpty().WithMessage("Passwort bestätigen darf nicht leer sein")
            .Equal(user => user.Password).WithMessage("Passwörter müssen übereinstimmen");
    }
}



