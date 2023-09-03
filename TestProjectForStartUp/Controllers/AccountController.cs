using Microsoft.AspNetCore.Mvc;
using TestProjectForStartUp.ExtraHelp;
using TestProjectForStartUp.Interfaces;
using ZauriStartUp.Models;

namespace TestProjectForStartUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository account;
        public AccountController(IAccountRepository account)
        {
            this.account = account;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Account>))]
        [ProducesResponseType(400)]
        public IActionResult GetAccounts()
        {
            var accs = account.GetAccounts();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accs);
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400)]
        public IActionResult GetAccount(int id)
        {
            if(!account.Exist(id))
            {
                return NotFound();
            }
            var accs = account.GetAccount(id);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accs);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAccount([FromBody] AccountHelp accountH)
        {
           

                if (accountH == null)
                return BadRequest(ModelState);

            var accs = account.GetAccounts()
                .Where(a => a.Email.Trim().ToUpper() == accountH.Email.Trim().ToUpper() || a.Name.Trim().ToUpper() == accountH.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (accs != null)
            {
                ModelState.AddModelError("", "account exsisted");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var acc = new Account
            {
                Name = accountH.Name,
                Email = accountH.Email,
                Password = accountH.Password,
                DateTime = DateTime.Now,
            };

            if (!account.CreateAccount(acc))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }





    }
}
