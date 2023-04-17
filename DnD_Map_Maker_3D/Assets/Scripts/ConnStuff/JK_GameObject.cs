using System;
using Unity.VisualScripting;
using UnityEngine;
using Update = UnityEngine.PlayerLoop.Update;

namespace ConnStuff
{
    public class JK_GameObject
    {
        private float[] _pos3;
        private float[] _rot3;
        private float[] _scale3;

        public float[] pos3
        {
            get => _pos3;
            set
            {
                Update();
                _pos3 = value;
            }
        }

        public float[] rot3
        {
            get => _rot3;
            set
            {
                Update();
                _rot3 = value;
            }
        }

        public float[] scale3
        {
            get => _scale3;
            set
            {
                Update();
                _scale3 = value;
            }
        }

        public Guid Guid { get; private set; }
        public DateTime LastChanged { get; private set; }

        private void Update()
        {
            LastChanged = DateTime.Now;
        }
        
        public JK_GameObject(float[] pos3, float[] rot3, float[] scale3, Guid guid)
        {
            this.pos3 = pos3;
            this.rot3 = rot3;
            this.scale3 = scale3;
            this.Guid = guid;
            LastChanged = DateTime.Now;
        }

        public JK_GameObject(GameObject go)
        {
            var position = go.transform.position;
            this.pos3 = new[] { position[0], position[1], position[2] };
            
            var rotation = go.transform.rotation;
            this.rot3 = new []{rotation[0], rotation[1], rotation[2] };
            
            var localScale = go.transform.localScale;
            this.scale3 = new []{localScale[0], localScale[1], localScale[2]};
            
            this.Guid = Guid.NewGuid();

            LastChanged = DateTime.Now;
        }
    }
}