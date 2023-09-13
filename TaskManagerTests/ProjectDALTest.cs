using Domain;
using TaskManagerTests.Base;
using TaskManagerTests.Service;

namespace TaskManagerTests;

public class ProjectDALTest : DALTestBase
{
   [Test]
    public async Task CreateNewProjectAndGetIt()
    {
        using(var scope = ScopeFactory.GetScope())
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            DateTime created = DateTime.Now;
            DateTime updated = DateTime.Now;

            ProjectModel model = new ProjectModel()
            {
                Id = id,
                ProjectName = name,
                CreateDate = created,
                UpdateDate = updated,
            };

            Assert.DoesNotThrowAsync(async () => await projectDAL.Create(model));

            ProjectModel fromDbModel = await projectDAL.GetById(id);                
            
            Assert.That(model.Id, Is.EqualTo(fromDbModel.Id));
            Assert.That(model.ProjectName, Is.EqualTo(fromDbModel.ProjectName));
        }           
    }

    [Test]
    public async Task UpdateTest()
    {
        using (var scope = ScopeFactory.GetScope())
        {
            Guid id = Guid.NewGuid();
            string name = "test";
            string updateName = "update";
            DateTime created = DateTime.Now;
            DateTime updated = DateTime.Now;

            ProjectModel model = new ProjectModel()
            {
                Id = id,
                ProjectName = name,
                CreateDate = created,
                UpdateDate = updated,
            };

            Assert.DoesNotThrowAsync(async () => await projectDAL.Create(model));

            model.ProjectName = updateName;
            Assert.DoesNotThrowAsync(async () => await projectDAL.Update(model));

            ProjectModel fromDbModel = await projectDAL.GetById(id);
            Assert.That(model.ProjectName, Is.EqualTo(updateName));
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
                Id = id,
                ProjectName = name,
                CreateDate = created,
                UpdateDate = updated,
            };

            Assert.DoesNotThrowAsync(async () => await projectDAL.Create(model));

            IEnumerable<ProjectModel> fromDb = await projectDAL.GetAll();
            Assert.IsTrue(fromDb.Any(model=>model.Id == id));
        }
    }
}