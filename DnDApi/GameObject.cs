namespace DnDApi;

public class GameObject
{
    // Pos3, Rot3,  Scale3, Modeltype
    public float[] Pos3 { get; set; }
    public float[] Rot3 { get; set; }
    public float[] Scale3 { get; set; }
    public string Modeltype { get; set; }
    public string Color { get; set; }
    public Guid Guid { get; set; }
    public DateTime LastChanged { get; set; }

    public GameObject(float[] pos3, float[] rot3, float[] scale3, string modeltype,string color ,Guid guid, DateTime lastChanged)
    {
        Pos3 = pos3;
        Rot3 = rot3;
        Scale3 = scale3;
        Modeltype = modeltype;
        Color = color;
        this.Guid = guid;
        this.LastChanged = DateTime.Now;
    }

    public override string ToString()
    {
        return $"Pos: [{Pos3[0]}, {Pos3[1]}, {Pos3[2]}], Rot: [{Rot3[0]}, {Rot3[1]}, {Rot3[2]}], Scale : [{Scale3[0]}, {Scale3[1]}, {Scale3[2]}]";
    }
}
