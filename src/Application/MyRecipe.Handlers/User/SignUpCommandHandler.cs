using MediatR;
using MyRecipe.AppServices.User;
using MyRecipe.Handlers.Contracts.User;

namespace MyRecipe.Handlers.User;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, bool>
{
    private readonly IUserService _userService;

    public SignUpCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        return await _userService.UserRegistration(command, cancellationToken);
    }
}