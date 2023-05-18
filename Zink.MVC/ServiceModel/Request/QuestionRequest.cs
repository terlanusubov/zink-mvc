using FluentValidation;

namespace Zink.MVC.ServiceModel.Request
{
    public class QuestionRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }

    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x => x.Name)
                .Matches(@"^[A-Z][a-zA-Z '.-]*[A-Za-z][^-]$").WithMessage("Ad düzgün daxil edilməyib.")
                .NotEmpty().WithMessage("Ad xanası boş ola bilməz.");
            RuleFor(x => x.Surname)
                .Matches(@"^[A-Z][a-zA-Z '.-]*[A-Za-z][^-]$").WithMessage("Soyad düzgün daxil edilməyib.")
                .NotEmpty().WithMessage("Soyad xanası boş ola bilməz.");
            RuleFor(x => x.PhoneNumber)
                .Matches(@"[+]994(40|5[015]|60|7[07])\d{7}$").WithMessage("Nömrə formata uyğun deyil.")
                .NotEmpty().WithMessage("Nömrə xanası boş ola bilməz.");
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().Matches(@"/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/").MaximumLength(3).WithMessage("Email düzgün daxil edilməyib.");
        }
    }
}
