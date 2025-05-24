using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;
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

//var secrets = await builder.GetSecretsFromKeyVaultAsync("ConnString", "JwtSecretKey");

var keyVaultUri = builder.Configuration.GetValue<string>("KeyVault:KeyVaultURL");
//var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

builder.Configuration.AddAzureKeyVault(
    new Uri(keyVaultUri),
    new DefaultAzureCredential());

var connString = builder.Configuration["ConnString"];

builder.Services.AddDbContext<PizzaAppContext>(options =>
    options.UseSqlServer(connString));

builder.Services.AddDbContext<ApplicationUserContext>(options =>
    options.UseSqlServer(connString));

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleExtensions.SeedRolesAsync(services);
}

app.Run();
