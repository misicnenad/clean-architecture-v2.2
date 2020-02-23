using MediatR;

using System;

namespace CleanArchitecture.Domain.Commands
{
    public abstract class Command<TResponse> : IRequest<TResponse>
    {
        public Guid CommandId { get; set; }
    }

    public interface ICommandHandler<in TRequest, TResponse>
        : IRequestHandler<TRequest, TResponse> where TRequest : Command<TResponse>
    {

    }
}
