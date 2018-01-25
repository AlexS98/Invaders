using Invaders;
using Invaders.GameModels.Additional;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void PlayerTest()
        {
            Player p = new Player(true, 5, new Price(10, 10, 10));
            Assert.Equal(10, p.Resources.Gold);
            Assert.Equal(10, p.Resources.Wheat);
            Assert.Equal(10, p.Resources.Wood);
        }
    }
}
