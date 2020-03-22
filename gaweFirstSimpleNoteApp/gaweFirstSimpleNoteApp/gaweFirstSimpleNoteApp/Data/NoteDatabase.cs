using System.Collections.Generic;
using System.Threading.Tasks;
using gaweFirstSimpleNoteApp.Models;
using SQLite;

namespace gaweFirstSimpleNoteApp.Data
{
    public class NoteDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        public NoteDatabase(string databasePath)
        {
            _database = new SQLiteAsyncConnection(databasePath);
            // You should not do this. But there is no other way at the moment.
            // Please use async/await in other situations.
            _database.CreateTableAsync<Note>().Wait();
        }
        public Task<List<Note>> GetNotesAsync() => _database.Table<Note>().ToListAsync();
        public Task<Note> GetNoteAsync(int id) => _database.Table<Note>().FirstOrDefaultAsync(i => i.Id == id);
        public Task<int> SaveNoteAsync(Note note) => note.Id != 0 ? _database.UpdateAsync(note) : _database.InsertAsync(note);
        public Task<int> DeleteNoteAsync(Note note) => _database.DeleteAsync(note);
    }
}
