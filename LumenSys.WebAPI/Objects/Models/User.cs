using LumenSys.Objects.Enums;
using LumenSys.WebAPI.Objects.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LumenSys.WebAPI.Objects;

[Table("user")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("cpf")]
    public string? Cpf { get; set; }
    [Column("phone")]
    public string? Phone { get; set; }
    [Column("typeemployee")]
    public TypeEmployee TypeEmployee { get; set; }
    [Column("userstatus")]
    public UserStats UserStatus { get; set; }
    [Column("hiredate")]
    public DateOnly HireDate { get; set; }
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
    public ICollection<Funeral> Funeral { get; set; } = new List<Funeral>();
    public ICollection<Client> Client { get; set; } = new List<Client>();
    public ICollection<Thanatopraxia> Thanatopraxia { get; set; } = new List<Thanatopraxia>();
    public User () { }
    
    public User (int id, string name, string email, string password, string cpf, string phone, DateOnly hiredate,TypeEmployee typeemployee, UserStats userstatus) 
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Cpf = cpf;
        Phone = phone;
        HireDate = hiredate;
        TypeEmployee = typeemployee;
        UserStatus = userstatus;
    }
}