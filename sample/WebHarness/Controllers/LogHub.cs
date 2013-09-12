using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebHarness.Controllers {
  public class LogHub : Hub {
    public void Hello() {
      Clients.All.SendLogEvent(new { hello = "world", x = 4 });
    }
  }
}