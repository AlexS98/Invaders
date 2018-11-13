using Invaders;
using Invaders.GameModels.Additional;
using Invaders.GameProcess;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void PlayerTest()
        {
            Player p = new Player(true, "", 5, new GameResources(10, 10, 10));
            Assert.Equal(10, p.PlayerResources.Gold);
            Assert.Equal(10, p.PlayerResources.Wheat);
            Assert.Equal(10, p.PlayerResources.Wood);
        }

        [Fact]
        public void ResourcesTest()
        {
            var res = new GameResources();
            Assert.Equal(0, res.Gold);
            Assert.Equal(0, res.Wheat);
            Assert.Equal(0, res.Wood);
        }
    }
}
