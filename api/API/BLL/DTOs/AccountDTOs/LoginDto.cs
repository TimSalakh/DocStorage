﻿using System.ComponentModel.DataAnnotations;

namespace API.BLL.DTOs.AccountDTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
