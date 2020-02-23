using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands
{
    public class CreateUserHandler : ICommandHandler<CreateUser, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdNumberValidationService _numberValidationService;

        public CreateUserHandler(IUserRepository userRepository, 
            IIdNumberValidationService numberValidationService)
        {
            _userRepository = userRepository;
            _numberValidationService = numberValidationService;
        }

        public async Task<bool> Handle(CreateUser request, CancellationToken cancellationToken = default)
        {
            var idNumberValid = await _numberValidationService.IsIdNumberValid(request.IdNumber);

            if (!idNumberValid)
            {
                throw new IdNumberValidationException();
            }

            var user = await _userRepository.AddAsync(new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdNumber = request.IdNumber,
            });

            return user != null;
        }
    }

    public class CreateUser : Command<bool>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
    }
}
