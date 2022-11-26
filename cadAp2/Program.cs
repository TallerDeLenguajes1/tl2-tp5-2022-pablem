
using AutoMapper;
using CadenasDeconexion;
using Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
// builder.Services.AddControllers();
// builder.Services.AddLogging();

/*Inyección Repositorios*/
builder.Services.AddSingleton<ICadenaDeConexion, CadenaParaSqlite>();
builder.Services.AddTransient<IRepositorioCadete, RepositorioCadeteSQLite>();
builder.Services.AddTransient<IRepositorioCliente, RepositorioClienteSQLite>();
builder.Services.AddTransient<IRepositorioPedido, RepositorioPedidoSQLite>();
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuarioSQlite>();

/* Auto Mapper Configurations  */
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new PerfilDeMapeo());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

/* set up the in-memory session provider with a default in-memory implementation of IDistributedCache: */
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MiSesion.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/*https://learn.microsoft.com/es-es/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-6.0&tabs=visual-studio-code*/
