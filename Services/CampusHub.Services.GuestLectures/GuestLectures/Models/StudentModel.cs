using AutoMapper;
using CampusHub.Context.Entities;

namespace CampusHub.Services.GuestLectures;

public class StudentModel
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}

public class StudentModelProfile : Profile
{
    public StudentModelProfile()
    {
        CreateMap<User, StudentModel>();
    }
}