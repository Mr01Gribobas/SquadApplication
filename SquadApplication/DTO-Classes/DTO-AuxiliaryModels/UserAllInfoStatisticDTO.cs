using SquadServer.Models.ModelsEntity.AuxiliaryModels;
using System.Text.Json.Serialization;

namespace SquadServer.DTO_Classes.DTO_AuxiliaryModels;

public record class UserAllInfoStatisticDTO
    (
    string NamePlayer,
    string CallSingPlayer,
    int CountKill,
    int CountDieds,
    int CountFees,
    int CountEvents,
    DateTime LastUpdateDataStatistics,
    string OldDataJson,
    List<Achievement>? Achievements
    );