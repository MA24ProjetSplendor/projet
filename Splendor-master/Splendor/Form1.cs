/**
 * \file      frmAddVideoGames.cs
 * \author    F. Andolfatto
 * \version   1.0
 * \date      August 22. 2018
 * \brief     Form to play.
 *
 * \details   This form enables to choose coins or cards to get ressources (precious stones) and prestige points 
 * to add and to play with other players
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Splendor
{
    /// <summary>
    /// manages the form that enables to play with the Splendor
    /// </summary>
    public partial class frmSplendor : Form
    {
        //used to store the number of coins selected for the current round of game
        private int nbRubis;
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        private int nbSaphir;

        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn;

        /// <summary>
        /// constructor
        /// </summary>
        public frmSplendor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads the form and initialize data in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_Load(object sender, EventArgs e)
        {

            lblGoldCoin.Text = "5";

            lblDiamandCoin.Text = "7";
            lblEmeraudeCoin.Text = "7" ;
            lblOnyxCoin.Text = "7";
            lblRubisCoin.Text = "7";
            lblSaphirCoin.Text = "7";

            conn = new ConnectionDB();


            // Get Player Coin

            int[] coins = conn.GetPlayerCoin(1);

            //load cards from the database
            //they are not hard coded any more
            //TO DO

            //load cards from the database

            //load cards level 1
           
            Stack<Card> listCardOne = conn.GetListCardAccordingToLevel(1);
            int i = 0;
            int nbstack = listCardOne.Count();

            foreach (Control ctrl in flwCardLevel1.Controls)
            {
                if(i < nbstack)
                {
                    ctrl.Text = listCardOne.Pop().ToString();
                    i++;
                }
                
            }

            //load cards level 2
            Stack<Card> listCardTwo = conn.GetListCardAccordingToLevel(2);
            i = 0;
            nbstack = listCardTwo.Count();

            foreach (Control ctrl in flwCardLevel2.Controls)
            {
                if (i < nbstack)
                {
                    ctrl.Text = listCardTwo.Pop().ToString();
                    i++;
                }
            }

            //load cards level 3
            Stack<Card> listCardThree = conn.GetListCardAccordingToLevel(3);
            i = 0;
            nbstack = listCardThree.Count();

            foreach (Control ctrl in flwCardLevel3.Controls)
            {
                if (i < nbstack)
                {
                    ctrl.Text = listCardThree.Pop().ToString();
                    i++;
                }
            }

            //load cards level 4
            /*Stack<Card> listCardFour = conn.GetListCardAccordingToLevel(4);

            foreach (Control ctrl in flwCardNoble.Controls)
            {
                ctrl.Text = listCardFour.Pop().ToString();
            }*/


            //Go through the results
            //Don't forget to check when you are at the end of the stack

            //fin TO DO

            this.Width = 680;
            this.Height = 540;

            enableClicLabel = false;

            lblChoiceDiamand.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceEmeraude.Visible = false;
            cmdValidateChoice.Visible = false;
            cmdNextPlayer.Visible = false;

            //we wire the click on all cards to the same event
            //TO DO for all cards

            // Cards level 1
            txtLevel11.Click += ClickOnCard;
            txtLevel12.Click += ClickOnCard;
            txtLevel13.Click += ClickOnCard;
            txtLevel14.Click += ClickOnCard;

            // Cards level 2
            txtLevel21.Click += ClickOnCard;
            txtLevel22.Click += ClickOnCard;
            txtLevel23.Click += ClickOnCard;
            txtLevel24.Click += ClickOnCard;

            // Cards level 3
            txtLevel31.Click += ClickOnCard;
            txtLevel32.Click += ClickOnCard;
            txtLevel33.Click += ClickOnCard;
            txtLevel34.Click += ClickOnCard;

            // Cards Noble
            txtNoble1.Click += ClickOnCard;
            txtNoble2.Click += ClickOnCard;
            txtNoble3.Click += ClickOnCard;
            txtNoble4.Click += ClickOnCard;

        }

        private void ClickOnCard(object sender, EventArgs e)
        {
            List<List<string>> RubisCard = new List<List<string>>();
            List<List<string>> EmeraudeCard = new List<List<string>>();
            List<List<string>> OnyxCard = new List<List<string>>();
            List<List<string>> SaphirCard = new List<List<string>>();
            List<List<string>> DiamandCard = new List<List<string>>();

            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
            {
                // Récupère toute les informations
                TextBox txtBox = (TextBox) sender;

                string[] informations = txtBox.Lines;

                string[] ressources = informations[0].Split('\t');
                ressources = ressources.Where(item => item != string.Empty).ToArray();

                if(ressources[1] != "")
                {
                    int ptPrestige = Convert.ToInt32(ressources[1]);
                }

                int nbLine = informations.Length;

                int line = 2;

                List<List<string>> CostCard = new List<List<string>>();
                List<string> cost = new List<string>();

                while(line < nbLine)
                {
                    cost.Clear();

                    if (informations[line].Contains("Rubis"))
                    {
                        string resultString = Regex.Match(informations[line], @"\d+").Value;

                        Int32.Parse(resultString);

                        cost.Add(resultString);

                        CostCard.Add(cost);
                    }

                    if (informations[line].Contains("Emeraude"))
                    {
                        string resultString = Regex.Match(informations[line], @"\d+").Value;

                        Int32.Parse(resultString);

                        cost.Add(resultString);

                        CostCard.Add(cost);
                    }

                    if (informations[line].Contains("Onyx"))
                    {
                        string resultString = Regex.Match(informations[line], @"\d+").Value;

                        Int32.Parse(resultString);

                        cost.Add(resultString);

                        CostCard.Add(cost);
                    }

                    if (informations[line].Contains("Saphir"))
                    {
                        string resultString = Regex.Match(informations[line], @"\d+").Value;

                        Int32.Parse(resultString);

                        cost.Add(resultString);

                        CostCard.Add(cost);
                    }

                    if (informations[line].Contains("Diamand"))
                    {
                        string resultString = Regex.Match(informations[line], @"\d+").Value;

                        Int32.Parse(resultString);

                        cost.Add(resultString);

                        CostCard.Add(cost);
                    }
                    
                    line++;
                }

                DialogResult dialogResult = MessageBox.Show(txtBox.Text, "Voulez-vous acheter la carte?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string Ressource = ressources[0];

                    switch (Ressource)
                    {

                        case "Rubis":
                            List<string> cardBought = new List<string>();
                            List<List<string>> cardBoughtDetail = new List<List<string>>();

                            cardBought.Add(ressources[0]);

                            cardBoughtDetail.Add(cardBought);
                            //cardBoughtDetail.Add(CostCard);

                                     
                            //RubisCard.Add();
                            break;


                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }



                //get the text displayed in the textbox that has been clicked
            }


        }

        /// <summary>
        /// click on the play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            this.Width = 680;
            this.Height = 780;

            int id = 0;
           
            LoadPlayer(id);

        }


        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) { 

            enableClicLabel = true;

            string name = conn.GetPlayerName(currentPlayerId);

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = "";
            lblChoiceOnyx.Text = "";
            lblChoiceRubis.Text = "";
            lblChoiceSaphir.Text = "";
            lblChoiceEmeraude.Text = "";

            lblChoiceCard.Text = "";

            //no coins selected
            nbDiamand = 0;
            nbOnyx = 0;
            nbRubis = 0;
            nbSaphir = 0;
            nbEmeraude = 0;

            Player player = new Player();
            player.Name = name;
            player.Id = id;
            player.Ressources = new int[] { 2, 0, 1, 1, 1 };
            player.Coins = new int[] { 0, 1, 0, 1, 1 };

            lblPlayerDiamandCoin.Text = player.Coins[0].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[1].ToString();
            lblPlayerRubisCoin.Text = player.Coins[2].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[3].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[4].ToString();
            currentPlayerId = id;

            lblPlayer.Text = "Jeu de " + name;

            cmdPlay.Enabled = false;
        }

        /// <summary>
        /// click on the red coin (rubis) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRubisCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceRubis.Visible = true;
                //TO DO check if possible to choose a coin, update the number of available coin
                nbRubis++;
                lblChoiceRubis.Text = nbRubis + "\r\n";
            }
        }

        /// <summary>
        /// click on the blue coin (saphir) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSaphirCoin_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// click on the black coin (onyx) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblOnyxCoin_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// click on the green coin (emeraude) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmeraudeCoin_Click(object sender, EventArgs e)
        {

            
        }

        /// <summary>
        /// click on the white coin (diamand) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDiamandCoin_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidateChoice_Click(object sender, EventArgs e)
        {
            cmdNextPlayer.Visible = true;
            //TO DO Check if card or coins are selected, impossible to do both at the same time
            
            cmdNextPlayer.Enabled = true;
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertPlayer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A implémenter");
        }

        /// <summary>
        /// click on the next player to tell him it is his turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button

            currentPlayerId = (currentPlayerId % 3) + 1;
            
        }

    }
}
