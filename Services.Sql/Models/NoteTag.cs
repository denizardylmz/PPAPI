using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Sql.Models;

[Table("NoteTags")]
public class NoteTag
{
    [Column("NOTEID")]
    public int NoteId { get; set; }
    [Column("TAGID")]
    public int TagId { get; set; }
    
    public Tag Tag { get; set; }
}