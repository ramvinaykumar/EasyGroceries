using EasyGroceries.E_Commerce.Api.Helpers;
using EasyGroceries.E_Commerce.Api.Interfaces;
using EasyGroceries.E_Commerce.Api.Repository.Interfaces;
using EasyGroceries.E_Commerce.Api.Repository.Services;
using EasyGroceries.E_Commerce.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// 2. Database Configuration
builder.Services.AddSingleton<DatabaseService>();

// 3. Repository Configuration
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

// 4. Business Logic Services
builder.Services.AddScoped<IProductDataServices, ProductDataServices>();
builder.Services.AddScoped<IOrderDataServices, OrderDataServices>();
builder.Services.AddScoped<ICustomerDataServices, CustomerDataServices>();

var app = builder.Build();

// 5. Configure Configure the HTTP request pipeline and Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 6. Map Endpoints
app.MapControllers();

app.Run();
