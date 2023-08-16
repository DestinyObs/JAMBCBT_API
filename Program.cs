using JAMBAPI.Authorization;
using JAMBAPI.Data;
using JAMBAPI.Interface;
using JAMBAPI.Repositories;
using JAMBAPI.SeedData;
using JAMBAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JAMBAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("LecturerOnly", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new LecturerRequirement());
                });
            });

            // Register the custom authorization handler
            builder.Services.AddScoped<IAuthorizationHandler, LecturerAuthorizationHandler>();


            //linking to the DbContext in Mysql
            builder.Services.AddDbContext<JambDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyString"));
            });

            //linking the STMP setting
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SmtpSettings>>().Value);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<ILecturerRepository, LecturerRepository>();
            builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
            builder.Services.AddScoped<IQuizRepository, QuizRepository>();
            builder.Services.AddScoped<ISuperAdminRepository, SuperAdminRepository>();
            

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
            {
                 SeedSuperAdmin.Initialize(app);
                //Seed.SeedData(app);
            }

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
        }
    }
}