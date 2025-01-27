using System.Collections.Generic;
 using System.Threading.Tasks;
 using Asp.Versioning;
 using AutoMapper;
 using HomeProjectAPI.Services.Contracts;
 using HomeProjectAPI.Services.Contracts.Services;
 using HomeProjectAPI.Services.Model;
 using Microsoft.AspNetCore.Http;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.Extensions.Logging;
 
 namespace HomeProjectAPI.API.Controllers.V1;
 
 [ApiVersion("1.0")]
 [Route("api/notes")] //required for default versioning
 [Route("api/v{version:apiVersion}/note")]
 [Consumes("application/json")]
 [Produces("application/json")]
 [ApiController]
 public class NoteController : Controller
 {
     private readonly INoteService _service;
     private readonly IMapper _mapper;
     private readonly ILogger<NoteController> _logger;
     
     public NoteController(INoteService service, IMapper mapper, ILogger<NoteController> logger)
     {
         _service = service;
         _mapper = mapper;
         _logger = logger;
     }   
     
     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Note))]
     [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Note))]
     [HttpGet("{id:int}")]
     public async Task<ActionResult<Note>> Get(int id)
     {
         var result = await _service.GetNoteById(id);
         return result == null ? NoContent() : Ok(result);
     }
     
     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Note>))]
     [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(List<Note>))]
     [HttpPost("lists")]
     public async Task<ActionResult<List<Note>>> Get([FromBody] List<int> ids)
     {
         var result = await _service.GetNoteByIds(ids);
         return result == null ? NoContent() : Ok(result);
     }
     
     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [HttpDelete("{id:int}")]
     public async Task<ActionResult<int>> Delete(int id)
     {
         var result = await _service.DeleteNote(id);
         return result == 0 ? NotFound() : Ok(result);
     }
     
     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<int>))]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [HttpDelete("list")]
     public async Task<ActionResult<int>> Delete([FromBody] List<int> ids)
     {
         var result = await _service.DeleteNotes(ids);
         return result == 0 ? NotFound() : Ok(result);
     }
     
     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Note))]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [HttpPut("")]
     public async Task<ActionResult<Note>> Update([FromBody] Note note)
     {
         var result = await _service.UpdateNote(note);
         return result == null ? NotFound() : Ok(result);
     }
     
     [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Note))]
     [ProducesResponseType(StatusCodes.Status404NotFound)]
     [HttpPost("")]
     public async Task<ActionResult<Note>> Create([FromBody] Note note)
     {
         var result = await _service.CreateNote(note);
         return result == null ? NotFound() : Ok(result);
     }
 
 }