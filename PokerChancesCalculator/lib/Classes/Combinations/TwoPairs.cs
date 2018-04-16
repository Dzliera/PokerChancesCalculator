﻿using System;

namespace PokerChancesCalculator.lib.Classes.Combinations
{


    /// <summary>
    /// The TwoPairs poker combination
    /// </summary>
    public class TwoPairs : Combination
    {
        #region CONSTRUCTOR
        public TwoPairs(Card[] cards) : base(CombinationType.TwoPairs, cards) { } 
        #endregion

        /// <summary>
        /// Checks if array contains TwoPairs combination
        /// </summary>
        /// <param name="cards">
        /// 7 elements cards array
        /// </param>
        /// <returns>
        /// Combinational cards if contains and Null if not contains
        /// </returns>
        public override Combination Check(Card[] cards)
        {
            Card[] firstPair = new Card[2];
            Card[] secondPair = new Card[2];

            for (int i = cards.Length - 1; i >= 1; i--)
            {
                // already assumed that combination doesn't contains Three Of A Kind combination
                if (cards[i].Rank == cards[i - 1].Rank) 
                {
                    if (firstPair[0].Rank == CardRank.Default)
                    {
                        firstPair[0] = cards[i];
                        firstPair[1] = cards[i - 1];
                    }
                    else
                    {
                        secondPair[0] = cards[i];
                        secondPair[1] = cards[i - 1];
                        Array.Resize(ref firstPair, 4); // turn firstPair array into resulting array
                        Array.Copy(secondPair, 0, firstPair, 2, 2); // copy elements of secondpair aray into resulting array
                        return new Combination(Type, firstPair);
                    }
                }
            }

            return null;
        }
    }
}
