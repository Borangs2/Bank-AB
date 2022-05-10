using AutoMapper;
using Bank_AB.Data;
using Bank_AB.Infrastructure.Profiles;
using Bank_AB.Services.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTests.Services.Customers
{
    [TestClass]
    public class CustomerServiceTests
    {
        private static IMapper _mapper;

        private readonly CustomerService _sut; //system under test
        private readonly ApplicationDbContext _context;

        public CustomerServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Bank_AB")
                .Options;
            _context = new ApplicationDbContext(options);

            _sut = new CustomerService(_context, _mapper);

            var data = new TestDataInitilizer(_context);
            data.SeedData();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new CustomerProfile()); });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [TestMethod]
        public void Get_customer_from_id()
        {
            var result = _sut.GetCustomerFromId(1);
            Assert.AreNotEqual(result, null);
        }


        [TestMethod]
        public void Get_customer_from_invalid_id()
        {
            var result = _sut.GetCustomerFromId(-4);
            Assert.AreEqual(result, null);
        }


    }
}
