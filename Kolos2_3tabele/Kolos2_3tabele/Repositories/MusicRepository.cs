using Kolos2_3tabele.DTOs;
using Kolos2_3tabele.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

namespace Kolos2_3tabele.Repositories;

public class MusicRepository : IMusicRepository
{
    private readonly MusicDbContext _context;

    public MusicRepository(MusicDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfMusicianExists(int id, CancellationToken token)
    {
        var res = await _context.Muzycy
            .FirstOrDefaultAsync(x => x.IdMuzyk == id, token);

        if (res == null)
        {
            return false;
        }

        return true;
    }

    public async Task<GetMusicianWithTracksDTO> GetMusician(int id, CancellationToken token)
    {
        var list = _context.Muzycy
            .Where(x => x.IdMuzyk == id)
            .Include(x => x.IdUtwory);
        

        var res = await list
            .Select(x => new GetMusicianWithTracksDTO()
            {
                IdMuzyk = x.IdMuzyk,
                Imie = x.Imie,
                Nazwisko = x.Nazwisko,
                Pseudonim = x.Pseudonim,
                Utwory = x.IdUtwory.Select(
                    u => new TrackDTO()
                    {
                        CzasTrwania = u.CzasTrwania,
                        NazwaUtworu = u.NazwaUtworu
                    }).ToList()
            }).ToListAsync(token);

        GetMusicianWithTracksDTO result = new GetMusicianWithTracksDTO()
        {
            IdMuzyk = res[0].IdMuzyk,
            Imie = res[0].Imie,
            Nazwisko = res[0].Nazwisko,
            Pseudonim = res[0].Pseudonim,
            Utwory = res[0].Utwory
        };

        return result;
    }

    public async Task<string> AddMusician(AddMusicianDTO dto, CancellationToken token)
    {
        Muzyk muzyk = new Muzyk()
        {
            Imie = dto.Imie, Nazwisko = dto.Nazwisko, Pseudonim = dto.Pseudonim
        };

        await _context.Muzycy.AddAsync(muzyk, token);
        await _context.SaveChangesAsync(token);
        return "Dodano muzyka";
    }

    public async Task<int> GetNewMusicianId(AddMusicianDTO dto, CancellationToken token)
    {
        Muzyk muzyk = await _context.Muzycy
            .FirstOrDefaultAsync(x => x.Imie == dto.Imie && x.Nazwisko == dto.Nazwisko, token);

        return muzyk.IdMuzyk;
    }

    public async Task<int> UtworExists(AddMusicianDTO dto, CancellationToken token)
    {
        Utwor utwor = await _context.Utwory
            .FirstOrDefaultAsync(x => x.IdUtwor == dto.IdUtwor, token);

        if (utwor == null)
        {
            return 0;
        }

        return utwor.IdUtwor;
    }

    public async Task<string> AddTrack(AddMusicianDTO dto, CancellationToken token)
    {
        Utwor utwor = new Utwor()
        {
            NazwaUtworu = dto.NazwaUtworu, CzasTrwania = dto.CzasTrwania
        };

        await _context.Utwory.AddAsync(utwor, token);
        await _context.SaveChangesAsync(token);
        return "Dodalem utwor";
    }

    public async Task<int> GetNewUtworId(AddMusicianDTO dto, CancellationToken token)
    {
        Utwor utwor = await _context.Utwory
            .FirstOrDefaultAsync(x =>
                x.NazwaUtworu == dto.NazwaUtworu, token);
        return utwor.IdUtwor;
    }

    public async Task<string> AddMusicianToTrack(int idMuzyk, int idUtwor , CancellationToken token)
    {
        Muzyk muzyk = await _context.Muzycy.FirstAsync(x => x.IdMuzyk == idMuzyk, token);
        Utwor utwor = await _context.Utwory.FirstAsync(x => x.IdUtwor == idUtwor, token);
        
        muzyk.IdUtwory.Add(utwor);

        await _context.SaveChangesAsync(token);
        return "Dodalem muzyka do utowru";
    }

    public async Task<int> TransactionToAdd(AddMusicianDTO dto, CancellationToken token)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(token);
        try
        {
            await AddMusician(dto, token);
            int muzykId = await GetNewMusicianId(dto, token);
            int utworId = await UtworExists(dto, token);

            if (utworId == 0)
            {
                await AddTrack(dto, token);
                utworId = await GetNewUtworId(dto, token);
            }

            await AddMusicianToTrack(muzykId, utworId, token);
            await transaction.CommitAsync(token);
            return 0;

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(token);
            return -1; // blad transakcji;
        }
    }




}