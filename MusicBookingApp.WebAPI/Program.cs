using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicBookingApp.Application.Interfaces;
using MusicBookingApp.Application.IServices;
using MusicBookingApp.Application.Mappings;
using MusicBookingApp.Application.Settings;
using MusicBookingApp.Application.Validators;
using MusicBookingApp.Domain.Entities;
using MusicBookingApp.Persistence.DBContext;
using MusicBookingApp.Persistence.Repositories;
using MusicBookingApp.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure DbContext
builder.Services.AddDbContext<MusicBookingDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MusicBookingDbContext>()
    .AddDefaultTokenProviders();

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(ArtistValidator).Assembly);
builder.Services.AddFluentValidationClientsideAdapters();

// Register Generic Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//Register Specific Repository
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// Register Services
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHttpClient<IFlutterwaveService, FlutterwaveService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.Configure<FlutterwaveSettings>(builder.Configuration.GetSection("Flutterwave"));


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

app.MapControllers();

app.Run();
