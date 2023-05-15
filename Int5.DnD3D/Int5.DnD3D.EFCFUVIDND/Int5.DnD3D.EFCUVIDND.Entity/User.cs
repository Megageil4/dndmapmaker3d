using System.ComponentModel.DataAnnotations;

namespace Int5.DnD3D.EFCUVIDND.Entity
{
    public class User
    {
		public int Id { get; set; }
		[StringLength(20)]
		public string Username { get; set; }

		#region Konstruktoren
		public User(string username)
		{
			Username = username;
		}
		public User()  : this(String.Empty) { }
		#endregion
	}
}