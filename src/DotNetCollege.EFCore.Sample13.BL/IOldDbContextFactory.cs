using DotNetCollege.EFCore.Sample13.DAL;

namespace DotNetCollege.EFCore.Sample13.BL.Services
{
    public interface IOldDbContextFactory
    {
        AppDbContext GetContext();
    }
}
