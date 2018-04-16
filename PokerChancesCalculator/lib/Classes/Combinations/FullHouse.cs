using System;
using static PokerChancesCalculator.Data;

namespace PokerChancesCalculator
{
    /// <summary>
    /// FullHouse combination
    /// </summary>
    public class FullHouse : Combination
    {
        #region CONSTRUCTOR
        public FullHouse(Card[] cards) : base(CombinationType.FullHouse, cards) { }
        #endregion

        /// <summary>
        /// Checks if 7 element Card array contains FullHouse
        /// </summary>
        /// <param name="cards">
        /// 7 element Card array
        /// </param>
        /// <returns>
        /// Full House Combination if found
        /// </returns>
        public override Combination Check(Card[] cards)
        {
            Card[] pair = new Card[2];
            Card[] threeOfaKind = new Card[3];

            Combinations[CombinationType.Pair] = null;
            Combinations[CombinationType.ThreeOfAKind] = null;

            for (int i = 0; i < cards.Length - 1; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank)
                {
                    if (cards.Length > i + 2 && cards[i + 1].Rank == cards[i + 2].Rank)
                    {
                        threeOfaKind[0] = cards[i];
                        threeOfaKind[1] = cards[i + 1];
                        threeOfaKind[2] = cards[i + 2];
                    }
                    else if(threeOfaKind[1] != cards[i])
                    {
                        pair[0] = cards[i];
                        pair[1] = cards[1 + 1];
                    }
                }

                bool isPair = pair[0].Rank != CardRank.Default;
                bool isThreeOfaKind = threeOfaKind[0].Rank != CardRank.Default;

                if (isPair) Combinations[CombinationType.Pair] = pair;
                if (isThreeOfaKind) Combinations[CombinationType.ThreeOfAKind] = threeOfaKind;

                if (isPair && isThreeOfaKind)
                {
                    Card[] fullHouse = new Card[5];
                    Array.Copy(pair, fullHouse, 2); // copy 2 elements to 0;1 positions
                    Array.Copy(threeOfaKind, 0, fullHouse, 2, 3); // copy 3 elements to 2;3;4 positions
                    return new Combination(Type, fullHouse);
                }
            }

            return null;
        }

        protected override int Compare(Combination other)
        {
            if (Cards[2] > other.Cards[2]) return 1;
            if (Cards[2] < other.Cards[2]) return -1;

            /* if 3rd elements (threeOfaKind combinational cards) is equal 
            we must compare 1rs elements (pair combinational cards) */
            if (Cards[0] > other.Cards[0]) return 1;
            if (Cards[0] < other.Cards[0]) return -1;

            return 0;
        }
    }
}
