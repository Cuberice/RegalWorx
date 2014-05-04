using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegalWorxData;

namespace Common
{
	public interface IDbAdapter
	{
		#region Command Building
		string SPECIAL(Column c);
		string DATATYPE(Column c);
		string CREATETABLE();
		#endregion

		#region Data Execution

		IAdapterCommand CreateCommand(string commandstring);
		void PerformWithDataReader(string cmdSelect, Func<IAdapterReader, object> perform);

		bool ExecuteNonQuery(Func<IAdapterCommand> command);
		bool ExecuteNonQuery(string commandstring);
		#endregion
	}

	public interface ITestObject
	{
		object CreateTestObject();
	}
	public interface IDebuggerObject
	{
		string GetDebuggerDisplay();
	}
	
	public interface IAdapterConnection : IDisposable
	{
		ConnectionState State { get; set; }
		object ConnObject { get; set; }
		void Open();
	}
	public interface IAdapterReader : IDisposable
	{
		bool HasRows();
		bool Read();

		object GetValue(string columnname);
	}
	public interface IAdapterCommand : IDisposable
	{
		IAdapterReader ExecuteReader();
		void SetConnection(IAdapterConnection conn);
		void AddParameter(string name, object value);
		int ExecuteNonQuery();
	}
}
