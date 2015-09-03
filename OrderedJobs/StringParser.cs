using System.Collections.Generic;

namespace OrderedJobs
{
    static internal class StringParser
    {
        public static IEnumerable<string> GetParts(string input)
        {
            return input
                .Replace("[", "")
                .Replace("]", "")
                .Split(',');
        }
    }
}