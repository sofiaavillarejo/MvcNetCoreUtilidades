using MvcNetCoreUtilidades.Helpers;
using MvcNetCoreUtilidades.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<HelperPathProvider>();
builder.Services.AddTransient<RepositoryCoches>();
// Add services to the container.
builder.Services.AddMemoryCache(); //necesario para almacenar en cache
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
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
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
