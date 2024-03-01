using MyWebApp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// builder.Services.AddAuthentication().AddCookie(option => option.LoginPath = "/Index");
builder.Services.AddDbContext<SiteDbContext>();
var app = builder.Build();
app.UseFileServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
