using System;
using UnityEngine;

namespace ConnStuff
{
    public class JK_GameObject
    {
        public float[] pos3 { get; set; }
        public float[] rot3 { get; set; }
        public float[] scale3 { get; set; }
        public Guid Guid { get; set; }

        public JK_GameObject(float[] pos3, float[] rot3, float[] scale3, Guid guid)
        {
            this.pos3 = pos3;
            this.rot3 = rot3;
            this.scale3 = scale3;
            this.Guid = guid;
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
        }
    }
}