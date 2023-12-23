namespace MyRecipe.Infrastructure.Repositories.Login;

public class LoginRepository : ILoginRepository
{
    private readonly MyRecipeDbContext _context;

    public LoginRepository(MyRecipeDbContext context)
    {
        _context = context;
    }
}