using System;

namespace CMS.WebApplication
{

    public sealed class MenuItem
    {

        public string Title { get; set; }

        public Func<string> Url { get; set; }

        public string RouteNamePrefix { get; set; }

    }

}