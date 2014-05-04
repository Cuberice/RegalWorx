using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RegalWorxData;

namespace RegalWorxData
{
	public class MySqlDataAdapter
	{
		#region Command Building
		public string CREATETABLE()
		{
			return "CREATE TABLE {0} ({1});";
		}
		public string SPECIAL(Column c)
		{
			if (c.PrimaryKey)
				return "PRIMARY KEY";

			string str = c.Unique ? "UNIQUE" : string.Empty;

			if (c.NotNull)
				str += "NOT NULL";

			return str;
		}
		public string DATATYPE(Column c)
		{
			switch (c.ColumnType) //Use propertyType to determine ColumnType if Null
			{
				case Column.DataType.Guid: return "GUID";
				case Column.DataType.Integer: return "INT";
				case Column.DataType.String: return "NVARCHAR(30)";
				case Column.DataType.Text: return "NVARCHAR(100)";
				case Column.DataType.LargeText: return "NVARCHAR(500)";
				case Column.DataType.Double: return "DOUBLE";
				case Column.DataType.DateTime: return "DATETIME";
			}

			return "UNDEFINED DATATYPE";
		}
		#endregion

		#region Data Execution

		public void PerformWithDataReader(string cmdSelect, Func<IAdapterReader, object> perform)
		{
			
		}

		public bool ExecuteNonQuery(Func<IAdapterCommand> command)
		{
			using (IAdapterConnection conn = new MySQLAdapterConnection())
			{
				using (IAdapterCommand cmd = command())
				{
					try
					{
						cmd.SetConnection(conn);
						return cmd.ExecuteNonQuery() > 0;
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
						return false;
					}
				}
			}
		}
		public bool ExecuteNonQuery(string commandstring)
		{
			return ExecuteNonQuery(() => new MySQLAdapterCommand(commandstring));
		}

		#endregion

		#region Adapter Classes
		protected class MySQLAdapterConnection : IAdapterConnection
		{
			public object ConnObject { get; set; }
			public SQLiteConnection SQLiteConnection { get { return ConnObject as SQLiteConnection; } set { ConnObject = value; } }
			public ConnectionState State { get; set; }

			public MySQLAdapterConnection()
			{
				SQLiteConnection = new SQLiteConnection(Properties.Settings.Default.DbConnection);

				while (SQLiteConnection.State != ConnectionState.Closed)
				{
					Thread.Sleep(100);
				}
				Open();
			}
			public void Open()
			{
				SQLiteConnection.Open();
			}
			public void Dispose()
			{
				SQLiteConnection.Dispose();
			}
		}
		protected class MySQLAdapterReader : IAdapterReader
		{
			public SQLiteDataReader Reader { get; set; }

			public bool HasRows()
			{
				return Reader.HasRows;
			}
			public bool Read()
			{
				return Reader.Read();
			}
			public object GetValue(string columnname)
			{
				return Reader.GetValue(Reader.GetOrdinal(columnname));
			}

			public void Dispose()
			{
				Reader.Dispose();
			}
		}
		protected class MySQLAdapterCommand : IAdapterCommand
		{
			private SQLiteCommand Command { get; set; }

			public MySQLAdapterCommand(string commandText)
			{
				Command = new SQLiteCommand(commandText);
			}
			public MySQLAdapterCommand(string commandText, IAdapterConnection conn)
			{
				Command = new SQLiteCommand(commandText, conn.ConnObject as SQLiteConnection);
			}

			public void SetConnection(IAdapterConnection conn)
			{
				Command.Connection = conn.ConnObject as SQLiteConnection;
			}
			public void AddParameter(string name, object value)
			{
				Command.Parameters.AddWithValue(name, value);
			}

			public IAdapterReader ExecuteReader()
			{
				return new MySQLAdapterReader { Reader = Command.ExecuteReader() };
			}
			public int ExecuteNonQuery()
			{
				throw new NotImplementedException();
			}

			public void Dispose()
			{
				Command.Dispose();
			}
		}
		#endregion

		public List<User> GetAllUsers()
		{
			PerformWithDataReader("Select * from TBL_USER", reader =>
			{
				SQLiteDataReader sreader = (SQLiteDataReader)reader;
				return null;
			});
			return new List<User>();
		}

		public void InsertUser(User user)
		{
			throw new NotImplementedException();
		}

		public void InsertEquipment(Equipment equipment)
		{
			throw new NotImplementedException();
		}
		
	}
}
