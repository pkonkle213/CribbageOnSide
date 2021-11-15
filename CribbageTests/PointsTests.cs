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
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(5,"Hearts"),
            new Card(11,"Clubs")
            };

            //Act
            int success = point.Fifteens(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void FifteenShouldAddThreeCardsRight()
        {
            const int valueOfFifteen = 2;
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(3,"Diamonds"),
            new Card(5,"Hearts"),
            new Card(7,"Clubs")
            };

            //Act
            int success = point.Fifteens(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void FifteenShouldAddFourCardsRight()
        {
            const int valueOfFifteen = 2;
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(2,"Diamonds"),
            new Card(3,"Hearts"),
            new Card(3,"Clubs"),
            new Card(7,"Spades")
            };

            //Act
            int success = point.Fifteens(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void FifteenShouldAddFiveCardsRight()
        {
            const int valueOfFifteen = 2;
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(2,"Diamonds"),
            new Card(2,"Hearts"),
            new Card(3,"Hearts"),
            new Card(3,"Clubs"),
            new Card(5,"Spades")
            };

            //Act
            int success = point.Fifteens(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(valueOfFifteen, success);
        }

        [TestMethod]
        public void FifteenShouldFindMultipleSuccesses()
        {
            const int valueOfFifteen = 2;
            const int successes = 3;
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card> {
            new Card(3,"Diamonds"),
            new Card(5,"Hearts"),
            new Card(10,"Hearts"),
            new Card(11,"Clubs"),
            new Card(12,"Spades")
            };

            //Act
            int success = point.Fifteens(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(valueOfFifteen * successes, success);
        }

        [TestMethod]
        public void PairsShouldFindAPair()
        {
            const int valueOfPair = 2;
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Clubs"),
                new Card(5,"Spades")
            };

            //Act
            int success = point.Pairs(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(valueOfPair, success);
        }

        [TestMethod]
        public void PairsShouldFindMultiplePairs()
        {
            const int valueOfPair = 2;
            const int successes = 6;
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Clubs"),
                new Card(5,"Spades"),
                new Card(5,"Diamonds"),
                new Card(5,"Hearts")
            };

            //Act
            int success = point.Pairs(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(valueOfPair * successes, success);
        }

        [TestMethod]
        public void RunsShouldFindAThreeCardRun()
        {
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Clubs"),
                new Card(6,"Spades"),
                new Card(7,"Diamonds")
            };

            //Act
            bool success = point.TestRun(cards);

            //Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void RunsShouldFindAFourCardRun()
        {
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Clubs"),
                new Card(6,"Spades"),
                new Card(7,"Diamonds"),
                new Card(8,"Diamonds")
            };

            //Act
            bool success = point.TestRun(cards);

            //Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void RunsShouldFindAFiveCardRun()
        {
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Clubs"),
                new Card(6,"Spades"),
                new Card(7,"Diamonds"),
                new Card(8,"Diamonds"),
                new Card(9,"Hearts")
            };

            //Act
            bool success = point.TestRun(cards);

            //Assert
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void RunsShouldFindDoubleRun()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(6,"Spades"),
                new Card(8,"Diamonds"),
                new Card(5,"Clubs"),
                new Card(8,"Hearts"),
                new Card(7,"Diamonds")
            };

            //Act
            int success = point.Runs(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(8, success);
        }

        [TestMethod]
        public void RunsShouldFindQuadRun()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(6,"Spades"),
                new Card(8,"Diamonds"),
                new Card(6,"Clubs"),
                new Card(8,"Hearts"),
                new Card(7,"Diamonds")
            };

            //Act
            int success = point.Runs(cards, initialPoints, false);

            //Assert
            Assert.AreEqual(12, success);
        }

        [TestMethod]
        public void FlushShouldNotFindFlushIfDoesntExist()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Clubs"),
                new Card(6,"Spades"),
                new Card(7,"Diamonds"),
                new Card(8,"Diamonds")
            };
            Card starter = new Card(8, "Hearts");

            //Act
            int success = point.Flush(cards, starter, initialPoints, false, false);

            //Assert
            Assert.AreEqual(0, success);
        }

        [TestMethod]
        public void FlushShouldFindFourFlushIfExist()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Diamonds"),
                new Card(6,"Diamonds"),
                new Card(7,"Diamonds"),
                new Card(8,"Diamonds")
            };
            Card starter = new Card(8, "Hearts");

            //Act
            int success = point.Flush(cards, starter, initialPoints,false, false);

            //Assert
            Assert.AreEqual(4, success);
        }

        [TestMethod]
        public void FlushShouldFindFiveFlushIfExist()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Diamonds"),
                new Card(6,"Diamonds"),
                new Card(7,"Diamonds"),
                new Card(8,"Diamonds")
            };
            Card starter = new Card(9, "Diamonds");

            //Act
            int success = point.Flush(cards, starter, initialPoints, false, false);

            //Assert
            Assert.AreEqual(5, success);
        }

        [TestMethod]
        public void FlushShouldNotFindFourCardFlushIfCrib()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(5,"Diamonds"),
                new Card(6,"Diamonds"),
                new Card(7,"Diamonds"),
                new Card(8,"Diamonds")
            };
            Card starter = new Card(9, "Spades");

            //Act
            int success = point.Flush(cards, starter, initialPoints, true, false);

            //Assert
            Assert.AreEqual(0, success);
        }

        [TestMethod]
        public void KnobsShouldFindKnobsIfExists()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(11,"Diamonds"),
                new Card(6,"Spades"),
                new Card(7,"Hearts"),
                new Card(8,"Clubs")
            };
            Card starter = new Card(9, "Diamonds");

            //Act
            int success = point.Knobs(cards, starter, initialPoints, false);

            //Assert
            Assert.AreEqual(1, success);
        }

        [TestMethod]
        public void KnobsShouldNotFindKnobsIfDoesntExist()
        {
            int initialPoints = 0;
            //Arrange
            Points point = new Points();
            List<Card> cards = new List<Card>
            {
                new Card(11,"Spades"),
                new Card(6,"Spades"),
                new Card(7,"Hearts"),
                new Card(8,"Clubs")
            };
            Card starter = new Card(9, "Diamonds");

            //Act
            int success = point.Knobs(cards, starter, initialPoints, false);

            //Assert
            Assert.AreEqual(0, success);
        }
    }
}
