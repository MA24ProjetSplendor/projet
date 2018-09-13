using System;
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
        }


        /// <summary>
        /// get the list of cards according to the level
        /// </summary>
        /// <returns>cards stack</returns>
        public Stack<Card> GetListCardAccordingToLevel(int level)
        {
            //Get all the data from card table selecting them according to the data
            //TO DO
            //Create an object "Stack of Card"
            Stack<Card> listCard = new Stack<Card>();
            //do while to go to every record of the card table
            //while (....)
            //{
                //Get the ressourceid and the number of prestige points
                //Create a card object
                
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
            //TO DO
            string sql = "CREATE TABLE cost (id INT PRIMARY KEY, prix VARCHAR(20))";
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
        
            InsertInto("CREATE TABLE cards (idcard INT PRIMARY KEY, fkRessource Int, level Int, nbPtPrestige Int, fkPlayer Int)");
            InsertInto("CREATE TABLE ressource (idRessource INT PRIMARY KEY, Nom STRING)");
            InsertInto("CREATE TABLE cost (idCost INT PRIMARY KEY, fkCard INT, fkRessource INT, nbressource INT)");

            // Insérer les données dans la table Ressource

            InsertInto("insert into card(idRessource, Nom) values (1,'Rubis')");
            InsertInto("insert into card(idRessource, Nom) values (2,'Saphir')");
            InsertInto("insert into card(idRessource, Nom) values (3,'Emeraude')");
            InsertInto("insert into card(idRessource, Nom) values (4,'Onyx')");
            InsertInto("insert into card(idRessource, Nom) values (5,'Or')");
            InsertInto("insert into card(idRessource, Nom) values (6,'Diamant')");

            // Insérer les données dans la table Costs
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (2, 2,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (2, 2,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (3, 3,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (3, 3,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (4, 4,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (4, 4,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (5, 5,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (6, 6,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (6, 6,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (7, 7,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (7, 7,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (8, 8,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (8, 8,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (9, 9,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (9, 9,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (9, 9,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (10, 10,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (11, 11,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (11, 11,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (11, 11,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (12, 12,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (13, 13,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (13, 13,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (14, 14,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (14, 14,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (14, 14,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (15, 15,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (15, 15,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (15, 15,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (15, 15,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (16, 16,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (16, 16,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (16, 16,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (17, 17,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (17, 17,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (18, 18,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (19, 19,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (20, 20,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (21, 21,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (21, 21,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (22, 22,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (22, 22,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (23, 23,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (24, 24,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (24, 24,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (24, 24,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (25, 25,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (25, 25,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (25, 25,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (26, 26,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (27, 27,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (27, 27,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (27, 27,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (29, 29,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (29, 29,7");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (30, 30,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (30, 30,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (31, 31,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (31, 31,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (31, 31,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (32, 32,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (33, 33,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (33, 33,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (33, 33,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (34, 34,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (34, 34,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (34, 34,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (35, 35,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (35, 35,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (35, 35,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (36, 36,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (36, 36,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (37, 37,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (37, 37,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (38, 38,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (38, 38,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (39, 39,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (39, 39,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (39, 39,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (40, 40,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (40, 40,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (41, 41,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (42, 42,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (42, 42,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (43, 43,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (45, 45,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (46, 46,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (46, 46,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (47, 47,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (47, 47,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (48, 48,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (49, 49,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (49, 49,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (49, 49,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (51, 51,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (51, 51,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (52, 52,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (53, 53,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (53, 53,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (54, 54,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (55, 55,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (55, 55,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (56, 56,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (57, 57,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (57, 57,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (57, 57,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (58, 58,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (58, 58,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (59, 59,5");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (59, 59,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (60, 60,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (61, 61,6");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (62, 62,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (62, 62,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (62, 62,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (63, 63,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (64, 64,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (64, 64,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (65, 65,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (65, 65,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (66, 66,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (66, 66,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (66, 66,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (67, 67,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (67, 67,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (68, 68,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (68, 68,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (69, 69,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (70, 70,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (70, 70,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (70, 70,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (70, 70,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (71, 71,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (71, 71,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (71, 71,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (72, 72,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (72, 72,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (72, 72,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (72, 72,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (73, 73,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (74, 74,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (74, 74,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (74, 74,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (76, 76,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (77, 77,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (77, 77,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (78, 78,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (78, 78,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (79, 79,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (79, 79,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (79, 79,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (81, 81,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (81, 81,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (82, 82,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (83, 83,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (84, 84,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (84, 84,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (85, 85,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (85, 85,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (85, 85,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (86, 86,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (86, 86,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (86, 86,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (87, 87,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (88, 88,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (88, 88,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (88, 88,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (89, 89,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (90, 90,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (91, 91,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (91, 91,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (92, 92,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (92, 92,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (93, 93,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (93, 93,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (93, 93,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (94, 94,4");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (95, 95,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (95, 95,3");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (96, 96,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (96, 96,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (96, 96,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (97, 97,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (97, 97,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (97, 97,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (98, 98,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (98, 98,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (99, 99,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (100, 100,1");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (100, 100,2");
            InsertInto("insert into cost(idCost, fkCard, fkRessource, nbRessource) values (100, 100,1");



            // Insérer les données dans la table Cards

            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (2, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (3, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (4, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (5, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (6, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (7, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (8, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (9, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (10, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (11, 0,4,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (12, 0,3,5");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (13, 0,3,5");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (14, 0,3,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (15, 0,3,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (16, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (17, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (18, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (19, 0,3,5");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (20, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (21, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (22, 0,3,5");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (23, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (24, 0,3,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (25, 0,3,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (26, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (27, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (28, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (29, 0,3,5");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (30, 0,3,4");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (31, 0,3,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (32, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (33, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (34, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (35, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (36, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (37, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (38, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (39, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (40, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (41, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (42, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (43, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (44, 0,2,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (45, 0,2,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (46, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (47, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (48, 0,2,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (49, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (50, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (51, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (52, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (53, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (54, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (55, 0,2,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (56, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (57, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (58, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (59, 0,2,2");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (60, 0,2,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (61, 0,2,3");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (62, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (63, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (64, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (65, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (66, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (67, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (68, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (69, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (70, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (71, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (72, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (73, 0,1,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (74, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (75, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (76, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (77, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (78, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (79, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (80, 0,1,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (81, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (82, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (83, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (84, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (85, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (86, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (87, 0,1,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (88, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (89, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (90, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (91, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (92, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (93, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (94, 0,1,1");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (95, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (96, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (97, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (98, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (99, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (100, 0,1,0");
            InsertInto("insert into card(idcard, fkRessource, level, nbPtPrestige) values (101, 0,1,1");

        }

    }
}
