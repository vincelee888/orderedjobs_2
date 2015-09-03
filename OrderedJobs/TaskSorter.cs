using System.Linq;

namespace OrderedJobs
{
    public class TaskSorter
    {
        private string[] Tasks { get; set; }
        private Dependency[] Dependencies { get; set; }

        public TaskSorter()
        {
            Tasks = new string[0];
            Dependencies = new Dependency[0];
        }

        public TaskSorter(string tasks) : this()
        {
            Tasks = StringTaskParser.ParseTasks(tasks);
        }

        public TaskSorter(string tasks, string dependencies) : this(tasks)
        {
            var listeners = new [] { new DependencyValidator() };
            Dependencies = new StringDependencyParser(listeners).ParseDependencies(dependencies);
        }


        public string[] Sort()
        {
            var orderedTasks = Tasks.ToList();

            Tasks.ToList()
                .ForEach(t =>
                {
                    Dependencies
                        .Where(d => d.SecondTask == t)
                        .ToList()
                        .ForEach(d => orderedTasks.MoveDependencyAhead(d));
        });

            return orderedTasks.ToArray();
        }
    }
}