using System;
using WebClientSystem;

namespace UnityWebClient
{
    public class TestAction : IWebAction
    {
        public string Action => "/fact";

        public Action<string> OnSuccess { get; set; }
        public Action<string> OnFail { get; set; }
    }
}
