using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CustomeModule.API.Controllers;
using CustomeModule.Interfaces.Services.Interface;

namespace CustomeModule.Test
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]

        public void TestGetUserById()
        {
            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetUserById(1));

            var cont = new UserController(mockService.Object,null,null,null);
            cont.Get(1);

            mockService.Verify(x => x.GetUserById(1));
        }

      
    }
}
