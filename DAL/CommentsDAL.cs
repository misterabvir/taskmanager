using Domain;
using System.Threading.Tasks;
using DAL.Base;

namespace DAL;

public class CommentsDAL : ICommentsDAL
{
    private readonly IRepository repository;
    public CommentsDAL(IRepository repository)
    {
        this.repository = repository;
    }

    public async Task Create(CommentModel model)
    {
        await repository.ExecuteAsync(SQL.Comments.Create, model);
    }
}