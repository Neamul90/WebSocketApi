using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketApi.SocketHelper
{
    public class WebSocketHandler
    {
        protected internal async Task ReceiveMessageAsync(WebSocket ws)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            while (true)
            {
                await ws.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
            }
        }
        public async Task SendMessageAsync(WebSocket ws, string data)
        {
            while (true)
            {
                var buffer = Encoding.UTF8.GetBytes(data);
                if (ws.State != WebSocketState.Open) break;
                var sendTask = ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                await sendTask.ConfigureAwait(false);
                if (ws.State != WebSocketState.Open) break;
                await Task.Delay(1000).ConfigureAwait(false);
            }
        }
    }
}