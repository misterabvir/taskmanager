using Domain;
using System.Threading.Tasks;

namespace DAL;

public interface ICommentsDAL
{
    Task Create(CommentModel model);
}
