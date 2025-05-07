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
    [Column("cpf")]
    public string Cpf { get; set; }
    [Column("phone")]
    public string Phone { get; set; }
    [Column("typeemployee")]
    public TypeEmployee TypeEmployee { get; set; }
    [Column("userstatus")]
    public UserStats UserStatus { get; set; }
    [Column("hiredate")]
    public DateOnly HireDate { get; set; }

    public User () { }

    public User (int id, string email, string password, string cpf, string phone, TypeEmployee typeemployee, UserStats userstatus) 
    {
        Id = id;
        Email = email;
        Password = password;
        Cpf = cpf;
        Phone = phone;
        TypeEmployee = typeemployee;
        UserStatus = userstatus;
    }
}