using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftTeK.BusinessAdvisors.Api.Helpers;
using SoftTeK.BusinessAdvisors.Data.Interface;
using SoftTeK.BusinessAdvisors.Data.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var corsName = "MyCorsEnable";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsName,
                      policy =>
                      {
                          policy.WithOrigins(
                              builder.Configuration["ClientPath1"]!,
                              builder.Configuration["ClientPath2"]!,
                              builder.Configuration["ClientPath3"]!)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                          .WithExposedHeaders("*");
                      });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>
    (o => o.UseInMemoryDatabase("UserDB"));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBuildTokenJwt, BuildTokenJwt>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, //Emisores
            ValidateAudience = false, // Audiencia, receptores
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["jwtkey"]!)),
            ClockSkew = TimeSpan.Zero
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsName);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
