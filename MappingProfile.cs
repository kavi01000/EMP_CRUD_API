using AutoMapper;
using EmpList.Model;
using EmpList.ModelDTO;

namespace Emplist.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        
           
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<MDepartment, MDepartmentDto>().ReverseMap();
            CreateMap<MQualification, MQualificationDto>().ReverseMap();

       
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.DepartmentCode))
                .ForMember(dest => dest.QualificationName, opt => opt.MapFrom(src => src.Qualification.QualificationName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Contact.Email))
                .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(src => src.Contact.PhoneNo))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Contact.Age))
                .ForMember(dest => dest.PinCode, opt => opt.MapFrom(src => src.Contact.PinCode))
                .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.Contact.DOB));

          
        }
    }
}