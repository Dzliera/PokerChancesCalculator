namespace PokerChancesCalculator
{
    /// <summary>
    /// The royal flush.
    /// </summary>
    public class RoyalFlush : Combination
    {
        #region CONSTRUCTOR
        public RoyalFlush(Card[] cards) : base(CombinationType.RoyalFlush, cards) { }

        #endregion

        /// <summary>
        /// Checks if 7 element StraightFlush combination is RoyalFlush
        /// </summary>
        /// <param name="cards">
        /// Array of 5 cards
        /// </param>
        /// <returns>
        /// Cards in RoyalFlush combination if exits otherwise null
        /// </returns>
        public override Combination Check(Card[] cards)
        {
            Combination straightFlush = new StraightFlush(null).Check(cards, false);
            if (straightFlush == null)
            {
                Data.Combinations[CombinationType.StraightFlush] = null;
                return null;
            }
            Data.Combinations[CombinationType.StraightFlush] = straightFlush.Cards;

            if (straightFlush.Cards[0].Rank == CardRank.Ace && straightFlush.Cards[4].Rank == CardRank.King) return new Combination(Type, cards);

            return null;
        }
    }
}
