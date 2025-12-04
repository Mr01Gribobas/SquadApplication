using SquadServer;
using SquadServer.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<SquadDbContext>(option =>
{
    string? connectionString = builder.Configuration.GetConnectionString("SquadDbContext");
    option.UseSqlServer(connectionString ?? throw new NullReferenceException());
});


var app = builder.Build();


if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Base");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();
//TestBlock

Test.TestMethodTeam(2);




//
app.UseAuthorization();


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();


