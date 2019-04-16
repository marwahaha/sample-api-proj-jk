using System;
using Xunit;
using foolapi.Controllers;

namespace foolapi_tests
{
    public class FoolApiTests
    {

        [Fact]
        public void GetValues()
        {
            ValuesController controller = new foolapi.Controllers.ValuesController();
            var result = controller.GetValues();
            int expected = 2;
            int actual = result.Length;
            Assert.Equal(expected, actual);
        }
    }
}