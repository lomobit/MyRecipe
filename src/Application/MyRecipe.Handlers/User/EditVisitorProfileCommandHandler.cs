using MediatR;
using MyRecipe.AppServices.User;
using MyRecipe.Handlers.Contracts.User;

namespace MyRecipe.Handlers.User;

public class EditVisitorProfileCommandHandler : IRequestHandler<EditVisitorProfileCommand, bool>
{
    private readonly IUserService _userService;

    public EditVisitorProfileCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(EditVisitorProfileCommand command, CancellationToken cancellationToken)
    {
        return await _userService.EditVisitorProfile(command, cancellationToken);
    }
}