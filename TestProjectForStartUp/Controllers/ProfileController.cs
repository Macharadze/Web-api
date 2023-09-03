using Microsoft.AspNetCore.Mvc;
using TestProjectForStartUp.ExtraHelp;
using TestProjectForStartUp.Interfaces;
using ZauriStartUp.Models;

namespace TestProjectForStartUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository profile;
        private readonly IAccountRepository account1;
        public ProfileController(IProfileRepository account, IAccountRepository accountRepository)
        {
            this.profile = account;
            this.account1 = accountRepository;
        }
       [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Profile>))]
        [ProducesResponseType(400)]
        public IActionResult GetProfiles()
        {
            var profs = profile.GetProfiles();
            return Ok(profs);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Profile))]
        [ProducesResponseType(400)]
        public IActionResult GetProfile(int id)
        {
            if (!profile.Exist(id))
            {
                return NotFound();
            }
            var accs = profile.GetProfile(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accs);
        }
        [HttpGet("{id}/owner")]
        [ProducesResponseType(200, Type = typeof(Profile))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int id)
        {
            if (!profile.Exist(id))
            {
                return NotFound();
            }
            var accs = profile.GetOwner(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accs);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProfile([FromBody] ProfileHelp profileH, [FromQuery] int accId)
        {


            if (profileH == null)
                return BadRequest(ModelState);

            var accs = profile.GetProfiles()
                .Where(a => a.Email.Trim().ToUpper() == profileH.about.Trim().ToUpper())
                .FirstOrDefault();

            if (accs != null)
            {
                ModelState.AddModelError("", "account exsisted");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!account1.Exist(accId))
                return NotFound("does not exsist");

            var acc = account1.GetAccount(accId);
            var prof = new Profile
            {
                Profession = profileH.Profession,
                Name = profileH.Name,
                LastName = profileH.LastName,
                Address = profileH.Address,
                Image = profileH.Image,
                Phone = profileH.Phone,
                about = profileH.about,
                userId = accId,
                User = acc,
                DateTime = DateTime.Now.ToString("yy-MM-dd HH:mm:ss.fff"),
                Email = profileH.Email,
                Comments = 0


        };

            if (!profile.CreateProfile(prof))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpGet("{id}/comments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int id)
        {
            if (!profile.Exist(id))
            {
                return NotFound();
            }
            var accs = profile.GetComments(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accs);
        }


    }
}
