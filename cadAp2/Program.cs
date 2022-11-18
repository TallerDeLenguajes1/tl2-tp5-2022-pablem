
using AutoMapper;
using Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddLogging();

/*Inyección Repositorios*/
builder.Services.AddTransient<IRepositorioCadete, RepositorioCadeteSQLite>();
builder.Services.AddTransient<IRepositorioCliente, RepositorioClienteSQLite>();
builder.Services.AddTransient<IRepositorioPedido, RepositorioPedidoSQLite>();

/* Auto Mapper Configurations  */
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new PerfilDeMapeo());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

/*https://learn.microsoft.com/es-es/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-6.0&tabs=visual-studio-code*/
