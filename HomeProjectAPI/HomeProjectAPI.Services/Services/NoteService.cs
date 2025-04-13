using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeProjectAPI.API.Common.Settings;
using HomeProjectAPI.Services.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using SM = HomeProjectAPI.Services.Model;
using  SqlM = Services.Sql.Models;
using Microsoft.Extensions.Options;



namespace HomeProjectAPI.Services.Services;

public class NoteService :INoteService
{
    private AppSettings _settings;
    private readonly IMapper _mapper;
    private readonly SqlM.EvdbContext _context;
    
    public NoteService(IOptions<AppSettings> settings, IMapper mapper, SqlM.EvdbContext context)
    {
        _settings = settings?.Value;
        _mapper = mapper;
        _context = context;
    }

    #region Note CRUDS   
    public async Task<SM.Note> CreateNote(SM.Note note)
    {
        var user = note.User;
        note.User = null;
        if(note.NoteTags.Count != 0) note.NoteTags.ForEach(x => x.Tag = null );
        
        var mappedNote = _mapper.Map<SqlM.Note>(note);
        await _context.Notes.AddAsync(mappedNote);
        
        int result = 0;
        result = await _context.SaveChangesAsync();

        if (result > 0)
        {
            var dbNote = await _context.Notes.Where(x => x.Id == mappedNote.Id)
                                        .Include(x => x.User)
                                        .Include(x => x.NoteTags)
                                        .ThenInclude(x => x.Tag)
                                        .FirstOrDefaultAsync();
            return _mapper.Map<SM.Note>(dbNote);
        }
        return null;
    }
    public async Task<SM.Note> UpdateNote(SM.Note note)
    {
        var mappedNote = _mapper.Map<SqlM.Note>(note);
        _context.Notes.Update(mappedNote);
        
        int result = 0;
        result = await _context.SaveChangesAsync();
        
        return result == 1 ? _mapper.Map<SM.Note>(mappedNote) : null;
    }
    public async Task<int> DeleteNote(int id) => await _context.Notes.Where(x => x.Id == id).ExecuteDeleteAsync();
    public async Task<int> DeleteNotes(List<int> ids) => await _context.Notes.Where(x =>ids.Contains((x.Id))).ExecuteDeleteAsync();
    public async Task<SM.Note> GetNoteById(int id)
    {
        var note = await _context.Notes.Where(x => x.Id == id)
                                         .Include(x => x.User)
                                         .Include(x => x.NoteTags)
                                         .ThenInclude(x => x.Tag)
                                        .FirstOrDefaultAsync();
        return note != null ? _mapper.Map<SM.Note>(note) : null;
    } 
    public async Task<List<SM.Note>> GetNoteByIds(List<int> ids)
    {
        var note = await _context.Notes.Where(x =>  ids.Contains(x.Id))
                                        .Include(x => x.User)
                                        .Include(x => x.NoteTags)
                                        .ThenInclude(x => x.Tag)
                                        .FirstOrDefaultAsync();
        return note != null ? _mapper.Map<List<SM.Note>>(note) : null;
    } 

    #endregion
    

}