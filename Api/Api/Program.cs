using Api.Data;
using Api.Repositories;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(cs));

// Repositories
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
//builder.Services.AddScoped<IPrescriptionDetailRepository, PrescriptionDetailRepository>();
//builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
//builder.Services.AddScoped<IPatientRepository, PatientRepository>();
//builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();

// Services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
//builder.Services.AddScoped<IPrescriptionDetailService, PrescriptionDetailService>();
//builder.Services.AddScoped<IDoctorService, DoctorService>();
//builder.Services.AddScoped<IPatientService, PatientService>();
//builder.Services.AddScoped<IMedicineService, MedicineService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt => opt.AddPolicy("ng", p => p
.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await Api.Data.Seed.RunAsync(db);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

app.UseCors("ng");
//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
