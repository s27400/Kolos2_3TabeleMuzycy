using System.Formats.Asn1;
using Kolos2_3tabele.DTOs;
using Kolos2_3tabele.Repositories;
using Kolos2_3tabele.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolos2_3tabele.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MusicController : ControllerBase
{
    private readonly IMusicService _musicService;

    public MusicController(IMusicService musicService)
    {
        _musicService = musicService;
    }

    [HttpGet("{MusicianId}")]
    public async Task<IActionResult> GetMusician(int MusicianId, CancellationToken token)
    {
        GetMusicianWithTracksDTO res = await _musicService.GetMusicianWithTracks(MusicianId, token);

        if (res.IdMuzyk == 0)
        {
            return NotFound($"Muzyk o id: {MusicianId} nie istnieje");
        }

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> AddMusicianWithTrakc(AddMusicianDTO dto, CancellationToken token)
    {
        string res = await _musicService.AddMusicianWithTrack(dto, token);

        if (res.StartsWith("Error"))
        {
            return NotFound(res);
        }

        return Ok(res);
    }
}