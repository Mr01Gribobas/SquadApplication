using SquadServer;

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
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<EventNotificationDistributor>();

builder.Services.AddSignalR();


builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});
builder.Services.AddDbContext<SquadDbContext>(option =>
{
    string? connectionString = builder.Configuration.GetConnectionString("SquadDbContext");
    option.UseNpgsql(connectionString ?? throw new NullReferenceException());
});


var app = builder.Build();
app.UseRouting();

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

Diagnostic(builder);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Base");
    app.UseHsts();
}

app.UseCors("CorsCustom");
app.UseAuthorization();
app.Use(async (context, next) =>
{
    Console.WriteLine("Start middlware___________________________________________");
    await next.Invoke(context);
});


app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Start server ......");
Console.ForegroundColor = ConsoleColor.Gray;
await  Test.SendMessageTest();
//app.Urls.Add("http://0.0.0.0:5213");
await app.RunAsync();

static void Diagnostic(WebApplicationBuilder builder)
{
    Console.WriteLine("===Diagnostics===");
    string? connString = builder.Configuration.GetConnectionString("SquadDbContext");
    Console.WriteLine($"Connection string from config {connString}");
    foreach (var env in Environment.GetEnvironmentVariables())
    {
        Console.WriteLine(env.ToString());
    }
}