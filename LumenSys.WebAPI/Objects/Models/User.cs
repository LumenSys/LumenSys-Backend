using LumenSys.Objects.Enums;
using LumenSys.WebAPI.Objects.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects;

[Table("user")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("typeemployee")]
    public TypeEmployee TypeEmployee { get; set; }
    [Column("userstatus")]
    public UserStats UserStatus { get; set; }

    public int EmployeeId  { get; set; }

    public Employee Employee { get; set; } = null!;

    public User () { }

    public User (int id, string email, string password, TypeEmployee typeemployee, UserStats userstatus) 
    {
        Id = id;
        Email = email;
        Password = password;
        TypeEmployee = typeemployee;
        UserStatus = userstatus;
    }
}