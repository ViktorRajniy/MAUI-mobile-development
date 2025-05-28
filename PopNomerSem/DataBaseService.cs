using SQLite;
using System.Diagnostics;
using System;

namespace PopNomerSem
{
    /// <summary>
    /// Service that work with database.
    /// </summary>
    public class DataBaseService
    {
        private SQLiteAsyncConnection _database;

        public DataBaseService()
        {
            InitializeDatabase();
        }

        private async Task InitializeDatabase()
        {
            try
            {
                if (_database != null) return;

                _database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "notes.db3"));
                await _database.CreateTableAsync<Note>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database error:{ex.Message}");
            }
        }

        const SQLite.SQLiteOpenFlags Flags = 
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public async Task<List<Note>> GetNotesAsync()
        {
            await InitializeDatabase();
            return await _database.Table<Note>().OrderByDescending(n => n.ModifiedDate).ToListAsync();
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            await InitializeDatabase();
            return await _database.Table<Note>().Where(n => n.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            await InitializeDatabase();

            if (note.Id != 0)
            {
                note.ModifiedDate = DateTime.Now;
                return await _database.UpdateAsync(note);
            }
            else
            {
                note.DateOfCreation = DateTime.Now;
                note.ModifiedDate = note.DateOfCreation;
                return await _database.InsertAsync(note);
            }
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            await InitializeDatabase();
            return await _database.DeleteAsync(note);
        }

    }
}
