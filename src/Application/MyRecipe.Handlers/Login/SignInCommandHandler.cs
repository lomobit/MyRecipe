using MediatR;
using MyRecipe.Handlers.Contracts.Login;

namespace MyRecipe.Handlers.Login;

public class SignInCommandHandler : IRequestHandler<SignInCommand, string>
{
    public async Task<string> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        // Get user
        
        // Generate JWT
        
        // Return JWT
        
        
        throw new NotImplementedException();
    }
}