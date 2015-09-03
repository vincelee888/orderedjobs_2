using System.Linq;
using NUnit.Framework;

namespace OrderedJobs
{
    [TestFixture]
    public class OrderedJobsTests
    {
        [Test]
        public void GivenNoTasks_ReturnNoTasks()
        {
            var target = new TaskSorter();
            var result = target.Sort();
            Assert.That(!result.Any());
        }

        [Test]
        public void GivenUnrelatedTasks_ReturnTasksInSameOrder()
        {
            var target = new TaskSorter("[a,b]");
            var result = target.Sort();
            Assert.That(result, Is.EqualTo(new[] { "a", "b" }));
        }

        [Test]
        public void GivenDependentTasks_ReturnTasksInRequiredOrder()
        {
            var target = new TaskSorter("[a,b]", "[a => b]");
            var result = target.Sort();
            Assert.That(result, Is.EqualTo(new[] { "b", "a" }));
        }

        [Test]
        public void GivenMultipleDependencies_DoPrioritisedTasksFirst()
        {
            var target = new TaskSorter("[a,b,c,d]", "[a => b,c => d]");
            var result = target.Sort();
            Assert.That(result, Is.EqualTo(new[] { "b", "a", "d", "c" }));
        }

        [Test]
        public void GivenChainOfDependencies_ReturnTasksInRequiredOrder()
        {
            var target = new TaskSorter("[a,b,c]", "[a => b,b => c]");
            var result = target.Sort();
            Assert.That(result, Is.EqualTo(new[] { "c", "b", "a" }));
        }

        [Test]
        public void GivenTaskWithManyDependencies_ReturnTasksInRequiredOrder()
        {
            var target = new TaskSorter("[a,b,c]", "[a => b,a => c]");
            var result = target.Sort();
            Assert.That(result, Is.EqualTo(new[] { "b", "c", "a" }));
        }

        [Test]
        public void GivenCyclicDependencies_ThrowException()
        {
            Assert.Throws<CyclicDependencyException>(() => new TaskSorter("[a,b,c]", "[a => b,b => c,c => a]"));
        }
    }
}
