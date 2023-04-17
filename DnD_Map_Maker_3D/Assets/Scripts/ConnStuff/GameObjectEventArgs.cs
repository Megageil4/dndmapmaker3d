using System;
using System.Collections.Generic;

namespace ConnStuff
{
    public class GameObjectEventArgs : EventArgs
    {
        List<JK_GameObject> GameObject { get; set; }

        public GameObjectEventArgs(List<JK_GameObject> gameObject) : base()
        {
            GameObject = gameObject;
        }
    }
}