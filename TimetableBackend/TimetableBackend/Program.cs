using TimetableBackend.Service;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===== Session / Auth =====
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});


//database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString == null)
{
    Console.Error.WriteLine("No database connection string found");
}
else
{
    builder.Services.AddSingleton(new Helper(connectionString));
}
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<ProfessorService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<TimeConstraintService>();
builder.Services.AddScoped<CollegeService>();
builder.Services.AddScoped<UniversityService>();
builder.Services.AddScoped<UserService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // React frontend URL
            //.AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



//builder.Services.AddAuthorization(options =>
//{
//    // Generic – we can still use [Authorize(Roles="teacher")]
//    options.AddPolicy("TeacherOnly", policy => policy.RequireRole("teacher"));
//    options.AddPolicy("StudentOnly", policy => policy.RequireRole("student"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
