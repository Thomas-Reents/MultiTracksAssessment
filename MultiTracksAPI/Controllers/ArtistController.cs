using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTracksAPI.Data;
using MultiTracksAPI.Models;
using NuGet.Versioning;

namespace MultiTracksAPI.Controllers
{
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly MultiTracksAPIContext _context;

        public ArtistController(MultiTracksAPIContext context)
        {
            _context = context;
        }

        // GET: api/Artists/5
        [HttpGet]
        [Route("api.multitracks.com/artist/search/{title}")]
        public ActionResult<Artist> GetArtist(string title)
        {
            if (String.IsNullOrEmpty(title))
            {
                return BadRequest();
            }

            var artist = _context.Artist.FirstOrDefault(art => art.title == title);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // POST: api/Artists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("api.multitracks.com/artist/add")]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            if(artist == null) 
            { 
                return BadRequest(); 
            }

            if (String.IsNullOrEmpty(artist.title))
            {
                return BadRequest("Missing title");
            }

            _context.Artist.Add(artist);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.ArtistID }, artist);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var Artist = await _context.Artist.FindAsync(id);

            if (Artist == null)
            {
                return NotFound();
            }

            return Artist;
        }

    }
}
