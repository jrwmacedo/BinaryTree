using FluentValidation;
using GameAPI.Domain.Entities;
using MediatR;

namespace GameAPI.Data.Request.PlayerRequest
{
	public class CreatePlayer : IRequest<Player>
	{
		public string FirstName { get; set; }
	}

	public class PlayerValidator : AbstractValidator<CreatePlayer>
	{
		public PlayerValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().NotNull();
		}
	}
}
