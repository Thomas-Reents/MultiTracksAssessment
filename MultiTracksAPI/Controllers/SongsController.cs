using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTracksAPI.Data;
using MultiTracksAPI.Models;

namespace MultiTracksAPI.Controllers
{
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MultiTracksAPIContext _context;

        public SongsController(MultiTracksAPIContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        [Route("api.multitracks.com/song/list")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSong([FromQuery] SongListInput listInput)
        {
            if(listInput == null)
            {
                return BadRequest();
            }

            if(listInput.pageNumber == -1 && listInput.pageSize == -1)
            {
                return await _context.Song.ToListAsync();
            }

            if (listInput.pageNumber == -1 && listInput.pageSize != -1)
            {
                return BadRequest("Missing Parameter pageNumber");
            }

            if (listInput.pageNumber != -1 && listInput.pageSize == -1)
            {
                return BadRequest("Missing Parameter pageSize");
            }

            if (listInput.pageNumber == 0)
            {
                return BadRequest("pageNumber cannot be 0");
            }

            var songList = await _context.Song.Skip((listInput.pageNumber - 1) * listInput.pageSize).Take(listInput.pageSize).ToListAsync();

            if (songList == null)
            {
                return NotFound();
            }

            return songList;
           
        }

    }
}
