using BL;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWithTSReact.Server.ViewModels.PostRequestModel;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IComments comments;
        public CommentsController(IComments comments)
        {
            this.comments = comments;
        }

        [HttpPost]
        [Route("/createComment")]
        public async Task<IActionResult> CreateComment(CreateCommentModel model)
        {
            if (string.IsNullOrEmpty(model.Content)) 
                return BadRequest();
            
            await comments.Create(model.TaskId, model.Content);
            return Ok();
        }
    }
}
