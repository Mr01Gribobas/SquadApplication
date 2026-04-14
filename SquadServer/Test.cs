namespace SquadServer;

public static class Test
{
    private static DataBaseRepository _dataBaseRepository;
    static Test()
    {
        SquadDbContext db = new SquadDbContext();
        _dataBaseRepository = new DataBaseRepository(db);
    }
    public static async Task TestMethodCreateCommanderAndTeam()
    {
        var user = UserModelEntity.CreateUserEntity("Dr", "Roma", "Bezumie", "897383", Role.Commander, 24, null);

        UserModelEntity? newUser = await _dataBaseRepository.CreateNewUser(user);
    }
    public static async Task TestMethodUser()
    {
        using(SquadDbContext db = new SquadDbContext())
        {
            var newUser = UserModelEntity.CreateUserEntity("Falanga", "Maks", "Bezumie", "897383", Role.LordOfTheApplication, 24, 1);
            await db.Players.AddAsync(newUser);
            var team = await db.Teams.FirstOrDefaultAsync();
            team.Members.Add(newUser);
            db.SaveChanges();


        }
    }
    public static async Task TestCreateEvenForAllCommaa()
    {

        using(SquadDbContext db = new SquadDbContext())
        {
            var commander = await db.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u._role == Role.Private);

            var events = await db.EventsForAllCommands.Include(u=>u.Players) .FirstOrDefaultAsync();

            events.Players.Add(commander);

            db.EventsForAllCommands.Update(events);
            await db.SaveChangesAsync();
        }
    }
    public static async Task TestGetStatistic()
    {
        using(SquadDbContext db = new SquadDbContext())
        {
            var res = await db.PlayerStatistics.FirstOrDefaultAsync(s => s.UserModelId == 1);
            var res2 = await db.Players.Include(s => s.Statistic).FirstOrDefaultAsync(u=>u.Id == 1);
        }
    }

    public static async Task TestCreqwteEquip()
    {
        using(SquadDbContext db = new SquadDbContext())
        {
            var equipId = 8;
            var equip = await db.Equipments.Include(u => u.OwnerEquipment).FirstOrDefaultAsync(e => e.Id == equipId);

            if(equip is not null)
            {
                equip.UnloudingEquipment = true;
                equip.HeadEquipment = true;
                equip.BodyEquipment = false;

                equip.MainWeapon = true;
                equip.NameMainWeapon = "PPPPPPP";

                equip.SecondaryWeapon = false;
                equip.NameSecondaryWeapon = null;
                db.Equipments.Update(equip);

                if(equip.OwnerEquipment is not null)
                    equip.OwnerEquipment.UpdateStaffed(equip);

                await db.SaveChangesAsync();

            }
            else
            {
                var userFromDb = await db.Players.Include(u => u.Equipment).FirstOrDefaultAsync(u => u.EquipmentId == 8);
                if(userFromDb is not null)
                {
                    if(userFromDb.Equipment is not null)
                    {
                        //если у юзера найден екип
                        userFromDb.Equipment.UnloudingEquipment = true;
                        userFromDb.Equipment.HeadEquipment = true;
                        userFromDb.Equipment.BodyEquipment = true;

                        userFromDb.Equipment.MainWeapon = true;
                        userFromDb.Equipment.NameMainWeapon = "M4A4";

                        userFromDb.Equipment.SecondaryWeapon = false;
                        userFromDb.Equipment.NameSecondaryWeapon = null;
                        db.Equipments.Update(userFromDb.Equipment);
                    }
                    else
                    {
                        //если у юзера нету укипа
                        var newEquip = EquipmentEntity.CreateModelEntity(new EquipmentDTO()
                        {
                            BodyEquipment = true,
                            HeadEquipment = true,
                            UnloudingEquipment = true,

                            SecondaryWeapon = false,
                            NameSecondaryWeapon = null,

                            NameMainWeapon = "Ak",
                            MainWeapon = true,
                        });
                        newEquip.OwnerEquipment = userFromDb;
                        newEquip.OwnerEquipmentId = userFromDb.Id;
                        userFromDb.Equipment = newEquip;
                        await db.Equipments.AddAsync(newEquip);
                    }
                    await db.SaveChangesAsync();
                }

            }



            //var user = db.Players.Include(eq => eq.Equipment).FirstOrDefault(u => u.Id == 1);

            //if(user is not null)
            //{
            //    if(user.Equipment is not null)
            //    {
            //        user.Equipment.MainWeapon = true;
            //        user.Equipment.NameMainWeapon = "M4A1";
            //        user.Equipment.NameSecondaryWeapon = null;
            //        user.Equipment.SecondaryWeapon = false;
            //        user.Equipment.HeadEquipment = true;
            //        user.Equipment.BodyEquipment = true;
            //        user.Equipment.UnloudingEquipment = true;
            //        db.Equipments.Update(user.Equipment);
            //        //user.UpdateStaffed(user.Equipment);
            //    }
            //    else
            //    {
            //        var newEquip = new EquipmentEntity()
            //        {
            //            MainWeapon = true,
            //            NameMainWeapon = "Ak",

            //            SecondaryWeapon = false,
            //            NameSecondaryWeapon = null,

            //            BodyEquipment = true,
            //            HeadEquipment = true,
            //            UnloudingEquipment = true,
            //            OwnerEquipment = user,
            //            OwnerEquipmentId = user.Id

            //        };
            //        user.Equipment = newEquip;
            //        await db.Equipments.AddAsync(newEquip);
            //        user.UpdateStaffed(newEquip);
            //    }
            //    await db.SaveChangesAsync();
            //}
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
    public static void TestMethodTeam()
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
    public async static void TestMethodRental()
    {
        using(SquadDbContext db = new SquadDbContext())
        {
            var team = await db.Teams.FirstOrDefaultAsync();
            db.Reantils.Add(new ReantalEntity()
            {
                TeamId = team.Id,
                Weapon = true,
                Mask = true,
                Helmet = true,
                Balaclava = true,
                SVMP = true,
                Outterwear = true,
                Gloves = true,
                BulletproofVestOrUnloadingVest = true,
                IsStaffed = true &&
                            true &&
                            true &&
                            true &&
                            true &&
                            true &&
                            true &&
                            true
                            ? true : false,
            });
            db.SaveChanges();
        }
        ;




    }
}

