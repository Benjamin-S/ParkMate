using Microsoft.AspNetCore.Mvc;
using Xunit;
using ParkMate.Web.Controllers;

namespace ParkMate.Web.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexWillReturnViewResult()
        {
            var sut = new HomeController();

            var result = sut.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
