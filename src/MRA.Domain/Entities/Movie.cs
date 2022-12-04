using MRA.Domain.Common;

namespace MRA.Domain.Entities
{
    public class Movie : BaseEntity<int>
    {
        public string Name { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
