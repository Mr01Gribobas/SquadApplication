using Microsoft.AspNetCore.Http.HttpResults;

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
    public async Task<bool> AppendRental(int commanderId,RentailsDTO? rentalDTOModel) 
    {
        try
        {
            if(rentalDTOModel is null || commanderId <= 0)
                throw new NullReferenceException();

            var commander = await _context.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == commanderId);
            ReantalEntity model = null;

            if(commander is not null && commander.Team is not null)
            {
                model = new ReantalEntity()
                {
                    TeamEntity = commander.Team,
                    TeamId = commander.TeamId ?? throw new ArgumentNullException(),
                    Weapon = rentalDTOModel.Weapon,
                    Mask = rentalDTOModel.Mask,
                    Helmet = rentalDTOModel.Helmet,
                    Balaclava = rentalDTOModel.Balaclava,
                    SVMP = rentalDTOModel.SVMP,
                    Outterwear = rentalDTOModel.Outterwear,
                    Gloves = rentalDTOModel.Gloves,
                    BulletproofVestOrUnloadingVest = rentalDTOModel.BulletproofVestOrUnloadingVest,
                    IsStaffed = rentalDTOModel.Balaclava &&
                            rentalDTOModel.Outterwear &&
                            rentalDTOModel.BulletproofVestOrUnloadingVest &&
                            rentalDTOModel.Gloves &&
                            rentalDTOModel.SVMP &&
                            rentalDTOModel.Helmet &&
                            rentalDTOModel.Mask &&
                            rentalDTOModel.Weapon
                            ? true : false,
                };
                await _context.Reantils.AddAsync(model);
                await _context.SaveChangesAsync();
            }

            if(model is null)
                throw new NullReferenceException();
            return true;

        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> UpdateRentalById(int rentaleId,RentailsDTO rentalDtoModel)
    {
        try
        {
            if(rentalDtoModel is null || rentaleId <= 0)
                throw new NullReferenceException();

            var rentaFromDb = await _context.Reantils.FirstOrDefaultAsync(u => u.Id == rentaleId);
            if(rentaFromDb is not null)
            {
                rentaFromDb.Weapon = rentalDtoModel.Weapon;
                rentaFromDb.Mask = rentalDtoModel.Mask;
                rentaFromDb.Helmet = rentalDtoModel.Helmet;
                rentaFromDb.Balaclava = rentalDtoModel.Balaclava;
                rentaFromDb.SVMP = rentalDtoModel.SVMP;
                rentaFromDb.Outterwear = rentalDtoModel.Outterwear;
                rentaFromDb.Gloves = rentalDtoModel.Gloves;
                rentaFromDb.BulletproofVestOrUnloadingVest = rentalDtoModel.BulletproofVestOrUnloadingVest;
                rentaFromDb.IsStaffed = rentalDtoModel.Balaclava &&
                            rentalDtoModel.Outterwear &&
                            rentalDtoModel.BulletproofVestOrUnloadingVest &&
                            rentalDtoModel.Gloves &&
                            rentalDtoModel.SVMP &&
                            rentalDtoModel.Helmet &&
                            rentalDtoModel.Mask &&
                            rentalDtoModel.Weapon
                            ? true : false;
                _context.Reantils.Update(rentaFromDb);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
