using AutoMapper;
using HomeProjectAPI.API.Common.Settings;
using HomeProjectAPI.Services.Contracts.Services;
using Microsoft.Extensions.Options;
using Services.Sql.Models;

namespace HomeProjectAPI.Services.Services;

public class NoteService :INoteService
{
    private AppSettings _settings;
    private readonly IMapper _mapper;
    private readonly EvdbContext _context;


    public NoteService(IOptions<AppSettings> settings, IMapper mapper, EvdbContext context)
    {
        _settings = settings?.Value;
        _mapper = mapper;
        _context = context;
    }

    
    
}