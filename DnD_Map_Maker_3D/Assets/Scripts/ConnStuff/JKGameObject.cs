using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ConnStuff
{
    public class JKGameObject
    {
        private float[] _pos3;
        private float[] _rot3;
        private float[] _scale3;
        private string _modeltype;
        public Guid ClientId;
        private string _color;

        public string Color
        {
            get => _color;
            set { _color = value; }
        }

        public float[] pos3
        {
            get => _pos3;
            set { Update(); _pos3 = value; }
        }

        public float[] rot3
        {
            get => _rot3;
            set { Update(); _rot3 = value; }
        }

        public float[] scale3
        {
            get => _scale3;
            set { Update(); _scale3 = value; }
        }

        public string Modeltype
        {
            get => _modeltype;
            set { Update(); _modeltype = value; }
        }

        public Guid Guid { get; set; }
        public DateTime LastChanged { get; private set; }

        private void Update()
        {
            LastChanged = DateTime.Now;
        }
        
        public JKGameObject(float[] pos3, float[] rot3, float[] scale3, string modeltype, string color,Guid guid)
        {
            this.pos3 = pos3;
            this.rot3 = rot3;
            this.scale3 = scale3;
            this.Guid = guid;
            Modeltype = modeltype; 
            Color = color;
            LastChanged = DateTime.Now;
            ClientId = DataContainer.ClientId;
        }

        public JKGameObject(GameObject go) :this(go, Guid.NewGuid())
        {}

        public JKGameObject(GameObject go, Guid guid)
        {
            var position = go.transform.position;
            this.pos3 = new[] { position[0], position[1], position[2] };
            
            var rotation = go.transform.rotation;
            this.rot3 = new []{rotation[0], rotation[1], rotation[2] };
            
            var localScale = go.transform.localScale;
            this.scale3 = new []{localScale[0], localScale[1], localScale[2]};

            this.Modeltype = go.name.Replace("(Clone)", "");

            Color = go.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color.ToHexString();

            this.Guid = guid;

            LastChanged = DateTime.Now;
            ClientId = DataContainer.ClientId;
        }

        public JKGameObject()
        {
            
        }
    }
}
