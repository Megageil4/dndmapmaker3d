namespace CCKG.DnDMM3D.Rest
{
    public class GameObject
    {
        // Pos3, Rot3,  Scale3, Modeltype
        public Vector3 Pos3 { get; set; }
        public Vector3 Rot3 { get; set; }
        public Vector3 Scale3 { get; set; }
        public string Modeltype { get; set; }

        public GameObject(Vector3 pos3, Vector3 rot3, Vector3 scale3, string modeltype)
        {
            Pos3 = pos3;
            Rot3 = rot3;
            Scale3 = scale3;
            Modeltype = modeltype;
        }
    }
}
