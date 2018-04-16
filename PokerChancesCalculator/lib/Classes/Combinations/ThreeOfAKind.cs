namespace PokerChancesCalculator.lib.Classes.Combinations
{
    /// <summary>
    /// Three Of A Kind combination
    /// </summary>
    public class ThreeOfAKind : Combination
    {
        #region CONSTRUCTOR
        public ThreeOfAKind(Card[] cards) : base(CombinationType.ThreeOfAKind, cards) { }
        #endregion

        /// <summary>
        /// Checks if given array of cards contains ThreeOfaKind combination
        /// before invoking this method array is already checked for this combination
        /// and result is saved in data class
        /// </summary>
        /// <param name="cards">
        /// 7 elements cards array
        /// </param>
        /// <returns>
        /// Combinational cards if contains, null if not contains
        /// </returns>
        public override Combination Check(Card[] cards)
        {
            if (Data.Combinations[CombinationType.ThreeOfAKind] == null) return null;
            return new Combination(Type, Data.Combinations[CombinationType.ThreeOfAKind]);
        }
    }
}
