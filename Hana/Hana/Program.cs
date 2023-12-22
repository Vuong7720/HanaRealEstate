using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hana.Data;
using Microsoft.EntityFrameworkCore;
using Hana.Services;
using Commom;
using Westwind.AspNetCore.LiveReload;
using Hana.Models.DataModels;
using Microsoft.AspNetCore.Identity;
using Hana.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddDbContext<HanaContext>(opts =>
    opts.UseSqlServer(builder.Configuration["ConnectionString:ConnectStr"]));

//builder.Services.AddControllersWithViews()
//    .AddNewtonsoftJson(options =>
//        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});

// Register services
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
builder.Services.AddScoped<IAboutUsServices, AboutUsServices>();
builder.Services.AddScoped<IFAQServices, FAQServices>();
builder.Services.AddScoped<ILevelServices, LevelServices>();
builder.Services.AddScoped<IPolicyServices, PolicyServices>();
builder.Services.AddScoped<IRealEstateTypeServices, RealEstateTypeServices>();
builder.Services.AddScoped<IAgentServices, AgentServices>();
builder.Services.AddScoped<ICommonServices, CommonServices>();
builder.Services.AddScoped<IVerification, Verification>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddSignalR();
builder.Services.AddLiveReload();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddMvc().AddRazorRuntimeCompilation();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("MyCookieScheme")
    .AddCookie("MyCookieScheme", options =>
    {
        options.AccessDeniedPath = new PathString("/AdminArea/Account/Denied");
        options.Cookie = new CookieBuilder
        {
            HttpOnly = true,
            Name = "Hana.Cookie",
            SameSite = SameSiteMode.Lax,
            SecurePolicy = CookieSecurePolicy.SameAsRequest
        };
        options.LoginPath = new PathString("/dang-nhap");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "AdminArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
