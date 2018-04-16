using System;
using System.Windows.Navigation;
using static PokerChancesCalculator.Data;

namespace PokerChancesCalculator
{
    /// <summary>
    /// The Flush combination.
    /// </summary>
    public class FLush : Combination
    {
        #region CONSTRUCTOR
        public FLush(Card[] cards) : base(CombinationType.Flush, cards) { }
        #endregion


        /// <summary>
        /// Checks if 7 element cards array contains Flush
        /// </summary>
        /// <param name="cards">
        /// 7 element array of cards
        /// </param>
        /// <returns>
        /// Flush combination if found
        /// </returns>
        public override Combination Check(Card[] cards)
        {
            return Check(cards, true);
        }

        public Combination Check(Card[] cards, bool alreadyChecked)
        {
            if (alreadyChecked)
            {
                if (Combinations[CombinationType.Flush] == null) return null;
                return new Combination(Type, Combinations[CombinationType.Flush]);
            }

            int[] frequency = new int[4]; // frequency of each suit

            int flushSuit = -1;

            for (int i = 0; i < cards.Length; i++)
            {
                frequency[(int)cards[i].Suit - 1] += 1;

                if (frequency[(int)cards[i].Suit - 1] == 5) flushSuit = (int)cards[i].Suit;
            }

            if (flushSuit != -1) return new Combination(Type, Array.FindAll(cards, card => card.Suit == (CardSuit)flushSuit));

            return null;
        }
    }

}
