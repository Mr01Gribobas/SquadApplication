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

app.Use( async (context,next) =>
{
    Console.WriteLine();
    await next.Invoke(context);
});
app.MapGet("/Login", () =>
{
    Console.WriteLine();
});


app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();


