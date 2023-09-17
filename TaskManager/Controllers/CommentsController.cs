using BL;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Mappers;
using TaskManager.ViewModels;
using TaskManager.ViewModels.PostRequestModel;

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
        [Route("/getComments")]
        public async Task<IEnumerable<CommentViewModel>> GetComments(GetCommentModel model)
        {
            var commentsList = await comments.GetByTaskId(model.TaskId);
            return commentsList.OrderByDescending(c=>c.Created).Select(CommentMapper.MapCommentModelToCommentViewModel);
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
