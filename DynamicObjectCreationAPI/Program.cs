using DynamicData.Abstract;
using DynamicData.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5219/",
                                              "http://localhost:5292")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowAnyOrigin();
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Micromarin API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var config = builder.Configuration;
    string? connStr = config.GetConnectionString("mysql_connection");
    options.UseMySql(connStr, new MySqlServerVersion(new Version(8, 3, 0)), options => options.MigrationsAssembly("DynamicObjectCreationAPI"));
});

builder.Services.AddScoped<IDynamicTableRepository, EfDynamicTableRepository>();
builder.Services.AddScoped<IDynamicFieldRepository, EfDynamicFieldRepository>();
builder.Services.AddScoped<IDynamicProductRepository, EfDynamicProductRepository>();
builder.Services.AddScoped<IDynamicCategoryRepository, EfDynamicCategoryRepository>();
builder.Services.AddScoped<IDynamicSubCategoryRepository, EfDynamicSubCategoryRepository>();
builder.Services.AddScoped<IDynamicBrandRepository, EfDynamicBrandRepository>();
builder.Services.AddScoped<IDynamicOrderProductRepository, EfDynamicOrderProductRepository>();
builder.Services.AddScoped<IDynamicOrderRepository, EfDynamicOrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
