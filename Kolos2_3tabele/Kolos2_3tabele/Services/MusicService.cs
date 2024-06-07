using Kolos2_3tabele.DTOs;
using Kolos2_3tabele.Repositories;

namespace Kolos2_3tabele.Services;

public class MusicService : IMusicService
{
    private readonly IMusicRepository _musicRepository;

    public MusicService(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }
    
    public async Task<GetMusicianWithTracksDTO> GetMusicianWithTracks(int id, CancellationToken token)
    {
        bool verify = await _musicRepository.CheckIfMusicianExists(id, token);
        if (verify)
        {
            return await _musicRepository.GetMusician(id, token);
        }

        return new GetMusicianWithTracksDTO()
        {
            IdMuzyk = 0
        };
    }

    public async Task<string> AddMusicianWithTrack(AddMusicianDTO dto, CancellationToken token)
    {
        int res = await _musicRepository.TransactionToAdd(dto, token);

        if (res == -1)
        {
            return "Error: Transaction problem -> rollbacked";
        }

        return "Everything added";
    }
}