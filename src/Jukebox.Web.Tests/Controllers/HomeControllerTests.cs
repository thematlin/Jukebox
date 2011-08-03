using FakeItEasy;
using Jukebox.Site.Controllers;
using NUnit.Framework;

namespace Jukebox.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [UnderTest] HomeController _controller;

        [SetUp]
        public void SetUp()
        {
            Fake.InitializeFixture(this);
        }
    }
}
