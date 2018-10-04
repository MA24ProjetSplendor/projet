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
            string sql = "CREATE TABLE ressource (idRessource INT PRIMARY KEY, Nom STRING)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            
            // Insérer les données dans la table Ressource

            string sql1 = "insert into ressource(idRessource, Nom) values (1,'Rubis')";
            command = new SQLiteCommand(sql1, m_dbConnection);
            command.ExecuteNonQuery();
            string sql2 = "insert into ressource(idRessource, Nom) values (2,'Saphir')";
            command = new SQLiteCommand(sql2, m_dbConnection);
            command.ExecuteNonQuery();
            string sql3 = "insert into ressource(idRessource, Nom) values (3,'Onyx')";
            command = new SQLiteCommand(sql3, m_dbConnection);
            command.ExecuteNonQuery();
            string sql4 = "insert into ressource(idRessource, Nom) values (4,'Emeraude')";
            command = new SQLiteCommand(sql4, m_dbConnection);
            command.ExecuteNonQuery();
            string sql5 = "insert into ressource(idRessource, Nom) values (5,'Diamant')";
            command = new SQLiteCommand(sql5, m_dbConnection);
            command.ExecuteNonQuery();
            string sql6 = "insert into ressource(idRessource, Nom) values (6,'Or')";
            command = new SQLiteCommand(sql6, m_dbConnection);
            command.ExecuteNonQuery();

        }

        private void LoadIdRessource()
        {
            
            string sql = "select ";
        }

        /// <summary>
        ///  create tables "cards", "cost" and insert data
        /// </summary>
        private void CreateInsertCards()
        {
           //TO DO
        }

    }
}
