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
        [Route("/Comment/Create")]
        public async Task<IActionResult> CreateComment(CreateCommentModel model)
        {
            if (string.IsNullOrEmpty(model.Content)) 
                return BadRequest();
            
            await comments.Create(model.TaskId, model.Content);
            return Ok();
        }

        [HttpPut]
        [Route("/Comment/Update")]
        public async Task<IActionResult> SaveComment(SaveCommentModel model)
        {
            if (string.IsNullOrEmpty(model.Content))
                return BadRequest();
            await comments.Update(model.CommentId, model.Content);
            return Ok();
        }


        [HttpDelete]
        [Route("/Comment/Delete")]
        public async Task<IActionResult> DeleteComment(DeleteCommentModel model)
        {
            await comments.Delete(model.CommentId);
            return Ok();
        }
    }
}
