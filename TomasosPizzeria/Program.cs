using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TomasosPizzeria.Api.Extensions;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Core.Services;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Identity;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Data.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationUserContext>()
       .AddDefaultTokenProviders();

builder.Services.AddJwtAuthentication(builder.Configuration);

//DI Container
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddSwaggerExtended();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwaggerExtended();

app.Run();
