using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TomasosPizzeria.Api.Extensions;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Core.Services;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Identity;
using TomasosPizzeria.Domain.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<PizzaAppContext>(options =>
    options.UseSqlServer(@"Data Source=MSI;Initial Catalog=TomasosPizzaDB;Integrated Security=SSPI;TrustServerCertificate=True;"));

builder.Services.AddDbContext<ApplicationUserContext>(options =>
    options.UseSqlServer(@"Data Source=MSI;Initial Catalog=TomasosPizzaDB;Integrated Security=SSPI;TrustServerCertificate=True;"));
//builder.Configuration.GetConnectionString("DefaultConnection"))

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationUserContext>()
       .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);

//DI Container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();

builder.Services.AddSwaggerExtended();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwaggerExtended();

app.Run();
