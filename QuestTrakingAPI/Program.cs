using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using QuestTrakingAPI.DataBase.Services;
using QuestTrakingAPI.Services.Interfaces;
using QuestTrakingAPI.Services.Realisation;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IQuestServices, QuestService>();
builder.Services.AddScoped<IUserServices, UserService>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
