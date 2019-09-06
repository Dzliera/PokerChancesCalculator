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
            //int combCount = 1;

            var crds = new List<Card>();
            crds.Add(cards[0]);
            
            for (int i = 0; i < cards.Length - 1; i++)
            {
                if (cards[i + 1].Rank == cards[i].Rank)continue;

                    if ((int)cards[i + 1].Rank - (int)cards[i].Rank != 1)
                    {
                        crds.Clear();
                    }

                crds.Add(cards[i + 1]);
                if (crds.Count == 5) return new Combination(Type, crds.ToArray());
            }

            if (crds.Count == 4 && crds[3].Rank == CardRank.King && cards[0].Rank == CardRank.Ace)
            {
                crds.Add(cards[0]);
                return new Combination(Type, crds.ToArray());
            }

            return null;
        }
    }

}
