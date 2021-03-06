﻿namespace XMLFeedsViewer.Web.Hubs
{
    using System.Collections.Generic;

    using Microsoft.AspNet.SignalR;
    using Microsoft.AspNet.SignalR.Hubs;

    using XMLFeedsViewer.Web.ViewModels;

    [HubName("Feeds")]
    public class FeedsHub : Hub
    {
        public ICollection<SportViewModel> GetAllSports()
        {
            //TODO: go to db and take all sports
            return new List<SportViewModel>
            {
                new SportViewModel
                {
                    Events = null,
                    Name = "test"
                }
            };
        }
    }
}