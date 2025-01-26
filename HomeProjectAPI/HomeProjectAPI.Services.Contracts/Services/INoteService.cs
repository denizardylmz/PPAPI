using System.Collections.Generic;
using System.Threading.Tasks;
using SM = HomeProjectAPI.Services.Model;
using  SqlM = Services.Sql.Models;

namespace HomeProjectAPI.Services.Contracts.Services;

public interface INoteService
{
    Task<SM.Note> CreateNote(SM.Note note);
    Task<SM.Note> UpdateNote(SM.Note note);
    Task<int> DeleteNote(int id);
    Task<int> DeleteNotes(List<int> ids);
    Task<SM.Note> GetNoteById(int id);
    Task<List<SM.Note>> GetNoteByIds(List<int> ids);
}