using System.Text.Json.Serialization;

namespace SquadServer.Models.ModelsEntity;

public class EventModelEntity
{
    public int Id { get; set; }
    public string? NameTeamEnemy { get; set; }//команда соперника
    public string NamePolygon { get; set; }//название полигона
    public string Coordinates { get; set; }//координаты полигона
    public DateOnly Date { get; set; }//дата сбора
    public TimeOnly Time{ get; set; }//время сбора

    public int CountMembers { get; set; } = 0;//колл-во сокомандников которые будет на игре 


    public int TeamId { get; set; }

    [JsonIgnore]
    public virtual TeamEntity Team { get; set; } = null!;
}


