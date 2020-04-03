using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaweNotesApi.Database;
using GaweNotesApi.Dtos;
using GaweNotesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GaweNotesApi.Services
{
    public class NoteService
    {
        private readonly NoteDbContext _context;
        public NoteService(NoteDbContext context) => _context = context;
        public async Task<List<Note>> GetAll(Guid userId) => await _context.Notes.Include(x => x.User).Where(n => n.User.Id == userId).ToListAsync();
        public async Task<Note> GetById(Guid id)
        {
            var note = await _context.Notes.Include(x => x.User).SingleOrDefaultAsync(n => n.Id == id);
            return note;
        }

        public async Task<ActionResult> Add(NoteDto noteDto)
        {
            var note = new Note{Id = noteDto.Id, LastModifiedAt = noteDto.LastModifiedAt, Text = noteDto.Text, Title = noteDto.Title, User = await _context.Users.FindAsync(noteDto.UserId) };
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        public async Task<ActionResult> Update(NoteDto noteDto)
        {
            var note = await _context.Notes.FindAsync(noteDto.Id);
            note.LastModifiedAt = noteDto.LastModifiedAt;
            note.Title = noteDto.Title;
            note.Text = noteDto.Text;
            note.User = await _context.Users.FindAsync(noteDto.UserId);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        public async Task<ActionResult> Delete(Guid noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
