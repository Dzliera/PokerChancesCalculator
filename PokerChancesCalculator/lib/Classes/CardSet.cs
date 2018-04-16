using System;

namespace PokerChancesCalculator
{
    /// <summary>
    /// Set of players best 5 cards
    /// </summary>
    public class CardSet
    {
        /// <summary>
        /// Highest combination in CardSet
        /// </summary>
        public Combination HighestCombination;

        /// <summary>
        /// Kickers array that contains all cards in CardSet except of highest combination cards
        /// </summary>
        public Card[] KickerCards;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardSet"/> class.
        /// </summary>
        /// <param name="cards">
        /// 5 element array of poker cards
        /// </param>
        public CardSet(Card[] cards)
        {
            // todo: done for base version todo for update

            Card[] cardsCopy = (Card[]) cards.Clone();
            Array.Sort(cardsCopy);
            Combination bestComb = null;

            foreach (Combination combType in Data.CombTypes)
            {
                bestComb = combType.Check(cardsCopy);
                if (bestComb != null) break;
            }

            HighestCombination = bestComb;
        }
    }
}
