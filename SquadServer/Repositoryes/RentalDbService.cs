namespace SquadServer.Repositoryes;

public class RentalDbService : BaseDbService
{
    public RentalDbService(SquadDbContext squadDb) : base(squadDb){}
    public async Task<List<RentailsDTO>>? GetAllReantilAsync(int teamId)
    {
        var list = await _context.Reantils.
                           Where(r => r.TeamId == teamId).
                           ToListAsync();
        if(list is null)
            return null;

        List<RentailsDTO> rentails = new List<RentailsDTO>();
        foreach(ReantalEntity item in list)
        {
            rentails.Add(new RentailsDTO()
            {
                NumderRental = item.Id,
                Weapon = item.Weapon,
                Mask = item.Mask,
                Helmet = item.Helmet,
                Balaclava = item.Balaclava,
                SVMP = item.SVMP,
                Outterwear = item.Outterwear,
                Gloves = item.Gloves,
                _isStaffed = item.IsStaffed,
                BulletproofVestOrUnloadingVest = item.BulletproofVestOrUnloadingVest,
            });
        }
        return rentails;
    }
    public async Task<bool> DeleteRentail(int rentailNymber)
    {
        var item = await _context.Reantils.FirstAsync(u => u.Id == rentailNymber);
        if(item is null)
            return false;

        _context.Reantils.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
