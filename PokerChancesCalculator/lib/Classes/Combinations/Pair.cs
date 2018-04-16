using static PokerChancesCalculator.Data;

namespace PokerChancesCalculator
{
    /// <summary>
    /// Pair combination
    /// </summary>
    public class Pair : Combination
    {

        #region CONSTRUCTOR
        public Pair(Card[] cards) : base(CombinationType.Pair, cards) { }
        #endregion

        /// <summary>
        /// Checks if 7 elements cards array contains pair combination
        /// before invoking this method array is already checked for this combination
        /// and result is saved in data class
        /// </summary>
        /// <param name="cards">
        /// 7 elements cards array
        /// </param>
        /// <returns>
        /// Pair combination if exits
        /// </returns>
        public override Combination Check(Card[] cards)
        {
            if (Combinations[CombinationType.Pair] == null) return null;
            return new Combination(Type, Combinations[CombinationType.Pair]);
        }
    }
}
