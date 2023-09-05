using StudyManagement.Data.IRepositories;
using StudyManagement.Data.Repositories;
using StudyManagement.Service.Interfaces;
using StudyManagement.Service.Mappers;
using StudyManagement.Service.Services;

namespace StudyManagement.WebApi.Extentions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IGradeService, GradeService>();
        services.AddScoped<IScienceService, ScienceService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentScienceService, StudentScienceService>();
        services.AddScoped<ITeacherService, TeacherService>();

        services.AddAutoMapper(typeof(MappingProfile));
    }
}
