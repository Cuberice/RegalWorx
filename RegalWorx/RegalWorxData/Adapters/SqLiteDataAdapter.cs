using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using RegalWorxData;
using RegalWorxData.Properties;

namespace RegalWorxData
{
	public class SQLiteDataAdapter : IDbAdapter
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
		public IAdapterCommand CreateCommand(string commandstring)
		{
			return new SQLiteAdapterCommand(commandstring);
		}
		
		public void PerformWithDataReader(string cmdSelect, Func<IAdapterReader, object> perform)
		{
			using (IAdapterConnection conn = new SQLiteAdapterConnection())
			{
				using (IAdapterCommand cmd = new SQLiteAdapterCommand(cmdSelect, conn))
				{
					using (IAdapterReader r = cmd.ExecuteReader())
					{
						if (!r.HasRows())
							return;

						while (r.Read())
						{
							perform(r);
						}
					}
				}
			}
		}

		public bool ExecuteNonQuery(Func<IAdapterCommand> command)
		{
			using (IAdapterConnection conn = new SQLiteAdapterConnection())
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
			return ExecuteNonQuery(() => new SQLiteAdapterCommand(commandstring));
		}

		#endregion

		#region Adapter Classes

		protected class SQLiteAdapterConnection : IAdapterConnection
		{
			public object ConnObject { get; set; }

			public SQLiteConnection SQLiteConnection
			{
				get { return ConnObject as SQLiteConnection; }
				set { ConnObject = value; }
			}

			public ConnectionState State { get; set; }

			public SQLiteAdapterConnection()
			{
				SQLiteConnection = new SQLiteConnection(Settings.Default.DbConnection);

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
		protected class SQLiteAdapterReader : IAdapterReader
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
		protected class SQLiteAdapterCommand : IAdapterCommand
		{
			private SQLiteCommand Command { get; set; }

			public SQLiteAdapterCommand(string commandText)
			{
				Command = new SQLiteCommand(commandText);
			}

			public SQLiteAdapterCommand(string commandText, IAdapterConnection conn)
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
				return new SQLiteAdapterReader {Reader = Command.ExecuteReader()};
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
	}
}
