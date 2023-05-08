using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Int5.DnD3D.WebClient.DnDWebSocketClient
{
    public class GuidEventArgs : EventArgs
    {
        public Guid Id { get; set; }
        public GuidEventArgs(Guid id)
        {
            Id = id;
        }
    }
}
