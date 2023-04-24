using System;
using System.Collections.Generic;

namespace ConnStuff
{
    public class GameObjectEventArgs : EventArgs
    {
        List<JKGameObject> GameObject { get; set; }

        public GameObjectEventArgs(List<JKGameObject> gameObject) : base()
        {
            GameObject = gameObject;
        }
    }
}