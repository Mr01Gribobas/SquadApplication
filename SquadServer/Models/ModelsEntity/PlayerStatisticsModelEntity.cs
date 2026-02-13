using SquadServer.Models.ModelsEntity.AuxiliaryModels;
using System.Text.Json;
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
    public string AchievementsJson { get; set; }

    public List<Achievement>? Achievements
    {
        get
        {
            try
            {
                List<Achievement?>? data = JsonSerializer.Deserialize<List<Achievement>>(AchievementsJson);
                return data ?? throw new NullReferenceException() ;
            }
            catch(Exception)
            {
                return new();
            }


        }
        private set;
    }

    [JsonInclude]
    public bool IsCommanderCheck { get; set; } = false;



    public int UserModelId { get; set; }

    [JsonIgnore]
    public UserModelEntity UserModel { get; set; } = null!;



    public void UpdateAchievements(Achievement achievement)
    {
        List<Achievement>? currentAchivement = Achievements;
        if(currentAchivement is not null)
        {
            currentAchivement.Add(achievement);
            AchievementsJson =  JsonSerializer.Serialize<List<Achievement>>(currentAchivement);
        }

        currentAchivement.Add(achievement);
        AchievementsJson = JsonSerializer.Serialize<List<Achievement>>(currentAchivement);

    }
}
