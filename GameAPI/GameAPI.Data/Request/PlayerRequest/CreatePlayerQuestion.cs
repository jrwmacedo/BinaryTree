using FluentValidation;
using GameAPI.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace GameAPI.Data.Request.PlayerRequest
{
    public class CreatePlayerQuestion : IRequest<IList<PlayerQuestion>>
    {
        public int Id { get; set; }
        public int[] Answers { get; set; }
    }

    public class CreatePlayerQuestionValidator : AbstractValidator<CreatePlayerQuestion>
    {
        public CreatePlayerQuestionValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Answers).NotEmpty().NotNull();
        }
    }
}
