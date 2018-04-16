using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static PokerChancesCalculator.Data;

namespace PokerChancesCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region GUI

        /// <summary>
        /// array of rank choose combo boxes
        /// </summary>
        private ComboBox[] _rankComboBoxes;

        /// <summary>
        /// array of suit choose combo boxes
        /// </summary>
        private ComboBox[] _suitComboBoxes;

        /// <summary>
        /// Card image objects
        /// </summary>
        private Image[] _images;
        #endregion

        /// <summary>
        /// Array of cards. which array elements has default value that card is not known.
        /// </summary>
        private readonly Card[] _userCards = new Card[7];

        private bool _allKnown = false;

        public MainWindow()
        {
            InitializeComponent();
            guiInit();

            Array.ForEach(_rankComboBoxes, box => box.SelectionChanged += ComboBox_SelectionChanged);
            Array.ForEach(_suitComboBoxes, box => box.SelectionChanged += ComboBox_SelectionChanged);
            for (int c = 0; c < AllCardCount; c++) CardDeck[c] = (c + 1).ToCard();
        }

        /// <summary>
        /// This method is used for initializing  GUI elements
        /// </summary>
        private void guiInit()
        {
            _images = new[]
                              {
                                Flop1,
                                Flop2,
                                FLop3,
                                Turn,
                                River,
                                MyCard1,
                                MyCard2
                              };

            _rankComboBoxes = new[]
                                      {
                                          C_ChooseRank_Flop1,
                                          C_ChooseRank_Flop2,
                                          C_ChooseRank_Flop3,
                                          C_ChooseRank_Turn,
                                          C_ChooseRank_River,
                                          C_ChooseRank_MyCard1,
                                          C_ChooseRank_MyCard2
                                      };

            _suitComboBoxes = new[]
                                      {
                                          C_ChooseSuit_Flop1,
                                          C_ChooseSuit_Flop2,
                                          C_ChooseSuit_Flop3,
                                          C_ChooseSuit_Turn,
                                          C_ChooseSuit_River,
                                          C_ChooseSuit_MyCard1,
                                          C_ChooseSuit_MyCard2
                                      };
        }

        /// <summary>
        /// Button "Calculate Chances" click event
        /// </summary>
        private void CalculateChances_Click(object sender, RoutedEventArgs e)
        {

            ResetResults();

            Card[] restCards = new Card[AllCardCount];
            Card[] userCardsSorted = (Card[])_userCards.Clone();

            Array.Sort(userCardsSorted);
            CardDeck.CopyTo(restCards, 0);

            for (int c = 0; c < AllCardCount; c++) if (_userCards.Contains((c + 1).ToCard())) restCards[c] = default(Card);

            int unknownCardsCount = userCardsSorted.Count(card => card == default(Card));
            _allKnown = unknownCardsCount == 0;

            CheckPossibleSets(userCardsSorted, restCards,unknownCardsCount);

            #region Print Results

            if (_allKnown)
            {
                foreach (var comb in FrequencyTable)
                {
                    L_Chances.Content = CombinationNames[(int)comb.Key];
                    return;
                }
            }

            float overallChance = 0;
            foreach (Combination comb in CombTypes)
            {
                if (!FrequencyTable.ContainsKey(comb.Type)) continue;

                int freq = FrequencyTable[comb.Type];
                float chance = freq / (float) CaseCount * 100;
                overallChance += chance;
                L_Chances.Content += (CombinationNames[(int)comb.Type] + " = " + chance + " || " + overallChance + "%" + "\r\n");
            }

            #endregion
        }

        /// <summary>
        /// Reset previous calculation results
        /// </summary>
        private void ResetResults()
        {
            L_Chances.Content = "";
            FrequencyTable.Clear();
            CaseCount = 0;
            Combinations.Clear();
            this.UpdateLayout();
        }

        /// <summary>
        /// checks all possible sets of cards from array
        /// in which some cards is already known and others aren't.
        /// Card is unknown if it is equal to "default(Card)".
        /// This method is recursive
        /// </summary>
        private void CheckPossibleSets(Card[] userCards, Card[] restCards, int unknownCardsCount)
        {

            #region BaseCase
            if (unknownCardsCount == 0)
            {
                CaseCount++;
                CardSet set = new CardSet(userCards);

                if (!FrequencyTable.ContainsKey(set.HighestCombination.Type))
                {
                    FrequencyTable.Add(set.HighestCombination.Type, 1);
                }
                else
                {
                    FrequencyTable[set.HighestCombination.Type]++;
                }

                return;
            } 
            #endregion

            int index = 0;
            while (userCards[index] != default(Card)) index++; // location first unknown cards poristion
            unknownCardsCount--; // temponary decrease unknown cards count

            for (int c = 0; c < restCards.Length; c++)
            {
                if (restCards[c] == default(Card)) continue;

                Card temp = restCards[c];
                
                userCards[index] = restCards[c];
                restCards[c] = default(Card);

                CheckPossibleSets(userCards, restCards,unknownCardsCount); // recursive step

                restCards[c] = temp;  //backtracking
            }

            
            userCards[index] = default(Card); //backtracking 
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox source = (ComboBox)sender;
            int rIndex = Array.FindIndex(_rankComboBoxes, box => box.Equals(source));
            int sIndex = Array.FindIndex(_suitComboBoxes, box => box.Equals(source));
            int index = rIndex == -1 ? sIndex : rIndex;

            if (_rankComboBoxes[index].SelectedIndex > 0 && _suitComboBoxes[index].SelectedIndex > 0)
            {
                int cardIndex = RankCount * (_suitComboBoxes[index].SelectedIndex - 1) + _rankComboBoxes[index].SelectedIndex;
                _images[index].Source = new BitmapImage(new Uri(@"images/cards/" + cardIndex + ".png", UriKind.Relative));

                CardRank rank = (CardRank)_rankComboBoxes[index].SelectedIndex;
                CardSuit suit = (CardSuit)_suitComboBoxes[index].SelectedIndex;
                _userCards[index] = new Card(rank, suit);
            }
            else
            {
                _images[index].Source = new BitmapImage(new Uri(@"images/cards/backside.jpg", UriKind.Relative));

                _userCards[index] = default(Card);
            }
        }

        private void b_Random_Click(object sender, RoutedEventArgs e)
        {
            ResetResults();

            List<Card> deckCopy = CardDeck.ToList();
            Random r =  new Random();

            for (int i = 0; i < CardCount; i++)
            {
                int randomNumber = r.Next(0, deckCopy.Count - 1);
                Card randomCard = deckCopy[randomNumber];

                _rankComboBoxes[i].SelectedIndex = (int)randomCard.Rank;
                _suitComboBoxes[i].SelectedIndex = (int)randomCard.Suit;

                ComboBox_SelectionChanged(_rankComboBoxes[i], null);
                ComboBox_SelectionChanged(_suitComboBoxes[i], null);

                deckCopy.RemoveAt(randomNumber);
            }
        }
    }
}
