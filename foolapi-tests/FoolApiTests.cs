using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using foolapi.Models;
using foolapi.Controllers;


namespace foolapi_tests
{
    public class FoolApiTests
    {
        // No data check
        [Fact]
        public void CallGetProdValuesAsyncReturnsSize()
        {
            List<string> valuesList = new List<string>();
            ProductController controller = new ProductController();
            var actionResult = controller.GetNonData();

            var result = actionResult.Result as ObjectResult;
            valuesList.AddRange((IEnumerable<string>)result.Value);
            int expected = 2;
            int actual = valuesList.Count;
            Assert.Equal(expected, actual);
        }

        // TODO: mock repository to make these good unit tests
        // These rely on real data so they are not effective unit tests :-(
        [Fact]
        public async void CallGetAllProductsIsNotNull()
        {
            ProductController controller = new ProductController();
            var actionResult = await controller.GetAllProducts();
            Assert.NotNull(actionResult);

            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Product>;
            Assert.NotNull(model);
        }

        [Fact]
        public async void CallGetAllProductsIsNotEmpty()
        {
            ProductController controller = new ProductController();
            var actionResult = await controller.GetAllProducts();

            //Assert
            var okObjectResult = actionResult.Result as OkObjectResult;
            var model = okObjectResult.Value as List<Product>;
            Assert.True(model.Count > 0);
        }
    }

}