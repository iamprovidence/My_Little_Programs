using FunWithOAuth.OAuth.GoogleOAuthService;

var builder = WebApplication.CreateBuilder(args);
builder
.Services
    .AddControllersWithViews();


builder
    .Services
    .Configure<GoogleOptions>(builder.Configuration.GetSection("GoogleConfig"));
builder
    .Services
    .AddScoped<GoogleConnectionService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
