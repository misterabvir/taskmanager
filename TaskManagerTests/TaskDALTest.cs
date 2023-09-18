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

}