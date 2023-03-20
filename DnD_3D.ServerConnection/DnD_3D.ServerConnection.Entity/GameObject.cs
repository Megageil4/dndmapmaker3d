namespace DnD_3D.ServerConnection.Entity;
public class GameObject
{
    // Pos3, Rot3,  Scale3, Modeltype
    public List<float[]> Pos3 { get; set; }
    public List<float[]> Rot3 { get; set; }
    public List<float[]> Scale3 { get; set; }
    public string Modeltype { get; set; }

    public GameObject(List<float[]> pos3, List<float[]> rot3, List<float[]> scale3, string modeltype)
    {
        Pos3 = pos3;
        Rot3 = rot3;
        Scale3 = scale3;
        Modeltype = modeltype;
    }
}
