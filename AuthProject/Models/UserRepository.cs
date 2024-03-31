using MySqlConnector;
using System.Text;

namespace AuthProject.Models
{


	public class MobsInfo
	{

		public int tier = 0;
		public int type = 2;
		public String localization = null;
	}
	public class UserRepository
	{


		MySqlConnection mySqlConnection = new MySqlConnection();
		MySqlCommand mySqlCommand = new MySqlCommand();

		MySqlDataReader mySqlDataReader = null;


		string server = "serwer2340828.home.pl";
		string username = "37679222_webproduction";
		string password = "1aNJfHZRMuNLYCOrAqSlE11ddKz8";

		string usernameItems = "37679222_webitems";
		string passwordItems = "QGFjPyLDd9cI1D9";

		void connectionString(MySqlConnection mySqlConnection)
		{
			mySqlConnection.ConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
			username + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";
		}

		void connectionStringItems(MySqlConnection mySqlConnection)
		{
			mySqlConnection.ConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
			usernameItems + ";" + "UID=" + usernameItems + ";" + "PASSWORD=" + passwordItems + ";";
		}
		public  Dictionary<int, String> GetItemsIds()
		{

			Dictionary<int, String> keyValues = new Dictionary<int, String>();


			string query = "SELECT *  FROM items";
			MySqlConnection connectionMySql = new MySqlConnection();
			connectionStringItems(connectionMySql);
			connectionMySql.Open();
			MySqlCommand cmd = new MySqlCommand(query, connectionMySql);
			MySqlDataReader dataReader = cmd.ExecuteReader();



			while (dataReader.Read())
			{
				int id = (int)dataReader[0];
				string name = (string)dataReader[1];


				int dataflag = (int)dataReader[2];

				bool flag = false;

				if (dataflag == 1)
				{
					flag = true;
				}

				if(flag)
				{

			
					addItem(keyValues, id, name);
					
			
				}
				else
				{
					keyValues.Add(id, name);
				}

		




			}
			connectionMySql.Close();
			return keyValues;

		}

		public void addItem(Dictionary<int, string> Items , int code, string itemName)
		{



			code = code - 1;

			StringBuilder itemNameBuilder = new StringBuilder(itemName);

			int counter = 0;



			for (int i = 4; i <= 8; i++)
			{


				counter++;
				itemNameBuilder[1] = char.Parse(i.ToString());
				Items.Add(code + counter, itemNameBuilder.ToString());


				for (int j = 1; j <= 4; j++)
				{
					counter++;

					itemName = itemNameBuilder + "@" + j;


					Items.Add(code + counter, itemName);


				}


			}
		}
		public Dictionary<int, object[]> GetMobsIds()
		{

			Dictionary<int, object[]> keyValues = new Dictionary<int, object[]>();

			string query = "SELECT *  FROM mobs";
			MySqlConnection connectionMySql = new MySqlConnection();
			connectionStringItems(connectionMySql);
			connectionMySql.Open();
			MySqlCommand cmd = new MySqlCommand(query, connectionMySql);
			MySqlDataReader dataReader = cmd.ExecuteReader();



			while (dataReader.Read())
			{
				int int0 = (int)dataReader[0];
				int int1 = (int)dataReader[1];
				int int2 = (int)dataReader[2];
				String string0 = (string)dataReader[3];


                object[] mixedArray = new object[]
				{
                    int1,    
					int2,   
					string0,       
				
				};


				keyValues.Add(int0, mixedArray);




			}
			connectionMySql.Close();
			return keyValues;


		}



		public static DateTimeOffset? GetCurrentTime()
		{
			using (var client = new HttpClient())
			{
				try
				{
					var result = client.GetAsync("https://google.com",
						  HttpCompletionOption.ResponseHeadersRead).Result;
					return result.Headers.Date;
				}
				catch
				{
					return null;
				}
			}
		}

	}
}
