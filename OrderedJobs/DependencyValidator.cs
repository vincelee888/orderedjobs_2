using System.Collections.Generic;
using System.Linq;

namespace OrderedJobs
{ 
    internal class DependencyValidator : IListenToDependencyParsing
    {
        private static string[] GetDependencyBranch(Dependency dependency, Dependency[] parsedDependencies)
        {
            var branch = new List<string> { dependency.SecondTask };

            var nextDependency = parsedDependencies.SingleOrDefault(d => d.SecondTask == dependency.FirstTask);
            var dependencyCount = 0;
            while (nextDependency != null && dependencyCount < parsedDependencies.Count())
            {
                var currDependency = nextDependency;
                branch.Add(currDependency.SecondTask);
                nextDependency = parsedDependencies.SingleOrDefault(d => d.SecondTask == currDependency.FirstTask);
                dependencyCount++;
            }

            return branch.ToArray();
        }

        private static void Validate(Dependency[] parsedDependencies)
        {
            parsedDependencies.ToList().ForEach(d =>
            {
                var branch = GetDependencyBranch(d, parsedDependencies);
                branch.ToList()
                    .ForEach(currentTask =>
                    {
                        if (TaskOccursMoreThanOnce(branch, currentTask)) throw new CyclicDependencyException();
                    });
            });
        }

        private static bool TaskOccursMoreThanOnce(IEnumerable<string> branch, string currentTask)
        {
            return branch.Count(task => task == currentTask) > 1;
        }

        public void DependenciesParsed(Dependency[] dependencies)
        {
            Validate(dependencies);
        }
    }
}