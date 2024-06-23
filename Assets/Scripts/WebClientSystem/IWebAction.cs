using System;

namespace WebClientSystem
{
    public interface IWebAction
    {
        string Action { get; }

        Action<string> OnSuccess { get; set; }
        Action<string> OnFail { get; set; }
    }
}
