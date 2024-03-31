using AuthProject.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using PacketCaptureServer;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();



builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromSeconds(5400);
    
    })
    ;



builder.WebHost.UseUrls("http://localhost:5001");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();



app.UseAuthentication();

app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<PacketHub>("/packetHub"); // Map the PacketHub to a specific endpoint
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
 
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
