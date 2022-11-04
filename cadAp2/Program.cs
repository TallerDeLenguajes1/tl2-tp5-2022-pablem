
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
 
builder.Services.AddAutoMapper(new PerfilDeMapeo);
// Auto Mapper Configurations ---->CONSULTA
// var mapperConfig = new MapperConfiguration(mc =>
// {
//     mc.AddProfile(new PerfilDeMapeo());
// });
// IMapper mapper = mapperConfig.CreateMapper();
// builder.Services.AddSingleton(mapper);
// builder.Services.AddMvc();
// CONSULTA


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
