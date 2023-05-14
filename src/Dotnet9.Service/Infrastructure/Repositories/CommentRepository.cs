namespace Dotnet9.Service.Infrastructure.Repositories;

public class CommentRepository : Repository<Dotnet9DbContext, Comment, Guid>, ICommentRepository
{
    public CommentRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}