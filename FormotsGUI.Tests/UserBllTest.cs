using FormotsBLL.BLL;
using FormotsBLL.Mapper;
using FormotsCommon.DTO;
using NUnit.Framework;

namespace FormotsGUI.Tests
{
    [TestFixture]
    public sealed class UserBllTests
    {
        [SetUp]
        public void TestInitialize()
        {
            //Initialisation des mappings profiles
            var loadMappers = new MainMapperProfile();
            loadMappers.ConfigureObjectMapper();
        }

        [Test]
        public void AddEmptyUser_ReturnsFalse_Test()
        {
            var newUserDto = new UserDto();
            var userBll = new UsersBLL();
            var addUserResult = userBll.AddOrUpdateUser(newUserDto);
            Assert.IsFalse(addUserResult.Success);
        }

        [Test]
        public void AddNonEmptyUser_ReturnsFalse_Test()
        {
            var newUserDto = new UserDto
            {
                FirstName = "Martin",
                LastName = "Prost",
                Login = "mprost",
                Password = "0"
            };
            var userBll = new UsersBLL();
            var addUserResult = userBll.AddOrUpdateUser(newUserDto);
            Assert.IsTrue(addUserResult.Success);
        }
    }
}
