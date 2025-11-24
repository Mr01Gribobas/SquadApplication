namespace SquadApplication.Models;

public class UserModel
{
    public int Id { get; set; }
    private Role _role ;//роль
    public string _callSing {  get;private set; }//позывной
    public string? _userName { get; private set; }//имя
    public int? _age { get; private set; }//возраст
    public bool? _isStaffed { get; private set; }//укомплектованность

    public int? EquipmentId { get;  set; }
    private Equipment _equipment;//снаряжение

}


