using System.Collections.Generic;

namespace Pkv.Common
{
    public interface IDebugInfoHelper
    {
        void End(DebugInfo dbgInfo);
        void ConsolePrintFormattedDebugInfo();
        DebugInfo Start(string tag);
        void Reset();
    }
}