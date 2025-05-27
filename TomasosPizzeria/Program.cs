using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TomasosPizzeria.Api.Extensions;
using TomasosPizzeria.Api.Seeding;
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

var keyVaultUri = builder.Configuration.GetValue<string>("KeyVault:KeyVaultURL");

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

builder.Services.AddApplicationDependencies();

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

    var userDb = services.GetRequiredService<ApplicationUserContext>();
    userDb.Database.Migrate();

    var pizzaDb = services.GetRequiredService<PizzaAppContext>();
    pizzaDb.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RoleSeeder.SeedRolesAsync(services);
}

app.Run();
