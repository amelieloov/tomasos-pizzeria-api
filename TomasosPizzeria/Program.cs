using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TomasosPizzeria.Api.Extensions;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<PizzaAppContext>(
   options => options.UseSqlServer(@"Data Source=MSI;Initial Catalog=TomasosPizzaDB;Integrated Security=SSPI;TrustServerCertificate=True;")
);

builder.Services.AddDbContext<ApplicationUserContext>(options =>
    options.UseSqlServer(@"Data Source=MSI;Initial Catalog=TomasosPizzaDB;Integrated Security=SSPI;TrustServerCertificate=True;"));
//builder.Configuration.GetConnectionString("DefaultConnection"))

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationUserContext>()
       .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
