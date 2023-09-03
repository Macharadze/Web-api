using Microsoft.AspNetCore.Mvc;
using TestProjectForStartUp.ExtraHelp;
using TestProjectForStartUp.Interfaces;
using ZauriStartUp.Models;

namespace TestProjectForStartUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository comment;
        private readonly IAccountRepository account;
        private readonly IProfileRepository profile;

       public CommentController(ICommentRepository comment, IAccountRepository account, IProfileRepository profile)
        {
            this.comment = comment;
            this.account = account;
            this.profile = profile;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetComments()
        {
            var accs = comment.GetComments();
          
            return Ok(accs);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int id)
        {
            if (!comment.Exist(id))
            {
                return NotFound();
            }
            var accs = comment.GetComment(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(accs);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComment([FromBody] CommentHelp commentHelp, [FromQuery] int profId)
        {


            if (commentHelp == null)
                return BadRequest(ModelState);



            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var prof = profile.GetProfile(profId);
            var acc = account.GetAccount(prof.userId);
            if (acc == null)
                return NotFound();
            var comment1 = new Comment
            {
                Body = commentHelp.Body,
                ProfileId = profId,
                AccountId = acc.Id,
                Profile = profile.ProfuleHelp(profId),
                Account = acc,
            };

            if (!comment.CreateComment(comment1))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
