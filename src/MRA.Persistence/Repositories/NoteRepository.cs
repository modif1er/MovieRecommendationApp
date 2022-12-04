using MRA.Application.Contracts.Persistence;
using MRA.Domain.Entities;
using MRA.Persistence.Context;

namespace MRA.Persistence.Repositories
{
    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
