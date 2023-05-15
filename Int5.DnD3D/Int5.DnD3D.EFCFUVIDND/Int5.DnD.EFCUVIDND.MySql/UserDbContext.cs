using Int5.DnD3D.EFCUVIDND.Entity;
using Microsoft.EntityFrameworkCore;

namespace Int5.DnD.EFCUVIDND.MySql
{
    public class UserDbContext : DbContext
    {
		#region Properties
		public DbSet<User> TeamSet => Set<User>();
		#endregion


		#region Konstruktoren
		public UserDbContext() : base() { }
		public UserDbContext(DbContextOptions options) : base(options) { }
		#endregion

		#region Methoden
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("DnD");
			modelBuilder.Entity<User>().ToTable("User");
			
			
		}


		#endregion
	}
}