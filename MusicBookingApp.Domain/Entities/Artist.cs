using MusicBookingApp.Domain.Common;

namespace MusicBookingApp.Domain.Entities
{
    public class Artist : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }

}
