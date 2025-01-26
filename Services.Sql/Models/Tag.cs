using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;

[Table("Tags")]
public class Tag
{
    [Column("ID")]
    public int Id { get; set; }
    [Column("NAME")]
    public string Name { get; set; }
    
}