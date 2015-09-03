namespace OrderedJobs
{
    public class Dependency
    {
        public string FirstTask { get; private set; }
        public string SecondTask { get; private set; }

        public Dependency(string firstTask, string secondTask)
        {
            FirstTask = firstTask;
            SecondTask = secondTask;
        }
    }
}