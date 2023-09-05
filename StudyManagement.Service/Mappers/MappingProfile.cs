using AutoMapper;
using StudyManagement.Domain.Entities.Grades;
using StudyManagement.Domain.Entities.Sciences;
using StudyManagement.Domain.Entities.Students;
using StudyManagement.Domain.Entities.StudentSciences;
using StudyManagement.Domain.Entities.Teachers;
using StudyManagement.Service.DTOs.Grades;
using StudyManagement.Service.DTOs.Sciences;
using StudyManagement.Service.DTOs.Students;
using StudyManagement.Service.DTOs.StudentSciences;
using StudyManagement.Service.DTOs.Teachers;

namespace StudyManagement.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile() 
    { 
        CreateMap<GradeEntity, GradeCreationDTO>().ReverseMap();
        CreateMap<GradeEntity, GradeUpdateDTO>().ReverseMap();
        CreateMap<GradeEntity, GradeResultDTO>().ReverseMap();

        CreateMap<ScienceEntity, ScienceCreationDTO>().ReverseMap();
        CreateMap<ScienceEntity, ScienceUpdateDTO>().ReverseMap();
        CreateMap<ScienceEntity, ScienceResultDTO>().ReverseMap();

        CreateMap<StudentEntity, StudentCreationDTO>().ReverseMap();
        CreateMap<StudentEntity, StudentUpdateDTO>().ReverseMap();
        CreateMap<StudentEntity, StudentResultDTO>().ReverseMap();

        CreateMap<StudentScienceEntity, StudentScienceCreationDTO>().ReverseMap();
        CreateMap<StudentScienceEntity, StudentScienceUpdateDTO>().ReverseMap();
        CreateMap<StudentScienceEntity, StudentScienceResultDTO>().ReverseMap();

        CreateMap<TeacherEntity, TeacherCreationDTO>().ReverseMap();
        CreateMap<TeacherEntity, TeacherUpdateDTO>().ReverseMap();
        CreateMap<TeacherEntity, TeacherResultDTO>().ReverseMap();
    }
}
