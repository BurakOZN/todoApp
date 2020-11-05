using Entity;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Job> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasIndex(i => i.Username).IsUnique();
            });
            mb.Entity<Job>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(x => x.User).WithMany(x => x.Jobs).HasForeignKey(x => x.UserId);
            });
            base.OnModelCreating(mb);
        }
    }
}
