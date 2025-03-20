using PetMongoDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetMongoDB.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PetMongoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetMongoDBContext") ?? throw new InvalidOperationException("Connection string 'PetMongoDBContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//fazendo a conex?o do servi?o
builder.Services.AddControllersWithViews();

ContextMongoDB.ConnectionString = builder.Configuration.GetSection("MongoConnection:ConnectionString").Value;
ContextMongoDB.Database = builder.Configuration.GetSection("MongoConnection:Database").Value;
ContextMongoDB.IsSSL = Convert.ToBoolean(builder.Configuration.GetSection("MongoConnection:IsSSL").Value);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
