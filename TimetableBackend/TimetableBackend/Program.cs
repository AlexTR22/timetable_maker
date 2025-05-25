using TimetableBackend.Service;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.Cookie.Name = ".MyApp.Session";
//    options.IdleTimeout = TimeSpan.FromHours(1);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
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
//app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();
