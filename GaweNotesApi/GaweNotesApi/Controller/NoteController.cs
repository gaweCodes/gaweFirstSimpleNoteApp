using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GaweNotesApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using GaweNotesApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace GaweNotesApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;
        public NoteController(NoteService noteService) =>_noteService = noteService;

        [HttpGet]
        public async Task<ActionResult<List<NoteDto>>> Get([FromQuery] Guid userId)
        {
            var noteDtos = new List<NoteDto>();
            var notes = await _noteService.GetAll(userId);
            notes.ForEach(note => noteDtos.Add(new NoteDto{Id = note.Id, Title = note.Title, Text = note.Text, LastModifiedAt = note.LastModifiedAt, UserId = userId}));
            return noteDtos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetById(Guid id)
        {
            var note = await _noteService.GetById(id);
            if (note == null) return NotFound();
            return new ActionResult<NoteDto>(new NoteDto
            {
                Id = note.Id, Title = note.Title, Text = note.Text, LastModifiedAt = note.LastModifiedAt,
                UserId = note.User.Id
            });
        }
        [HttpPost]
        public async Task<ActionResult> Post(NoteDto note) => await _noteService.Add(note);
        [HttpPut]
        public async Task<ActionResult> Put(NoteDto note) => await _noteService.Update(note);
        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] Guid noteId) => await _noteService.Delete(noteId);
    }
}
