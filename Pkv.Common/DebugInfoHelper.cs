using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Pkv.Common
{
    public struct DebugInfo
    {
        public string MethodName;
        public long Start;
        public long End;
    }


    [ExcludeFromCodeCoverage]
    public class DebugInfoHelper : IDebugInfoHelper
    {
        List<DebugInfo> debugData;
        public DebugInfoHelper()
        {
            Console.Out.WriteLine("Init DebugInfoHelper Singleton");
            debugData = new List<DebugInfo>();
        }
        public DebugInfo Start(string tag)
        {
            return new DebugInfo { MethodName = tag, Start = DateTime.UtcNow.Ticks };
        }

        public void End(DebugInfo dbgInfo)
        {
            dbgInfo.End = DateTime.UtcNow.Ticks;
            debugData.Add(dbgInfo);
        }

        public void ConsolePrintFormattedDebugInfo()
        {
            try
            {
                Console.Out.WriteLine("Printing Debug Info...");

                var parentSpan = debugData.OrderBy(x => x.Start).Take(1).FirstOrDefault();
                var maxDuration = parentSpan.End - parentSpan.Start;
                const int maxPercentage = 30;

                var maxLengthOfTag = debugData?.Select(x => new { len = x.MethodName.Length }).OrderByDescending(x => x.len)
                    ?.Take(1)?.FirstOrDefault()?.len;

                var updInfo = debugData.OrderBy(x => x.Start).Select(x => new
                {
                    ops = x.MethodName,
                    startAt = (x.Start - parentSpan.Start) * maxPercentage / maxDuration,
                    duration = (x.End - x.Start) * maxPercentage / maxDuration,
                    milliSec = (x.End - x.Start) / 10000
                });

                var sb = new StringBuilder();
                foreach (var info in updInfo)
                {
                    if (maxLengthOfTag != null)
                        sb
                            .Append(GetTagToPrint(info.ops, maxLengthOfTag.Value))
                            .Append(GetStartAtToPrint(info.startAt))
                            .Append(GetDurationToPrint(info.duration))
                            .Append($"  {info.duration * 100 / maxPercentage}%")
                            .Append($"  {info.milliSec} ms")
                            .AppendLine();
                }

                Console.Out.WriteLine(sb.ToString());
            }
            catch (Exception)
            {
                Console.Out.WriteLine("Couldn't print debugInfo");
            }
        }

        private static string GetDurationToPrint(in long duration)
        {
            var arr = new char[duration];
            Populate(arr, '|');
            return new string(arr);
        }

        private static string GetStartAtToPrint(in long startAt)
        {
            var arr = new char[startAt];
            Populate(arr, '.');
            return new string(arr);
        }

        private static string GetTagToPrint(string ops, in int maxLengthOfTag)
        {
            var arr = new char[maxLengthOfTag - ops.Length];
            Populate(arr, '-');

            return ops + new string(arr) + ">";
        }

        private static void Populate<T>(IList<T> arr, T value)
        {
            for (var i = 0; i < arr.Count; i++)
            {
                arr[i] = value;
            }
        }

        public void Reset()
        {
            debugData.Clear();
            debugData = new List<DebugInfo>();
        }
    }
}