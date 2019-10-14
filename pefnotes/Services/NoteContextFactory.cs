using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace pefnotes.Services
{
    public class NoteContextFactory : IDesignTimeDbContextFactory<NoteContext>
    {
        private string connectionString = "Host=localhost;Username=danilo;Password=danilo;Database=danilo";

        public NoteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NoteContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new NoteContext(connectionString);
        }
    }
}
