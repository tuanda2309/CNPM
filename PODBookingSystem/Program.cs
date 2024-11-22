using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PODBooking.Repositories.Models.Momo;
using PODBooking.Services.Services.Momo;
using PODBookingSystem.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>(); 


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("PODBooking.Repositories")
    ));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["GoogleCalendar:ClientId"];
        options.ClientSecret = builder.Configuration["GoogleCalendar:ClientSecret"];
        options.Scope.Add("openid");
        options.Scope.Add("https://www.googleapis.com/auth/userinfo.email");
        options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
    options.AddPolicy("CustomerOnly", policy => policy.RequireRole("Customer"));
});


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<BookingService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Bật xác thực
app.UseAuthorization();   // Bật phân quyền

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
