using MRA.Domain.Common;

namespace MRA.Domain.Entities
{
    public class Genre : BaseEntity<int>
    {
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
