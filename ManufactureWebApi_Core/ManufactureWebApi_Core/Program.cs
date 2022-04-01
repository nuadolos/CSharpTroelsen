using ManufactureEF_Core.Context;
using ManufactureEF_Core.DataInitializer;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using ManufactureEF_Core.Repos;
using ManufactureWebApi_Core.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region ���������������� ���������� ��� ��������� ������������

//�������� ���� ������� ��������� ������ DI
builder.Services.AddDbContextPool<ManufactureContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Manufacture"),
        o => o.EnableRetryOnFailure()));

//���������� UserRepo � RoleRepo � ��������� DI
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();

#endregion

//����������� ������� ���������� � ����������� � ����� Pascal
builder.Services.AddMvc(config => config.Filters.Add(new ManufactureExceptionFilter(builder.Environment)))
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

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

app.UseAuthorization();

app.MapControllers();

//��������� ������� �������� ���������� DI
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //���������� ������� ManufactureContext �� ����������
    var context = services.GetRequiredService<ManufactureContext>();

    Initializer.RecreateDatabase(context);
    Initializer.InitializeData(context);
}

app.Run();
