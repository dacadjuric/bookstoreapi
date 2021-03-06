using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class UnauthorizedUseCaseException : Exception
    {
        public UnauthorizedUseCaseException(IUseCase useCase, IApplicatioActor actor)
           : base($"Author with id of {actor.Id} --- {actor.Identity} tried to execute {useCase.Name}.")
        {

        }
    }
}
