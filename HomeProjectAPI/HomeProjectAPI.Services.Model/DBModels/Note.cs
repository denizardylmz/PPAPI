using System;
using System.Collections.Generic;

namespace HomeProjectAPI.Services.Model;

public class Note
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; } 
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
    
    public List<NoteTag> NoteTags { get; set; }
}