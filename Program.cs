using Microsoft.EntityFrameworkCore;
using Market.Data;
using Microsoft.AspNetCore.Builder;
using Market.Services.Repository.IRepository;
using Market.Services.Repository;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddRazorPages();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Market API v1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseRouting();
//app.UseAuthorization();
app.MapControllers();
app.Run();