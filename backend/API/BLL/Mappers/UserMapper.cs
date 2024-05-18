using API.BLL.DTOs.UserDTOs;
using API.DAL.Entities;

namespace API.BLL.Mappers;

public static class UserMapper
{
    public static User ToUserTable(this RegisterStudentDto registerStudentDto)
    {
        return new User
        {
            Name = registerStudentDto.Name,
            Surname = registerStudentDto.Surname,
            Patronymic = registerStudentDto.Patronymic,
            GroupTag = registerStudentDto.GroupTag.ToString(),
            Email = registerStudentDto.Email,
            CreationDate = DateTime.UtcNow.AddHours(7),
            UserName = registerStudentDto.Email
        };
    }

    public static User ToUserTable(this RegisterUserDto registerUserDto)
    {
        return new User
        {
            Name = registerUserDto.Name,
            Surname = registerUserDto.Surname,
            Patronymic = registerUserDto.Patronymic,
            GroupTag = null,
            Email = registerUserDto.Email,
            CreationDate = DateTime.UtcNow.AddHours(7),
            UserName = registerUserDto.Email
        };
    }
}
