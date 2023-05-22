using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTestClient.DnDWebSocketClient
{
    public class DnDClient : WebSocketClient
    {
        public DnDClient() { }
        public event EventHandler<EventArgs> NewMap;
        public event EventHandler<EventArgs> NewGameObject;
        protected virtual void OnNewMap() { 
            NewMap?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnNewGameObject()
        {
            NewGameObject?.Invoke(this, EventArgs.Empty);
        }

    }
}
