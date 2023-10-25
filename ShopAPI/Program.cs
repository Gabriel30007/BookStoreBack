using Microsoft.Extensions.Configuration;
using Shop.DAL;
using ShopAPI.Helpers;
using ShopAPI.Hubs;
using ShopAPI.Services;
using ShopBLL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
const string BPMPolicy = "BPMPolicy";


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddLogging();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthentication().AddCookie();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<NotificationService>();
ShopDalModule.Load(builder.Services, builder.Configuration);
ShopBLLModule.Load(builder.Services, builder.Configuration);
ConfigurationHelper.Initialize(builder.Configuration);
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyHeader()
           .SetIsOriginAllowed((host) => true).AllowAnyMethod()

           .WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .WithMethods("GET", "POST")
           .AllowCredentials()
     

           );

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseSession();


app.UseAuthorization();

app.MapControllers(); app.MapDefaultControllerRoute();
app.MapHub<NotificationHub>("/notification");
app.Run();