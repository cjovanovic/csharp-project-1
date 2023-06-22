namespace SeleniumLearning;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        TestContext.Progress.WriteLine("Firtsly executing setup method!");
    }

    [Test]
    public void Test1()
    {
        TestContext.Progress.WriteLine("Test 1 execution!");
    }

    [Test]
    public void Test2()
    {
        TestContext.Progress.WriteLine("Test 2 execution!");
    }

    [TearDown]
    public void CloseBrowser()
    {
        TestContext.Progress.WriteLine("Teardown method!");
    }
}
