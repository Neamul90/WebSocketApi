using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebSocketApi.SocketHelper;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.WebSockets;
namespace WebSocketApi.Controllers
{
   
    public class WsTimeController : ApiController
    {
        private WebSocketHandler _webSocketHandler;

        public WsTimeController()
        {
            _webSocketHandler = new WebSocketHandler();
        }
        public HttpResponseMessage GetMessage()
        {
            var status = HttpStatusCode.BadRequest;
            var context = HttpContext.Current;
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessRequest);
                status = HttpStatusCode.SwitchingProtocols;
            }
            return new HttpResponseMessage(status);
        }
        private async Task ProcessRequest(AspNetWebSocketContext context)
        {
            var ws = context.WebSocket;
            if (context.QueryString.AllKeys.Contains("id"))
            {
                int vehicleRepaireId = Convert.ToInt32(context.QueryString["id"]);
                //string data = JsonConvert.SerializeObject(_bidService.GetBidsByVehicleRepaireId(vehicleRepaireId));

                string data = JsonConvert.SerializeObject("{'name':'John', 'age':30, 'car':null}");
                await Task.WhenAll(_webSocketHandler.SendMessageAsync(ws, data), _webSocketHandler.ReceiveMessageAsync(ws));
            }
        }

    }
}