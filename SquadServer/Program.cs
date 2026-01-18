using Microsoft.Extensions.Options;
using SquadServer.Models;
using SquadServer.Services.Service_DeviceRegistration;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddCors(build =>
{
    build.AddPolicy("CorsCustom", option =>
    {
        option.AllowAnyHeader();
        option.AllowAnyMethod();
        option.AllowAnyOrigin();
        //option.AllowCredentials();
    });
});
builder.Services.AddScoped<IDeviceRegistrationService, DeviceRegistrationService>();
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
builder.Services.AddDbContext<SquadDbContext>(option =>
{
    string? connectionString = builder.Configuration.GetConnectionString("SquadDbContext");
    option.UseSqlServer(connectionString ?? throw new NullReferenceException());
});


var app = builder.Build();
app.UseRouting();
app.UseHttpsRedirection();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Base");
    app.UseHsts();
}

app.UseCors("CorsCustom");
app.UseAuthorization();

//app.Use( async (context,next) =>
//{
//    Console.WriteLine();
//    await next.Invoke(context);
//});




app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run("http://0.0.0.0:5213");


