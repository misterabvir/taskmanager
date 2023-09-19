using Domain;
using TaskManagerTests.Base;
using TaskManagerTests.Service;

namespace TaskManagerTests;

public class TaskDALTest : DALTestBase
{
    [Test]
    public async Task CreateNewTaskAndGetIt()
    {
        using (var scope = ScopeFactory.GetScope())
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            DateTime created = DateTime.Now;
            DateTime updated = DateTime.Now;

            ProjectModel model = new ProjectModel()
            {
                ProjectId = id,
                ProjectName = name,
                CreateDate = created,
                UpdateDate = updated,
            };

            Assert.DoesNotThrowAsync(async () => await projectDAL.Create(model));

            ProjectModel fromDbModel = await projectDAL.GetById(id);

            Assert.That(model.ProjectId, Is.EqualTo(fromDbModel.ProjectId));
            Assert.That(model.ProjectName, Is.EqualTo(fromDbModel.ProjectName));
        }

    }
    [Test]
    public async Task UpdateTest()
    {
        using (var scope = ScopeFactory.GetScope())
        {
            Guid projectId = Guid.NewGuid();
            string projectName = "test-project";
            DateTime projectCreated = DateTime.Now;
            DateTime projectUpdated = DateTime.Now;

            ProjectModel project = new ProjectModel()
            {
                ProjectId = projectId,
                ProjectName = projectName,
                CreateDate = projectCreated,
                UpdateDate = projectUpdated,
            };

            Guid taskId = Guid.NewGuid();
            string taskName = "test-project";
            DateTime taskCreated = DateTime.Now;

            TaskModel model = new TaskModel()
            {
                TaskId = taskId,
                ProjectId = projectId,
                TaskName = taskName,
                CreateDate = taskCreated
            };

            Assert.DoesNotThrowAsync(async () => await taskDAL.Create(model));

            TaskModel fromDbModel = await taskDAL.GetById(taskId);

            Assert.That(model.TaskId, Is.EqualTo(fromDbModel.TaskId));
            Assert.That(model.TaskName, Is.EqualTo(fromDbModel.TaskName));
            Assert.That(model.ProjectId, Is.EqualTo(fromDbModel.ProjectId));

        }
    }

    [Test]
    public async Task GetAllTest()
    {
        using (var scope = ScopeFactory.GetScope())
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            DateTime created = DateTime.Now;
            DateTime updated = DateTime.Now;

            ProjectModel model = new ProjectModel()
            {
                ProjectId = id,
                ProjectName = name,
                CreateDate = created,
                UpdateDate = updated,
            };

            Assert.DoesNotThrowAsync(async () => await projectDAL.Create(model));

            IEnumerable<ProjectModel> fromDb = await projectDAL.GetAll();
            Assert.IsTrue(fromDb.Any(model => model.ProjectId == id));
        }
    }
}
