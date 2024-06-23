using System;
using WebClientSystem;

namespace TonDurak.WebClient
{
    public class GetJwtAction : IWebAction
    {
        public string Action => "/gameauth";

        public Action<string> OnSuccess { get; set; }
        public Action<string> OnFail { get; set; }
    }
}
