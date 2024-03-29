using Microsoft.EntityFrameworkCore;
using webapinet.Controllers;

//var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:8081");                                            
                          policy.AllowCredentials();
                      });
});*/
// Add services to the container.

builder.Services.AddDbContext<PizzaDbContext>(
        options => options.UseInMemoryDatabase("DBInMemory"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

//app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();



app.UseMiddleware<webapinet.Controllers.ExceptionMiddleware>();

app.Run();
