using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;

public class Tag
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    
}