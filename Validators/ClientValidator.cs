using FluentValidation;
using ASP.NET_Projekt_Wypozyczalnia.Models;

namespace ASP.NET_Projekt_Wypozyczalnia.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Imię jest wymagane.")
                .Length(1, 50).WithMessage("Imię nie może być dłuższe niż 50 znaków.")
                .Matches(@"^[a-zA-ZąęłńóśźżĄĘŁŃÓŚŹŻ]+$").WithMessage("Imię może zawierać tylko litery.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko jest wymagane.")
                .Length(1, 50).WithMessage("Nazwisko nie może być dłuższe niż 50 znaków.")
                .Matches(@"^[a-zA-ZąęłńóśźżĄĘŁŃÓŚŹŻ]+$").WithMessage("Nazwisko może zawierać tylko litery.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail jest wymagany.")
                .Length(1, 50).WithMessage("E-mail nie może być dłuższy niż 50 znaków.")
                .EmailAddress().WithMessage("Nieprawidłowy format adresu e-mail.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Numer telefonu jest wymagany.")
                .Matches(@"^\+?\d{9,12}$").WithMessage("Numer telefonu musi mieć od 9 do 12 cyfr i może zaczynać się od '+'.");

            RuleFor(x => x.DocumentNumber)
                .NotEmpty().WithMessage("Numer dokumentu jest wymagany.")
                .Length(1, 20).WithMessage("Numer dokumentu nie może być dłuższy niż 20 znaków.")
                .When(x => x.DocumentType == DocumentType.ID)
                .Matches(@"^[A-Z]{3}\d{6}$").WithMessage("Numer dowodu osobistego powinien mieć format ABC123456.")
                .When(x => x.DocumentType == DocumentType.DriverLicence)
                .Matches(@"^\d{5}\/\d{2}\/\d{4}$").WithMessage("Numer prawa jazdy powinien mieć format 12345/12/2023.");

            RuleFor(x => x.DocumentType)
                .IsInEnum().WithMessage("Nieprawidłowy typ dokumentu.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres jest wymagany.")
                .Length(1, 100).WithMessage("Adres nie może być dłuższy niż 100 znaków.");
        }
    }
}