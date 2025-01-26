namespace HomeProjectAPI.Services.Model;

public class NoteTag
{
    public int NoteId { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}