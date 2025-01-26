using AutoMapper;
using HomeProjectAPI.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace HomeProjectAPI.API.Controllers.V1;

public class NotesController
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<NotesController> _logger;
    
    public NotesController(IUserService service, IMapper mapper, ILogger<NotesController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }   
    
    
    
    
    
}