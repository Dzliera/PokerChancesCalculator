using System.Collections.Generic;
using static PokerChancesCalculator.Data;

namespace PokerChancesCalculator
{
    /// <summary>
    /// Straight combination
    /// </summary>
    public class Straight : Combination
    {
        #region CONSTRUCTOR
        public Straight(Card[] cards) : base(CombinationType.Straight, cards) { }
        #endregion


        /// <summary>
        /// Checks if 7 element cards array contains Straight
        /// </summary>
        /// <param name="cards">
        /// 7 element array of cards
        /// </param>
        /// <returns>
        /// Cards in Straight combination
        /// </returns>
        public override Combination Check(Card[] cards)
        {
            int combCount = 1;

            for (int i = 0; i < cards.Length - 1; i++)
            {
                if ((int)cards[i + 1].Rank - (int)cards[i].Rank != 1 &&
                    (int)cards[i + 1].Rank - (int)cards[i].Rank != 13)
                {
                    combCount = 1;
                }

                combCount++;
                if (combCount == 5) return new Combination(Type, cards);
            }

            return null;
        }
    }

}
