using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiTracksAPI.Models;

namespace MultiTracksAPI.Data
{
    public class MultiTracksAPIContext : DbContext
    {
        public MultiTracksAPIContext(DbContextOptions<MultiTracksAPIContext> options)
            : base(options)
        {
        }

        public DbSet<MultiTracksAPI.Models.Artist> Artist { get; set; } = default!;

        public DbSet<MultiTracksAPI.Models.Song> Song { get; set; }
    }
}
