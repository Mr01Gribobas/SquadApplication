namespace SquadServer.Repositoryes;

public class EquipmentDbService : BaseDbService
{
    public EquipmentDbService(SquadDbContext squadDb) : base(squadDb)
    {
    }
    public async Task<EquipmentEntity?> GetEquipById(int id) => await _context.Equipments.FirstOrDefaultAsync(e => e.Id == id);
    public EquipmentEntity? GetEquipByUserId(int userId) => _context.Equipments.Include(u => u.OwnerEquipment).FirstOrDefault(e => e.OwnerEquipmentId == userId);
    public async Task<bool> CreateEquipment(int userId, EquipmentDTO? equipFromApp)
    {
        try
        {
            if(equipFromApp == null)
                throw new Exception("Ошибка при десериализации");

            var userFromDb = await _context.Players.Include(eq => eq.Equipment).FirstOrDefaultAsync(u => u.Id == userId);
            if(userFromDb is not null)
            {

                if(userFromDb.Equipment is not null)
                {
                    userFromDb.Equipment.MainWeapon = equipFromApp.MainWeapon;
                    userFromDb.Equipment.NameMainWeapon = equipFromApp.NameMainWeapon;

                    userFromDb.Equipment.SecondaryWeapon = equipFromApp.SecondaryWeapon;
                    userFromDb.Equipment.NameSecondaryWeapon = equipFromApp.NameSecondaryWeapon;

                    userFromDb.Equipment.HeadEquipment = equipFromApp.HeadEquipment;
                    userFromDb.Equipment.BodyEquipment = equipFromApp.BodyEquipment;
                    userFromDb.Equipment.UnloudingEquipment = equipFromApp.UnloudingEquipment;
                    _context.Equipments.Update(userFromDb.Equipment);
                }
                else
                {
                    var newEquip = EquipmentEntity.CreateModelEntity(equipFromApp);
                    newEquip.OwnerEquipment = userFromDb;
                    newEquip.OwnerEquipmentId = userFromDb.Id;
                    userFromDb.Equipment = newEquip;
                    await _context.Equipments.AddAsync(newEquip);

                }
            }
            else
                throw new Exception("Ошибка при попытке достать юреза");

            userFromDb?.UpdateStaffed(userFromDb?.Equipment ?? throw new NullReferenceException());
            await _context.SaveChangesAsync();

            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public async Task<bool> UpdateEquipment(int equipId, EquipmentDTO? equipFromApp)
    {
        EquipmentEntity? equipEntity = await _context.Equipments.Include(e => e.OwnerEquipment).FirstOrDefaultAsync(eq => eq.Id == equipId);

        try
        {

            if(equipEntity is not null)
            {
                equipEntity.MainWeapon = equipFromApp.MainWeapon;
                equipEntity.NameMainWeapon = equipFromApp.NameMainWeapon;

                equipEntity.SecondaryWeapon = equipFromApp.SecondaryWeapon;
                equipEntity.NameSecondaryWeapon = equipFromApp.NameSecondaryWeapon;

                equipEntity.UnloudingEquipment = equipFromApp.UnloudingEquipment;
                equipEntity.HeadEquipment = equipFromApp.HeadEquipment;
                equipEntity.BodyEquipment = equipFromApp.BodyEquipment;
                _context.Equipments.Update(equipEntity);

                if(equipEntity.OwnerEquipment is not null)
                    equipEntity.OwnerEquipment.UpdateStaffed(equipEntity);

                await _context.SaveChangesAsync();
            }
            else
            {
                UserModelEntity? userFromDb = await _context.Players.
                                                                   Include(eq => eq.Equipment).
                                                                   FirstOrDefaultAsync(u => u.EquipmentId == equipId);
                if(userFromDb is not null)
                {
                    if(userFromDb.Equipment is not null)
                    {
                        userFromDb.Equipment.MainWeapon = equipFromApp.MainWeapon;
                        userFromDb.Equipment.NameMainWeapon = equipFromApp.NameMainWeapon;

                        userFromDb.Equipment.SecondaryWeapon = equipFromApp.SecondaryWeapon;
                        userFromDb.Equipment.NameSecondaryWeapon = equipFromApp.NameSecondaryWeapon;

                        userFromDb.Equipment.UnloudingEquipment = equipFromApp.UnloudingEquipment;
                        userFromDb.Equipment.HeadEquipment = equipFromApp.HeadEquipment;
                        userFromDb.Equipment.BodyEquipment = equipFromApp.BodyEquipment;
                        _context.Equipments.Update(userFromDb.Equipment);
                        userFromDb.UpdateStaffed(userFromDb.Equipment);

                    }
                    else
                    {

                        EquipmentEntity newEquip = EquipmentEntity.CreateModelEntity(equipFromApp);
                        newEquip.OwnerEquipment = userFromDb;
                        newEquip.OwnerEquipmentId = userFromDb.Id;
                        userFromDb.Equipment = newEquip;
                        await _context.Equipments.AddAsync(newEquip);
                        userFromDb.UpdateStaffed(newEquip);
                    }
                    await _context.SaveChangesAsync();
                }
                else
                    throw new NullReferenceException();
            }
            return true;//status code ??
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
