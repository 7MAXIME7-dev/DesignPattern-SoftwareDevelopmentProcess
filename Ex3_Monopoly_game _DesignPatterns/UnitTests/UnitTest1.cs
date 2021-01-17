using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDesignPattern;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void shouldWeReleasePlayer_PlayerIsInJail_ReturnsFalse()
        {
            //Arrange
            var game = new Game();

            Player a = Observer.getPlayerObject("a");
            Player b = Observer.getPlayerObject("b");
            Player c = Observer.getPlayerObject("c");
            Player d = Observer.getPlayerObject("d");
            Player e = Observer.getPlayerObject("e");

            Player[] azertyuiop = new Player[] { a, b, c, d, e };

            game.setPlayersOnBoard(azertyuiop);

            //Act
            var result = game.shouldWeReleasePlayer(0);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void shouldWeReleasePlayer_PlayerIsInJail_ReturnsTrue()
        {
            //Arrange
            var game = new Game();

            Player a = Observer.getPlayerObject("a");
            Player b = Observer.getPlayerObject("b");
            Player c = Observer.getPlayerObject("c");
            Player d = Observer.getPlayerObject("d");
            Player e = Observer.getPlayerObject("e");

            a.jailCount = 2;

            Player[] azertyuiop = new Player[] { a, b, c, d, e };

            game.setPlayersOnBoard(azertyuiop);

            //Act
            var result = game.shouldWeReleasePlayer(0);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
