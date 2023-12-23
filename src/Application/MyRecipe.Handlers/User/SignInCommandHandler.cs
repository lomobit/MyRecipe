using MediatR;
using MyRecipe.AppServices.User;
using MyRecipe.Handlers.Contracts.User;

namespace MyRecipe.Handlers.User;

public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
{
    private readonly IUserService _userService;

    public SignInCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userService.GetUserAsync();
        
        // Generate JWT
        
        // Return JWT
        
        
        throw new NotImplementedException();
    }
}