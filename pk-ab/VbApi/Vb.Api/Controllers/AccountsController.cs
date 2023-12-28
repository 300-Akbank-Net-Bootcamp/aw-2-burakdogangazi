using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly VbDbContext dbContext;

        public AccountsController(VbDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<List<Account>> Get()
        {
            return await dbContext.Set<Account>()
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Account> Get(int id)
        {
            var account = await dbContext.Set<Account>()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return account;
        }

        [HttpPost]
        public async Task Post([FromBody] Account account)
        {
            await dbContext.Set<Account>().AddAsync(account);
            await dbContext.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Account account)
        {
            var fromdb = await dbContext.Set<Account>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.AccountNumber = account.AccountNumber;
            fromdb.IBAN = account.IBAN;
            fromdb.Balance = account.Balance;
            fromdb.CurrencyType = account.CurrencyType;
            fromdb.Name = account.Name;
            fromdb.OpenDate = account.OpenDate;

            await dbContext.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var fromdb = await dbContext.Set<Account>().Where(x => x.Id == id).FirstOrDefaultAsync();
            fromdb.IsActive = false;
            await dbContext.SaveChangesAsync();
        }
    }
}