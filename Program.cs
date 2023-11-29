using Kursach.Data;
using Kursach.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KursachContext>();

builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<JobTitleService>();
builder.Services.AddScoped<JournalService>();
builder.Services.AddScoped<LessonService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<TeacherService>();

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