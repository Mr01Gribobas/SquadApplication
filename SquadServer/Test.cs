using SquadServer.Models;
using SquadServer.Repositoryes;

namespace SquadServer;

public static class Test
{
    private static DataBaseRepository _dataBaseRepository;
    static Test()
    {
        SquadDbContext db = new SquadDbContext();
        _dataBaseRepository = new DataBaseRepository(db);
    }
    public static void TestMethodCreateCommanderAndTeam()
    {
        var user = UserModelEntity.CreateUserEntity("Falanga", "Roma", "Bezumie", "897383", Role.Private, 24,null);

        UserModelEntity? newUser = _dataBaseRepository.CreateNewUser(user);


    }
    public static void TestMethodUser()
    {
        using(SquadDbContext db = new SquadDbContext())
        {
            db.Players.Add(UserModelEntity.CreateUserEntity("Falanga", "Maks", "Bezumie", "897383", Role.LordOfTheApplication, 24, 1));
            var team = db.Teams.FirstOrDefault();
            team.Members.Add(UserModelEntity.CreateUserEntity("Falanga", "Maks", "Bezumie", "897383", Role.LordOfTheApplication, 24, 1));
            db.SaveChanges();


        }
    }


    public static void TestMethodEquip()
    {
        using(SquadDbContext db = new SquadDbContext())
        {
            db.Equipments.Add(new EquipmentEntity()
            {
                MainWeapon = true,
                NameMainWeapon = "AK",
                SecondaryWeapon = false,
                BodyEquipment = true,
                HeadEquipment = true,
                UnloudingEquipment = true,
                OwnerEquipmentId = 1
            });
            db.SaveChanges();

        }
    }
    public static void TestMethodEvent()
    {
        using(SquadDbContext db = new SquadDbContext())
        {
            db.Events.Add(new EventModelEntity()
            {
                NameTeamEnemy = "Demo",
                Coordinates = "12314",
                CountMembers = 1,
                NamePolygon = "Mal",
                Time = new TimeOnly(9, 0, 0),
                Date = DateOnly.Parse("30.12.2025"),
            });
            db.SaveChanges();

        }
    }
    public static void TestMethodTeam(int command)
    {
        if(command == 2)
        {
            using(SquadDbContext db = new SquadDbContext())
            {
                db.Teams.Add(new TeamEntity()
                {
                    Name = "Falanga",
                });
                db.SaveChanges();

            }
        }
        else
        {
            using(SquadDbContext db = new SquadDbContext())
            {
                var humans = db.Teams.Include(t => t.Members).FirstOrDefault(t => t.Id == 1);


            }
        }

    }
}
