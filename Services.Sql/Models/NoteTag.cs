using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;

public class NoteTag
{
    [Column("noteid")]
    public int NoteId { get; set; }
    [Column("tagid")]
    public int TagId { get; set; }
    
    public Tag Tag { get; set; }
}