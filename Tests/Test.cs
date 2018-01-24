using Invaders;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void PlayerTest()
        {
            Player p = new Player(true, 5, 10, 10, 10);
            Assert.Equal(10, p.Gold);
            Assert.Equal(10, p.Wheat);
            Assert.Equal(10, p.Wood);
        }
    }
}
