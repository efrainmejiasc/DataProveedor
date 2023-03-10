
using DatosGTMWeb.Filters;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ModeloDataProveedor.DataModel;
using ModeloDataProveedor.IRepositories;
using ModeloDataProveedor.Repositories;
using NegocioDataProveedor.Helpers;
using NegocioDataProveedor.IServices;
using NegocioDataProveedor.Services;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new CustomAuthenticationFilter());
    //config.Filters.Add(new CustomAuthorizationFilter());
});

builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc();
#if DEBUG
builder.Services.AddDbContext<MyAppContext>(
    op => op.UseSqlServer(@"Server=EMCSERVERHP\SQLEXPRESS;DataBase=DataProveedor;User Id=sa;Password=1234santiago;MultipleActiveResultSets=false;Connection Timeout=120;TrustServerCertificate=True;",
    b => b.MigrationsAssembly("ModeloDataProveedor")));
#else 
builder.Services.AddDbContext<MyAppContext>(
    op => op.UseSqlServer(@"Server=SQL5101.site4now.net;DataBase=db_a8f8a1_datosgtm;User Id=db_a8f8a1_datosgtm_admin;Password=1234santiago;MultipleActiveResultSets=false;Connection Timeout=120;TrustServerCertificate=True;",
    b => b.MigrationsAssembly("ModeloDataProveedor")));
#endif

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<MyAppContext, MyAppContext>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

AppEnviroments.log_excepcion = builder.Configuration["AppEnviroments:log_excepcion"];

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.UseSession();
app.Run();

//******************************************************************************
var culturaInglesa = "en-US";
var culturaEspa?ola = "es-ES";
var ci = new CultureInfo(culturaInglesa);
ci.NumberFormat.NumberDecimalSeparator = ".";
ci.NumberFormat.CurrencyDecimalSeparator = ".";
var ce = new CultureInfo(culturaEspa?ola);
ce.NumberFormat.NumberDecimalSeparator = ",";
ce.NumberFormat.CurrencyDecimalSeparator = ",";
CultureInfo.DefaultThreadCurrentCulture = ci;
CultureInfo.DefaultThreadCurrentUICulture = ci;

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ci),
    SupportedCultures = new List<CultureInfo>
    {
     ci,ce
    },
    SupportedUICultures = new List<CultureInfo>
    {
    ci,ce
    }
});
//******************************************************************************
