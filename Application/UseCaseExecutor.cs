using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class UseCaseExecutor
    {
        private readonly IApplicatioActor _actor;
        private readonly IUseCaseLogger _logger;

        public UseCaseExecutor(IApplicatioActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }

        public TResult ExecuteQuery<TSearch, TResult>(IQuery<TSearch, TResult> query, TSearch search)
        {

            _logger.Log(query, _actor, search);

            if (!_actor.UseCases.Contains(query.Id))
            {
                throw new UnauthorizedUseCaseException(query, _actor);
            }

            return query.Execute(search);
        }

        public void ExecuteCommand<TRequest>(
            ICommand<TRequest> command,
            TRequest request)
        {
            _logger.Log(command, _actor, request);

            if (!_actor.UseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseException(command, _actor);
            }

            command.Execute(request);

        }
    }
}
