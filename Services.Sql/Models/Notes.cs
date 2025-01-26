using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;

[Table("Notes")]
public class Note
{
    [Column("ID")]
    public int Id { get; set; }
    [Column("USERID")]
    public int UserId { get; set; }
    [Column("TITLE")]
    public string Title { get; set; }
    [Column("CONTENT")]
    public string Content { get; set; } 
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; } 
    [Column("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }
    
    public List<NoteTag> NoteTags { get; set; }
}