namespace WebApplication1;

public class Userverwaltung
{
    public List<User> users { get; set; }

    public Userverwaltung()
    {
        users = new();
    }

    public static Userverwaltung def
    {
        get;
        set;
    } = new Userverwaltung();

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public List<User> AlleUser()
    {
        return users;
    }
}