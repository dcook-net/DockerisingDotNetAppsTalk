using System;

namespace FrontEnd
{
    public class MembersApiConfiguration
    {
        public Uri? Uri { get; set; }
        public int? Timeout { get; set; } = 2000;
    }
}