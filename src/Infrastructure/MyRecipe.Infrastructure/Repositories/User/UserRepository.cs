namespace MyRecipe.Infrastructure.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly MyRecipeDbContext _context;

    public UserRepository(MyRecipeDbContext context)
    {
        _context = context;
    }
}