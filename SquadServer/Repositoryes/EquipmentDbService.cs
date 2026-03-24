namespace SquadServer.Repositoryes;

public class EquipmentDbService : BaseDbService
{
    public EquipmentDbService(SquadDbContext squadDb) : base(squadDb)
    {
    }
    public async Task<EquipmentEntity?> GetEquipById(int id) => await _context.Equipments.FirstOrDefaultAsync(e => e.Id == id);
    public EquipmentEntity? GetEquipByUserId(int userId) => _context.Equipments.Include(u => u.OwnerEquipment).FirstOrDefault(e => e.OwnerEquipmentId == userId);

}
