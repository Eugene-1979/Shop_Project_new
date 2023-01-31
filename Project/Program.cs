using _07_CustomMiddlewareComponent;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shop_Project.Data;
using Shop_Project.Db;
using Shop_Project.Filter;
using Shop_Project.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// ���������� �������� Idenity
builder.Services.AddDefaultIdentity<IdentityUser>
(
/*options => options.SignIn.RequireConfirmedAccount = true*/
options => { 
    options.Password.RequireDigit = true; /*����� �����������*/
    options.Password.RequiredLength = 5;/* ��� ����� */
    options.Password.RequireUppercase = true; /*���� ���� �������*/
    options.Lockout.MaxFailedAccessAttempts= 5; /*����������� ������� ����� ���������� ��������*/
    options.User.RequireUniqueEmail= true; /*��������� �����*/
    options.SignIn.RequireConfirmedEmail = false;/* ������������� ����*/
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();











builder.Services.AddControllersWithViews();

/*MyDatabase*/
IConfigurationRoot conString = builder.Configuration.AddJsonFile("appsettingsShop.json").Build();
builder.Services.AddDbContext<AppDbContent>(option => option.
UseSqlServer(conString.GetConnectionString("DefaultConnection")));

/*Loging*/
builder.Logging.ClearProviders();
builder.Logging.AddConsole();





/*MyExtention method for IoS*/
builder.Services.CreateServiceCollection();













var app = builder.Build();


using IServiceScope serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
SeedData.EnsureSeedData(serviceScope.ServiceProvider);






// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
    {
    app.UseMigrationsEndPoint();
    }
else
    {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

/*�������������� � �����������*/
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

/*MyMiddleware*/
/*app.UseMiddleware<MyMiddleware>();*/






/*using(var scope = app.Services.CreateScope())
    {
    AppDbContent context = scope.ServiceProvider.GetRequiredService<AppDbContent>();


    if(context.Categorys.Count() == 0) { DbObjects.Initial(context); }

    context.Products.RemoveRange(context.Products);
    context.Categorys.RemoveRange(context.Categorys);
    context.Customers.RemoveRange(context.Customers);
    context.Employees.RemoveRange(context.Employees);
    context.Orders.RemoveRange(context.Orders);

    context.SaveChanges();
    }*/

app.Run();
public partial class Program { }
