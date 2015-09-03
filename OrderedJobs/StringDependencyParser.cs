using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderedJobs
{
    public class StringDependencyParser
    {
        private readonly IEnumerable<IListenToDependencyParsing> _listeners;

        public StringDependencyParser(IEnumerable<IListenToDependencyParsing> listeners)
        {
            _listeners = listeners;
        }

        public Dependency[] ParseDependencies(string dependencies)
        {
            var parsedDependencies = StringParser.GetParts(dependencies)
                .Select(d =>
                {
                    var parts = d.Split(new[] {" => "}, StringSplitOptions.None);
                    return new Dependency(parts[1], parts[0]);
                })
                .ToArray();

            _listeners.ToList().ForEach(l => l.DependenciesParsed(parsedDependencies));

            return parsedDependencies;
        }
    }
}