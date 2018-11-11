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
        public int nbRubis;
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        private int nbSaphir;
        private int nbtotal;
   
        private const int maxSameCoin = 2;


        private List<Player> player = new List<Player>();

        private Stack<Card> listCardOne = new Stack<Card>();
        private Stack<Card> listCardTwo = new Stack<Card>();
        private Stack<Card> listCardThree = new Stack<Card>();
        private Stack<Card> listCardFour = new Stack<Card>();


        //id of the player that is playing
        private int currentPlayerId=0;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn;

        Player p;

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
            conn = new ConnectionDB();
           

            lblGoldCoin.Text = "5";
            lblDiamandCoin.Text = "7";
            lblEmeraudeCoin.Text = "7";
            lblOnyxCoin.Text = "7";
            lblRubisCoin.Text = "7";
            lblSaphirCoin.Text = "7";

            // Get Player Coin

            int[] coins = conn.GetPlayerCoin(1);

            //load cards from the database
            //they are not hard coded any more
            //TO DO

            //load cards from the database

            //load cards level 1

            listCardOne = conn.GetListCardAccordingToLevel(1);
            int i = 0;
            int nbstack = listCardOne.Count();

            foreach (Control ctrl in flwCardLevel1.Controls)
            {
                if (i < nbstack)
                {
                    ctrl.Text = listCardOne.Pop().ToString();
                    i++;
                }

            }

            //load cards level 2
            listCardTwo = conn.GetListCardAccordingToLevel(2);
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
            listCardThree = conn.GetListCardAccordingToLevel(3);
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
            /*
            listCardFour = conn.GetListCardAccordingToLevel(4);

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

        /// <summary>
        /// ClickOnCard: Event Buy Card (Léo)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO

            // Get Value of the textbox and display informations in a popup
            TextBox txtBox = sender as TextBox;

            MessageBox.Show(txtBox.Text);

            int[] costCard = new int[5]; // Array for the cost of the Card  
            string[] cardInformations = txtBox.Text.Split('\t'); // informations of the card
            bool canBuy = true; // If the player can buy

            string costString = cardInformations[2]; // Cost the card format String
            string ptPrestigeString = cardInformations[1]; // Pt Prestige of the card format String
            string cardRessource = cardInformations[0]; // Ressource of the card

            int ptPrestige;

            // Pattern to parse the informations
            string pattern = @"[^0-9a-zA-Z\n]";

            Regex rex = new Regex(pattern); // Create a new pattern

            string result = rex.Replace(costString, ""); // Replace all characters that doesn't match the pattern

            string[] couts = result.Split('\n'); // Array of the cost

            // Parse the cost of the card and put in a Array
            for (int i = 0; i < couts.Length; i++)
            {
                string ress = couts[i];
                int ressQuantity = 0;
                int resLenght = ress.Length;
                if (resLenght > 0)
                {
                    string ressType = ress.Substring(0, resLenght - 1);

                    ressQuantity = System.Convert.ToInt32(ress.Substring(resLenght - 1));

                    switch (ressType)
                    {
                        case "Rubis": costCard[0] = ressQuantity; break;

                        case "Emeraude": costCard[1] = ressQuantity; break;

                        case "Onyx": costCard[2] = ressQuantity; break;

                        case "Saphir": costCard[3] = ressQuantity; break;

                        case "Diamand": costCard[4] = ressQuantity; break;
                    }
                }
            }

            // Check if the player has enough ressources to buy the card
            for (int i = 0; i < costCard.Length; i++)
            {
                costCard[i] -= player[currentPlayerId].Ressources[i];
                costCard[i] -= player[currentPlayerId].Coins[i];
                // The player can't buy the card
                if (costCard[i] > 0)
                {
                    canBuy = false;
                }
            }

            // The player can buy the card
            if (canBuy)
            {
                // Remove ressource from the player and buy the card
                for (int i = 0; i < costCard.Length; i++)
                {
                    player[currentPlayerId].Coins[i] -= costCard[i] - player[currentPlayerId].Ressources[i];
                }

                switch (cardRessource)
                {
                    case "Rubis": player[currentPlayerId].Ressources[0] += 1; break;

                    case "Emeraude": player[currentPlayerId].Ressources[1] += 1; break;

                    case "Onyx": player[currentPlayerId].Ressources[2] += 1; break;

                    case "Saphir": player[currentPlayerId].Ressources[3] += 1; break;

                    case "Diamand": player[currentPlayerId].Ressources[4] += 1; break;
                }

                // Add prestige points to the player
                if (ptPrestigeString == "") 
                {
                    ptPrestige = 0;
                }
                else ptPrestige = System.Convert.ToInt32(ptPrestigeString);
  
                if (ptPrestige != 0)
                {
                    player[currentPlayerId].ptPrestige += ptPrestige;
                }

                MessageBox.Show("Vous avez acheté cette carte");

                // Clear the card and replace with another one
                txtBox.Clear();

                // Replace level 1 card
                if(txtBox.Name.Contains("txtLevel1"))
                {
                    if(listCardOne.Count > 0)
                    {
                        txtBox.Text = listCardOne.Pop().ToString();
                    }    
                }
                // Replace level 2 card
                else if (txtBox.Name.Contains("txtLevel2"))
                {
                    if (listCardTwo.Count > 0)
                    {
                        txtBox.Text = listCardTwo.Pop().ToString();
                    }
                }
                // Replace level 3 card
                else if (txtBox.Name.Contains("txtLevel3"))
                {
                    if(listCardThree.Count > 0)
                    {
                        txtBox.Text = listCardThree.Pop().ToString();
                    }         
                }

            }
            else
            {
                MessageBox.Show("Vous n'avez pas assez pour cette carte");
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
        private void LoadPlayer(int id)
        {
            // Show the validate choice button
            cmdValidateChoice.Visible = true;
            cmdValidateChoice.Enabled = true;

            // Hide button next player
            cmdNextPlayer.Visible = false;
            cmdNextPlayer.Enabled = false;

            // Put currentId to 0 if the currentId is bigger than the number of player
            if(currentPlayerId >= conn.GetNumberPlayer())
            {
                currentPlayerId = 0;
            }

            string name = conn.GetPlayerName(currentPlayerId);

            // Create a new Player for each player in the database
            if(player.Count() < conn.GetNumberPlayer())
            {
                p = new Player();
                player.Add(p);
                p.Id = currentPlayerId;
                p.Name = name;
                // Set all coins to 0
                p.Coins[0] = 0;
                p.Coins[1] = 0;
                p.Coins[2] = 0;
                p.Coins[3] = 0;
                p.Coins[4] = 0;
                // Set all ressources to 0
                p.Ressources[0] = 0;
                p.Ressources[1] = 0;
                p.Ressources[2] = 0;
                p.Ressources[3] = 0;
                p.Ressources[4] = 0;

            }

            enableClicLabel = true;

        
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

            // Display coins of the current player
            lblPlayerRubisCoin.Text = player[currentPlayerId].Coins[0].ToString();  
            lblPlayerEmeraudeCoin.Text = player[currentPlayerId].Coins[1].ToString();
            lblPlayerOnyxCoin.Text = player[currentPlayerId].Coins[2].ToString();
            lblPlayerSaphirCoin.Text = player[currentPlayerId].Coins[3].ToString();
            lblPlayerDiamandCoin.Text = player[currentPlayerId].Coins[4].ToString();

            // Display ressources of the current player
            txtPlayerRubisCard.Text = player[currentPlayerId].Ressources[0].ToString();
            txtPlayerEmeraudeCard.Text = player[currentPlayerId].Ressources[1].ToString();
            txtPlayerOnyxCard.Text = player[currentPlayerId].Ressources[2].ToString();
            txtPlayerSaphirCard.Text = player[currentPlayerId].Ressources[3].ToString();
            txtPlayerDiamandCard.Text = player[currentPlayerId].Ressources[4].ToString();

            nbtotal = 0;

            lblPlayer.Text = "Jeu de " + name;

            cmdPlay.Enabled = false;
        }
     
        /// <summary>
        /// </summary>
        /// <param name="labelChoix"></param>
        /// <param name="labelJeton"></param>
        /// <param name="nbJeton"></param>
        /// <param name="type"></param>
        void TestJetons(Label labelChoix, Label labelJeton, int nbJeton, string type) //method test des jetons
        {
            
            int jeton = Convert.ToInt32(labelJeton.Text);

            const int maxSameCoin = 2;
            
            if (jeton == 2 && nbJeton == 1)
            {
                MessageBox.Show("Vous ne pouvez pas prendre deux jetons de cette couleur");
            }
            else
            {
                if (nbRubis == maxSameCoin || nbSaphir == maxSameCoin || nbOnyx == maxSameCoin || nbEmeraude == maxSameCoin || nbDiamand == maxSameCoin)
                {
                    MessageBox.Show("Nombre max de pièces de la même couleur = 2");
                }
                else
                {
                    if ((nbJeton == 1 && nbSaphir == 1) || (nbJeton == 1 && nbOnyx == 1) || (nbJeton == 1 && nbEmeraude == 1) || (nbJeton == 1 && nbDiamand == 1))
                    {
                        MessageBox.Show("Vous ne pouvez pas prendre un deuxiéme jeton de la même couleur si vous avez déjà choisi un jeton de couleur différente");
                    }

             
                    if (nbtotal >= 3)
                    {
                        MessageBox.Show("Vous ne pouvez pas prendre plus de jetons");
                    }

                    else
                    {
                        nbtotal = nbRubis + nbSaphir + nbOnyx + nbEmeraude + nbDiamand;
                        if (nbtotal >= 3)
                        {
                            MessageBox.Show("Vous ne pouvez pas prendre plus de jetons");
                        }
                        else
                        {
                            nbJeton++;
                            int nbJetonsDispo = jeton - 1;
                            labelJeton.Text = nbJetonsDispo.ToString();
                            labelChoix.Text = nbJeton + "\r\n";

                            switch (type)
                            {
                                case "Rubis": nbRubis++; nbtotal = nbSaphir + nbRubis + nbEmeraude + nbOnyx + nbDiamand; break;

                                case "Saphir": nbSaphir++; nbtotal = nbSaphir + nbRubis + nbEmeraude + nbOnyx + nbDiamand; break;

                                case "Emeraude": nbEmeraude++; nbtotal = nbSaphir + nbRubis + nbEmeraude + nbOnyx + nbDiamand; break;

                                case "Onyx": nbOnyx++; nbtotal = nbSaphir + nbRubis + nbEmeraude + nbOnyx + nbDiamand; break;

                                case "Diamand": nbDiamand++; nbtotal = nbSaphir + nbRubis + nbEmeraude + nbOnyx + nbDiamand; break;

                            }
                           

                            }
                        }
                    }
                    
                }
            }
        /// <summary>
        /// ErreurJetonSelect: if the player made a mistake, he can unselect the coin
        /// </summary>
        void ErreurJetonSelect(string ressource)
        {
            switch(ressource)
            {
                case "Rubis":
                    nbRubis = nbRubis - 1;
                    int var = Convert.ToInt32(lblRubisCoin.Text) - 1;

                    if (nbRubis == 0)
                    {
                        lblChoiceRubis.Visible = false;
                        var = Convert.ToInt32(lblRubisCoin.Text) + 1;
                        lblRubisCoin.Text = var.ToString();

                    }
                    else
                    {
                        var = Convert.ToInt32(lblRubisCoin.Text) + 1;
                        lblRubisCoin.Text = var.ToString();
                        lblChoiceRubis.Text = nbRubis + "\r\n";
                    }

                    nbtotal--;
                    break;

                case "Emeraude":
                    nbEmeraude = nbEmeraude - 1;
                    var = Convert.ToInt32(lblEmeraudeCoin.Text) - 1;

                    if (nbEmeraude == 0)
                    {
                        lblChoiceEmeraude.Visible = false;
                        var = Convert.ToInt32(lblEmeraudeCoin.Text) + 1;
                        lblEmeraudeCoin.Text = var.ToString();
                    }
                    else
                    {
                        var = Convert.ToInt32(lblEmeraudeCoin.Text) + 1;
                        lblEmeraudeCoin.Text = var.ToString();
                        lblChoiceEmeraude.Text = nbEmeraude + "\r\n";
                    }

                    nbtotal--;
                    break;

                case "Onyx":
                    nbOnyx = nbOnyx - 1;
                    var = Convert.ToInt32(lblOnyxCoin.Text) - 1;

                    if (nbOnyx == 0)
                    {
                        lblChoiceOnyx.Visible = false;
                        var = Convert.ToInt32(lblOnyxCoin.Text) + 1;
                        lblOnyxCoin.Text = var.ToString();

                    }
                    else
                    {
                        var = Convert.ToInt32(lblOnyxCoin.Text) + 1;
                        lblOnyxCoin.Text = var.ToString();
                        lblChoiceOnyx.Text = nbOnyx + "\r\n";
                    }

                    nbtotal--;
                    break;

                case "Saphir":
                    nbSaphir = nbSaphir - 1;
                    var = Convert.ToInt32(lblSaphirCoin.Text) - 1;

                    if (nbSaphir == 0)
                    {
                        lblChoiceSaphir.Visible = false;
                        var = Convert.ToInt32(lblSaphirCoin.Text) + 1;
                        lblSaphirCoin.Text = var.ToString();

                    }
                    else
                    {
                        var = Convert.ToInt32(lblSaphirCoin.Text) + 1;
                        lblSaphirCoin.Text = var.ToString();
                        lblChoiceSaphir.Text = nbSaphir + "\r\n";
                    }

                    nbtotal--;
                    break;

                case "Diamand":
                    nbDiamand = nbDiamand - 1;
                    var = Convert.ToInt32(lblDiamandCoin.Text) - 1;

                    if (nbDiamand == 0)
                    {
                        lblChoiceDiamand.Visible = false;
                        var = Convert.ToInt32(lblDiamandCoin.Text) + 1;
                        lblDiamandCoin.Text = var.ToString();

                    }
                    else
                    {
                        var = Convert.ToInt32(lblDiamandCoin.Text) + 1;
                        lblDiamandCoin.Text = var.ToString();
                        lblChoiceDiamand.Text = nbDiamand + "\r\n";
                    }

                    nbtotal--;
                    break;
            }
            
           
            currentPlayerId = (currentPlayerId % 3) + 1;

            if (nbtotal == 0)
            {
                cmdValidateChoice.Visible = false;
            }
        }

        /// <summary>
        /// click on the red coin (rubis) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblRubisCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceRubis.Visible = true;
                TestJetons(lblChoiceRubis, lblRubisCoin, nbRubis,"Rubis");
            }
        }

        /// <summary>
        /// click on the blue coin (saphir) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblSaphirCoin_Click(object sender, EventArgs e)
        {

            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceSaphir.Visible = true;
                TestJetons(lblChoiceSaphir, lblSaphirCoin, nbSaphir, "Saphir");
            }
   
        }

            /// <summary>
            /// click on the black coin (onyx) to tell the player has selected this coin
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>

            void lblOnyxCoin_Click(object sender, EventArgs e)
            {
                if (enableClicLabel)
                {
                    cmdValidateChoice.Visible = true;
                    lblChoiceOnyx.Visible = true;
                    TestJetons(lblChoiceOnyx, lblOnyxCoin, nbOnyx, "Onyx");

                }
            }

        /// <summary>
        /// click on the green coin (emeraude) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblEmeraudeCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceEmeraude.Visible = true;
                TestJetons(lblChoiceEmeraude, lblEmeraudeCoin, nbEmeraude, "Emeraude");
            }
        }

        /// <summary>
        /// click on the white coin (diamand) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lblDiamandCoin_Click(object sender, EventArgs e)
        {
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceDiamand.Visible = true;
                TestJetons(lblChoiceDiamand, lblDiamandCoin, nbDiamand, "Diamand");
            }
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmdValidateChoice_Click(object sender, EventArgs e)
        {

            if (nbtotal >= 2)
            {
                if (nbRubis > 0)
                {
                    if (player[currentPlayerId].Name == "")
                    {
                        p.Coins[0] = nbRubis;          
                    }
                    else
                    {
                        player[currentPlayerId].Coins[0] += nbRubis;
                    }          
                }

                if (nbEmeraude > 0)
                {
                    if (player[currentPlayerId].Name == "")
                    {
                        p.Coins[1] = nbEmeraude;
                    }
                    else
                    {
                        player[currentPlayerId].Coins[1] += nbEmeraude;
                    }
                }

                if (nbOnyx > 0)
                {
                    if (player[currentPlayerId].Name == "")
                    {
                        p.Coins[2] = nbOnyx;
                    }
                    else
                    {
                        player[currentPlayerId].Coins[2] += nbOnyx;
                    }
                }

                if (nbSaphir > 0)
                {
                    if (player[currentPlayerId].Name == "")
                    {
                        p.Coins[3] = nbSaphir;
                    }
                    else
                    {
                        player[currentPlayerId].Coins[3] += nbSaphir;
                    }
                }

                if (nbDiamand > 0)
                {
                    if (player[currentPlayerId].Name == "")
                    {
                        p.Coins[4] = nbDiamand;
                    }
                    else
                    {
                        player[currentPlayerId].Coins[4] += nbDiamand;
                    }
                }

                MessageBox.Show("Votre choix a été validé, cliquez sur le bouton 'joueur suivant'");
                cmdValidateChoice.Visible = false;
                cmdValidateChoice.Enabled = false;
                cmdNextPlayer.Visible = true;
                cmdNextPlayer.Enabled = true;
            }
            else
            {
                cmdNextPlayer.Visible = false;
                cmdNextPlayer.Enabled = false;
                MessageBox.Show("Merci de choisir des jetons");
            }    
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //a terminer 
        private void cmdInsertPlayer_Click(object sender, EventArgs e)

        {
            lblNewPlayer.Visible = true;
            txtNewPlayer.Visible = true;
            cmdValiderNewPlayer.Visible = true;

            
        }

        /// <summary>
        /// click on the next player to tell him it is his turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            currentPlayerId++;
            LoadPlayer(currentPlayerId);


            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button

        }

        void lblChoiceRubis_Click(object sender, EventArgs e)
        {
            ErreurJetonSelect("Rubis");
        }

        void lblChoiceSaphir_Click(object sender, EventArgs e)
        {
            ErreurJetonSelect("Saphir");
        }

        void lblChoiceOnyx_Click(object sender, EventArgs e)
        {
            ErreurJetonSelect("Onyx");
        }

        private void lblChoiceEmeraude_Click(object sender, EventArgs e)
        {
            ErreurJetonSelect("Emeraude");
        }

        private void lblChoiceDiamand_Click(object sender, EventArgs e)
        {
            ErreurJetonSelect("Diamand");
        }


        private void cmdValiderNewPlayer_Click(object sender, EventArgs e)
        {
            string name = txtNewPlayer.Text;

            if(txtNewPlayer.Text == "")
            {
                MessageBox.Show("Le champ ne peut pas être vide");
            }
            else
            {
                conn.CreateNewPlayer(name);
            }
            

            lblNewPlayer.Visible = false;
            txtNewPlayer.Visible = false;
            cmdValiderNewPlayer.Visible = false;



        }

        private void lblPlayerRubisCoin_Click(object sender, EventArgs e)
        {

        }
    }
}

