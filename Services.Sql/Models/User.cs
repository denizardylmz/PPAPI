using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("username")]
    public string Username { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("surname")]
    public string Surname { get; set; }
    [Column("mail")]
    public string Mail { get; set; }
}