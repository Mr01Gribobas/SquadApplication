namespace SquadServer.Models;

public class UserModelEntity
{
    public int Id { get; set; }
    public Role _role { get; private set; } //роль
    public string _callSing { get; private set; } = null!;  //позывной
    public string _teamName { get; private set; } = null!;//team
    public string _phoneNumber { get; private set; } = null!;// number
    public string? _userName { get; private set; }//имя
    public int? _age { get; private set; }//возраст
    public bool? _isStaffed { get; private set; }//укомплектованность
    public bool? _goingToTheGame { get; set; }//явка на игру 


    public int? EquipmentId { get; set; }
    public EquipmentEntity? Equipment;//снаряжение

}


