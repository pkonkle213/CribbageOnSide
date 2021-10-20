using cribbage2021.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CribbageTests
{
    [TestClass]
    public class PointsTests
    {
        [TestMethod]
        public void FifteenShouldAddTwoCardsRight()
        {
            const int valueOfFifteen = 2;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(5,"Hearts"),
            new Card(11,"Clubs")
            };

            //Act
            int success = point.Fifteen(cards);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void FifteenShouldAddThreeCardsRight()
        {
            const int valueOfFifteen = 2;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(3,"Clubs"),
            new Card(5,"Hearts"),
            new Card(7,"Diamonds")
            };

            //Act
            int success = point.Fifteen(cards);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void FifteenShouldAddFourCardsRight()
        {
            const int valueOfFifteen = 2;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(1,"Spades"),
            new Card(2,"Clubs"),
            new Card(5,"Hearts"),
            new Card(7,"Diamonds")
            };

            //Act
            int success = point.Fifteen(cards);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void FifteenShouldAddFiveCardsRight()
        {
            const int valueOfFifteen = 2;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(1,"Hearts"),
            new Card(2,"Hearts"),
            new Card(3,"Spades"),
            new Card(4,"Diamonds"),
            new Card(5,"Clubs")
            };

            //Act
            int success = point.Fifteen(cards);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void PairShouldAddPoints()
        {
            const int pointsForPair = 2;

            //Arrange
            Points point = new Points();
            Card card1 = new Card(10, "Clubs");
            Card card2 = new Card(10, "Spades");

            //Act
            int success = point.Pair(card1, card2);

            //Assert
            Assert.AreEqual(pointsForPair, success);
        }
    }
}
