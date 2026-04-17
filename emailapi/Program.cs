using emailapi.Business.Services;
using emailapi.Services;
using EmailApi.Data;
using EmailApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Detect environment
var isDevelopment = builder.Environment.IsDevelopment();

// SQLite path (different for dev vs prod)
var dbPath = isDevelopment
    ? "studyapp.db"                    // local file
    : "/home/data/studyapp.db";        // Azure persistent storage

// Ensure directory exists in production
if (!isDevelopment)
{
    Directory.CreateDirectory("/home/data");
}

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()   // 🌍 ANY domain
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UsersServices>();
builder.Services.AddScoped<UserTaskAndServices>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<AdminService>();
builder.Services.AddSingleton<OtpService>();
var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // This ensures it stays at /swagger
});
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
