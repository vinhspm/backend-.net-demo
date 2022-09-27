using MISA.Web08.AMIS.BL;
using MISA.Web08.AMIS.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();

DataContext.MySqlConnectionString = builder.Configuration.GetConnectionString("MySqlConnectionString");

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
