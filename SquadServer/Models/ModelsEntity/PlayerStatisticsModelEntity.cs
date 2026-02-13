using System.Text.Json.Serialization;

namespace SquadServer.Models.ModelsEntity;

public class PlayerStatisticsModelEntity
{
    public int Id { get; set; }


    [JsonInclude]
    public string NamePlayer { get; set; }
    [JsonInclude]
    public string CallSingPlayer { get; set; }
    [JsonInclude]
    public int CountKill { get; set; } = 0;
    [JsonInclude]
    public int CountDieds { get; set; } = 0;

    [JsonInclude]
    public int CountFees { get; set; } = 0;

    [JsonInclude]
    public int CountEvents { get; set; } = 0;

    [JsonInclude]
    public DateTime LastUpdateDataStatistics { get; set; }

    [JsonInclude]
    public string OldDataJson { get; set; }

    [JsonInclude]
    public bool IsCommanderCheck { get; set; } = false;


    public int UserModelId { get; set; }

    [JsonIgnore]
    public UserModelEntity UserModel { get; set; } = null!;
}
