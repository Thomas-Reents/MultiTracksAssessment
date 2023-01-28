using Microsoft.Build.Framework;

namespace MultiTracksAPI.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }

        public DateTime dateCreation { get; set; }
        public string title { get; set; }
        public string? biography { get; set; }
        public string? imageURL { get; set; }
        public string? heroURL { get; set; }
    }
}
