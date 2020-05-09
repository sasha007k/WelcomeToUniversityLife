using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Domain.Entities;
namespace WelcomeToUniversityLifeAspServer.Controllers
{
 public   interface IFucker 
    {
        Task Send(Сampaign c);

    }
    public class NewsHub  : Hub,IFucker
    {
         public async Task Send(Сampaign c)
        {
            await this.Clients.All.SendAsync("Send", c);
        }
    }
}
