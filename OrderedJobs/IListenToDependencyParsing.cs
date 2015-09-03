namespace OrderedJobs
{
    public interface IListenToDependencyParsing
    {
        void DependenciesParsed(Dependency[] dependencies);
    }
}