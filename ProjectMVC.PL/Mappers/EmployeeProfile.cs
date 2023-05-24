using AutoMapper;
using DataAccessLayer.Models;
using ProjectMVC.PL.ViewModel;

namespace ProjectMVC.PL.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
