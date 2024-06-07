using Kolos2_3tabele.DTOs;

namespace Kolos2_3tabele.Services;

public interface IMusicService
{
    public Task<GetMusicianWithTracksDTO> GetMusicianWithTracks(int id, CancellationToken token);
    public Task<string> AddMusicianWithTrack(AddMusicianDTO dto, CancellationToken token);
}