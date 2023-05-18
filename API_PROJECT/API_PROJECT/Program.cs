using API_PROJECT.Model;
using API_PROJECT.Models;
using API_PROJECT.reposatory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => option.AddPolicy("my", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddScoped<Ireposatory<Brand>, reposatory<Brand>>();
builder.Services.AddScoped<Ireposatory<Cart>, reposatory<Cart>>();
builder.Services.AddScoped<Ireposatory<Category>, reposatory<Category>>();
builder.Services.AddScoped<Ireposatory<Category_Brand>, reposatory<Category_Brand>>();
builder.Services.AddScoped<Ireposatory<Customer>, reposatory<Customer>>();
builder.Services.AddScoped<Ireposatory<Delivary>, reposatory<Delivary>>();
builder.Services.AddScoped<Ireposatory<Offer>, reposatory<Offer>>();
builder.Services.AddScoped<Ireposatory<Order>, reposatory<Order>>();
builder.Services.AddScoped<Ireposatory<OrderDetailes>, reposatory<OrderDetailes>>();
builder.Services.AddScoped<Ireposatory<Product>, reposatory<Product>>();
builder.Services.AddScoped<Ireposatory<Review>, reposatory<Review>>();
builder.Services.AddScoped<Ireposatory<Supplier>, reposatory<Supplier>>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<DelivaryReposatory>();

builder.Services.AddDbContext<Context>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
builder.Services.AddDbContext<Context>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
          );
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>();
builder.Services.AddAuthentication(options =>
{
    //jwt
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//not valid account
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        //parmeter
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIss"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAud"],
        IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecrytKey"]))//asdZXCZX!#!@342352
    };
})  //how to check if toke valid or not
    ;
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ASP.NET 5 Web API",
        Description = " ITI Projrcy"
    });
    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
new string[] { }

                    }
                });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();    ////////////to  make all file in rooot to work fine
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("my");

app.MapControllers();

app.Run();
