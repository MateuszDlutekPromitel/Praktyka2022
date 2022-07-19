using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace promitel1.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var number = common.TestClass.Test1(-1);
            Assert.IsTrue(number >= 0);
        }

    }
}
