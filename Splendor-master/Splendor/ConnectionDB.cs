﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Splendor
{
    /// <summary>
    /// contains methods and attributes to connect and deal with the database
    /// TO DO : le modèle de données n'est pas super, à revoir!!!!
    /// </summary>
    class ConnectionDB
    {

        //connection to the database
        private SQLiteConnection m_dbConnection;

        /// <summary>
        /// constructor : creates the connection to the database SQLite
        /// </summary>
        public ConnectionDB()
        {

            SQLiteConnection.CreateFile("Splendor.sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=Splendor.sqlite;Version=3;");
            m_dbConnection.Open();

            //create and insert players
            CreateInsertPlayer();
            //Create and insert cards
            //TO DO
            CreateInsertCards();
            //Create and insert ressources
            //TO DO
            CreateInsertRessources();
            //Create and insert Coin
            CreateInsertCoin();
        }

        private void CreateInsertCoin()
        {
            string sql = "CREATE TABLE NbCoin (IdNbCoin INTEGER PRIMARY KEY AUTOINCREMENT, fkPlayer INT, fkressource INT, nbCoin INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
        }


        public int[] GetPlayerCoin(int id)
        {
            string sql = "select * from NbCoin where fkPlayer ="+id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int[] coins = new int[5];

            int coin = 0;
            int PlayerRessource = 0;
        
            while (reader.Read())
            {
                coin = (int)reader["nbCoin"];
                PlayerRessource = (int)reader["fkRessource"];
                coins[PlayerRessource] = coin;
            }

            return coins;


           /* int NbPlayers = (Int32)command.ExecuteScalar();

            for(int i=0;i<=NbPlayers;i++)
            {

                coins[i] = 




                string insertCoin = "insert into NbCoin(NbCoin) VALUES(0) where FkPlayer ="+i;
                SQLiteCommand cmd = new SQLiteCommand(insertCoin, m_dbConnection);
            }*/
       
        }


        /// <summary>
        /// get the list of cards according to the level
        /// </summary>
        /// <returns>cards stack</returns>
        public Stack<Card> GetListCardAccordingToLevel(int level)
        {
            //Get all the data from card table selecting them according to the data
            string sql = "select * from card where level=" + level;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();



            //TO DO
            //Create an object "Stack of Card"
            Stack<Card> listCard = new Stack<Card>();
            //do while to go to every record of the card table
            //Get the ressourceid and the number of prestige points

            while (reader.Read())
            {
                Card card = new Card();
                card.Level = (int)reader["level"];
                card.PrestigePt = (int)reader["nbPtPrestige"];
                card.IdCard = (int)reader["idcard"];
                card.Ress = (Ressources)reader["fkRessource"];

                sql = "select * from cost where fkCard = " + card.IdCard;
                SQLiteCommand commande = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader Costreader = commande.ExecuteReader();

                while(Costreader.Read())
                {
                    card.Cout[(int)Costreader["fkRessource"] - 1] = (int)Costreader["nbRessource"];
                }

                listCard.Push(card);
            }



            //while (....)

            //select the cost of the card : look at the cost table (and other)

            //do while to go to every record of the card table
            //while (....)
            //{
            //get the nbRessource of the cost
            //}
            //push card into the stack

            //}
            return listCard;
        }


        /// <summary>
        /// create the "player" table and insert data
        /// </summary>
        private void CreateInsertPlayer()
        {
            string sql = "CREATE TABLE player (id INT PRIMARY KEY, pseudo VARCHAR(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into player (id, pseudo) values (0, 'Fred')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into player (id, pseudo) values (1, 'Harry')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into player (id, pseudo) values (2, 'Sam')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }


        /// <summary>
        /// get the name of the player according to his id
        /// </summary>
        /// <param name="id">id of the player</param>
        /// <returns></returns>
        public string GetPlayerName(int id)
        {
            string sql = "select pseudo from player where id = " + id;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader["pseudo"].ToString();
            }
            return name;
        }

        /// <summary>
        /// create the table "ressources" and insert data
        /// </summary>
        private void CreateInsertRessources()
        {

            InsertInto("CREATE TABLE ressource (idRessource INT PRIMARY KEY, Nom STRING)");
            
            // Insérer les données dans la table Ressource

            InsertInto("insert into ressource(idRessource, Nom) values (1,'Rubis')");
            InsertInto("insert into ressource(idRessource, Nom) values (2,'Emeraude')");
            InsertInto("insert into ressource(idRessource, Nom) values (3,'Onyx')");
            InsertInto("insert into ressource(idRessource, Nom) values (4,'Saphir')");
            InsertInto("insert into ressource(idRessource, Nom) values (5,'Diamant')");
            InsertInto("insert into ressource(idRessource, Nom) values (6,'Or')");
        }

        private void InsertInto(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }


        /// <summary>
        ///  create tables "cards", "cost", "ressource" and insert data
        /// </summary>
        private void CreateInsertCards()
        {
            // Créer la table

            InsertInto("CREATE TABLE card (idcard INT PRIMARY KEY, fkRessource Int, level Int, nbPtPrestige Int, fkPlayer Int)");  
            InsertInto("CREATE TABLE cost (idCost INTEGER PRIMARY KEY AUTOINCREMENT , fkCard INT, fkRessource INT, nbRessource INT)");
         
            // Insérer les données dans la table Costs

            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (2,2,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (2,4,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (3,1,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (3,2,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (4,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (4,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (4,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (5,3,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (5,5,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (6,1,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (6,3,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (7,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (7,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (7,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (8,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (8,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (8,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (9,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (9,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (9,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (10,4,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (10,5,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (11,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (11,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (11,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (12,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (12,5,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (13,1,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (13,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (14,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (14,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (14,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (14,5,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (15,1,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (15,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (15,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (15,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (16,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (16,2,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (16,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (17,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (17,4,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (17,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (18,3,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (19,3,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (19,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (20,2,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (21,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (21,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (21,5,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (22,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (22,4,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (23,1,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (24,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (24,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (24,4,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (24,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (25,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (25,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (25,3,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (25,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (26,4,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (27,1,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (27,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (27,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (28,5,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (29,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (29,2,7)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (30,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (30,3,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (30,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (31,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (31,2,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (31,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (31,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (32,1,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (33,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (33,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (33,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (34,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (34,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (34,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (35,1,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (35,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (35,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (36,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (36,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (36,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (37,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (37,4,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (38,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (38,3,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (38,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (39,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (39,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (39,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (40,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (40,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (40,5,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (41,2,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (42,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (42,2,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (43,3,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (43,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (44,5,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (45,4,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (46,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (46,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (46,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (47,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (47,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (47,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (48,1,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (49,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (49,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (49,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (50,5,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (51,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (51,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (51,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (52,4,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (53,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (53,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (53,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (54,3,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (55,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (55,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (55,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (56,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (56,5,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (57,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (57,2,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (57,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (58,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (58,4,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (58,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (59,1,5)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (59,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (60,2,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (61,3,6)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (62,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (62,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (62,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (63,1,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (64,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (64,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (64,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (65,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (65,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (65,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (66,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (66,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (66,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (66,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (67,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (67,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (68,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (68,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (69,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (70,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (70,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (70,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (70,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (71,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (71,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (71,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (72,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (72,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (72,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (72,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (73,2,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (74,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (74,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (74,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (74,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (75,5,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (76,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (76,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (77,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (77,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (78,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (78,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (78,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (79,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (79,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (79,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (79,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (80,5,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (81,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (81,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (81,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (82,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (82,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (83,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (84,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (84,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (85,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (85,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (85,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (85,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (86,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (86,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (86,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (86,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (87,4,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (88,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (88,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (88,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (88,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (89,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (89,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (90,3,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (91,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (91,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (91,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (92,2,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (92,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (93,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (93,2,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (93,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (94,1,4)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (95,2,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (95,4,3)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (95,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (96,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (96,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (96,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (97,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (97,3,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (97,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (97,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (98,1,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (98,4,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (99,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (99,5,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (100,1,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (100,3,2)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (100,4,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (100,5,1)");
            InsertInto("insert into cost(fkCard, fkRessource, nbRessource) values (101,4,4)");


            // Insérer les données dans la table Cards

            InsertInto("insert into card(idcard, level, nbPtPrestige) values (2,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (3,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (4,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (5,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (6,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (7,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (8,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (9,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (10,4,3)");
            InsertInto("insert into card(idcard, level, nbPtPrestige) values (11,4,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (12, 4,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (13, 3,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (14, 2,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (15, 5,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (16, 1,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (17, 2,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (18, 5,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (19, 5,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (20, 1,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (21, 4,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (22, 2,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (23, 3,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (24, 1,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (25, 4,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (26, 2,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (27, 3,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (28, 4,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (29, 1,3,5)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (30, 5,3,4)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (31, 3,3,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (32, 5,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (33, 1,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (34, 5,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (35, 5,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (36, 5,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (37, 2,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (38, 4,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (39, 4,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (40, 2,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (41, 2,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (42, 3,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (43, 1,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (44, 5,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (45, 4,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (46, 2,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (47, 3,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (48, 1,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (49, 4,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (50, 3,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (51, 2,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (52, 4,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (53, 1,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (54, 1,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (55, 3,2,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (56, 4,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (57, 3,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (58, 1,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (59, 5,2,2)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (60, 2,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (61, 3,2,3)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (62, 3,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (63, 2,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (64, 1,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (65, 5,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (66, 4,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (67, 5,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (68, 5,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (69, 5,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (70, 5,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (71, 5,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (72, 5,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (73, 5,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (74, 1,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (75, 1,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (76, 1,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (77, 1,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (78, 1,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (79, 1,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (80, 1,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (81, 3,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (82, 3,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (83, 3,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (84, 3,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (85, 3,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (86, 3,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (87, 3,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (88, 4,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (89, 4,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (90, 4,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (91, 4,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (92, 4,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (93, 4,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (94, 4,1,1)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (95, 2,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (96, 2,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (97, 2,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (98, 2,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (99, 2,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (100, 2,1,0)");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (101, 2,1,1)");


        }

    }
}
