using Bank_AB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTests.Services.Users;

[TestClass]
internal class UserServiceTests
{
    private readonly ApplicationDbContext _context;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("Bank_AB")
            .Options;
        _context = new ApplicationDbContext(options);
    }
}