using MRA.Domain.Common;

namespace MRA.Domain.Entities
{
    public class Note : BaseEntity<int>
    {
        public string Text { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
