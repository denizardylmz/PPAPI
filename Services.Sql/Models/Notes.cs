using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;

public class Note
{
    [Column("id")]
    public int Id { get; set; }
    [Column("userid")]
    public int UserId { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("content")]
    public string Content { get; set; } 
    [Column("createdat")]
    public DateTime CreatedAt { get; set; } 
    [Column("updatedat")]
    public DateTime UpdatedAt { get; set; }
    
    public List<NoteTag>? NoteTags { get; set; }
    public User? User { get; set; }
}