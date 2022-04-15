using Microsoft.Extensions.Options;
using MongoDB.Driver;

using Backend.Models;
using Backend.Services;

using Microsoft.AspNetCore.Mvc;
[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectionURI")));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
