using Kolos2_3tabele.DTOs;

namespace Kolos2_3tabele.Repositories;

public interface IMusicRepository
{
    public Task<bool> CheckIfMusicianExists(int id, CancellationToken token);
    public Task<GetMusicianWithTracksDTO> GetMusician(int id, CancellationToken token);


    public Task<string> AddMusician(AddMusicianDTO dto, CancellationToken token);
    public Task<int> GetNewMusicianId(AddMusicianDTO dto, CancellationToken token);
    public Task<int> UtworExists(AddMusicianDTO dto, CancellationToken token);
    public Task<string> AddTrack(AddMusicianDTO dto, CancellationToken token);
    public Task<int> GetNewUtworId(AddMusicianDTO dto, CancellationToken token);
    public Task<string> AddMusicianToTrack(int idMuzyk, int idUtwor ,CancellationToken token);
    public Task<int> TransactionToAdd(AddMusicianDTO dto, CancellationToken token);

}