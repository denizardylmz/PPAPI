using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;
[Table("USERS")]
public class User
{
    [Column("ID")]
    public int Id { get; set; }
    [Column("USERNAME")]
    public string Username { get; set; }
    [Column("PASSWORD")]
    public string Password { get; set; }
    [Column("NAME")]
    public string Name { get; set; }
    [Column("SURNAME")]
    public string Surname { get; set; }
    [Column("MAIL")]
    public string Mail { get; set; }
}