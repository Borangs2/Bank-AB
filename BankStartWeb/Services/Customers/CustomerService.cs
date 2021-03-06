using AutoMapper;
using Bank_AB.Data;
using Bank_AB.Services.Accounts;
using Faker;
using Microsoft.EntityFrameworkCore;

namespace Bank_AB.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CustomerService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Customer? GetCustomerFromId(int id)
    {
        return _context.Customers.Include(c => c.Accounts).FirstOrDefault(cust => cust.Id == id);
    }

    public List<Account> GetAccounts(int id)
    {
        var cust = GetCustomerFromId(id);
        if (cust == null)
            return null;
        return cust.Accounts;
    }

    public ICustomerService.ReturnCode CreateNewCustomer(Customer customer)
    {
        string countryCode = GetCountryCode(customer.Country);
        if (countryCode == null)
            return ICustomerService.ReturnCode.InvalidCountry;
        customer.CountryCode = countryCode;


        int telephoneCountryCode = GetTelephoneCountryCode(customer.Country);
        if (telephoneCountryCode == -1)
            return ICustomerService.ReturnCode.InvalidCountry;
        customer.TelephoneCountryCode = telephoneCountryCode;

        customer.Accounts.Add(new Account
        {
            Created = DateTime.Now,
            AccountType = "Debit",
            Balance = 0,
            Transactions = new List<Transaction>()
        });

        _context.Customers.Add(customer);
        _context.SaveChanges();
        return ICustomerService.ReturnCode.Ok;
    }

    public ICustomerService.ReturnCode EditCustomer(Customer customer, int id)
    {
        var editCustomer = GetCustomerFromId(id);

        customer.CountryCode = GetCountryCode(customer.Country);
        customer.TelephoneCountryCode = GetTelephoneCountryCode(customer.Country);

        _mapper.Map(customer, editCustomer);

        _context.SaveChanges();
        return ICustomerService.ReturnCode.Ok;
    }


    public ICustomerService.ReturnCode CreateNewAccount(int customerId, string accountType)
    {
        Customer customer = GetCustomerFromId(customerId);
        if (customer == null)
            return ICustomerService.ReturnCode.InvalidId;


        customer.Accounts.Add(new Account
        {
            AccountType = accountType,
            Balance = 0,
            Created = DateTime.Now,
            Transactions = new List<Transaction>(),
        });

        _context.SaveChanges();
        return ICustomerService.ReturnCode.Ok;
    }

    private string GetCountryCode(string country)
    {
        switch (country)
        {
            case "Sverige":
                return "SE";
            case "Norge":
                return "NO";
            case "Finland":
                return "FI";
            default:
                return null;
        }
    }

    private int GetTelephoneCountryCode(string country)
    {
        switch (country)
        {
            case "Sverige":
                return 46;
            case "Norge":
                return 47;
            case "Finland":
                return 48;
            default:
                return -1;
        }
    }
}