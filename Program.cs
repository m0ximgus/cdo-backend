using Kursach.Data;
using Kursach.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlite<StudentContext>("Data Source=Kursach.db");
builder.Services.AddSqlite<AuthContext>("Data source = Kursach.db");

builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.UseCors("AllowAllOrigins");

app.CreateDbIfNotExists();

app.MapGet("/", () => @"Use /swagger to see things.");

app.Run();
