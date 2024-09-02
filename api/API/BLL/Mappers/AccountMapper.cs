using API.BLL.DTOs.AccountDTOs;
using API.DAL.Entities;

namespace API.BLL.Mappers;

public static class AccountMapper
{
    public static User ToUserTable(this RegisterDto registerDto)
    {
        return new User
        {
            Name = registerDto.Name,
            Surname = registerDto.Surname,
            Patronymic = registerDto.Patronymic,
            Email = registerDto.Email,
            CreationTime = DateTime.UtcNow.AddHours(7)
        };
    }
}
