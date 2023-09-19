using Domain;
using TaskManagerTests.Base;
using TaskManagerTests.Service;

namespace TaskManagerTests;

public class CommentsDALTest : DALTestBase
{
    [Test]
    public async Task CreateNewCommentAndGetIt()
    {
        using (var scope = ScopeFactory.GetScope())
        {
            Guid commentId = Guid.NewGuid();
            Guid taskId = Guid.NewGuid();
            string content = "test-project";
            DateTime commentCreated = DateTime.Now;

            CommentModel model = new CommentModel()
            {
                CommentId = commentId,
                TaskId =taskId,
                Content = content,
                CreateDate = commentCreated,
            };

            Assert.DoesNotThrowAsync(async () => await commentsDAL.Create(model));

            IEnumerable<CommentModel> fromDbModel = await commentsDAL.GetByTaskId(taskId);

            Assert.IsTrue(fromDbModel.Any(c=>c.CommentId == commentId));
            Assert.IsTrue(fromDbModel.Any(c=>c.Content == content));

        }

    }
}