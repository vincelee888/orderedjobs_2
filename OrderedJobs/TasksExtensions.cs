using System.Collections.Generic;

namespace OrderedJobs
{
    static internal class TasksExtensions
    {
        public static void MoveDependencyAhead(this List<string> tasks, Dependency dependency)
        {
            tasks.RemoveAt(tasks.FindIndex(x => x == dependency.FirstTask));
            tasks.Insert(tasks.FindIndex(x => x == dependency.SecondTask), dependency.FirstTask);
        }
    }
}