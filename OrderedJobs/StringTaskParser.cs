using System.Linq;

namespace OrderedJobs
{
    static internal class StringTaskParser
    {
        public static string[] ParseTasks(string tasks)
        {
            return StringParser.GetParts(tasks).ToArray();
        }
    }
}