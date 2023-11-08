using FluentValidation;

namespace Proje1.Models.Validator
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator ()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName cannot be empty");
			RuleFor(x => x.UserMail).NotEmpty().WithMessage("Gmail cannot be empty");
			RuleFor(x => x.UserPhone).NotEmpty().WithMessage("Phone number cannot be empty");
		}
	}
}
