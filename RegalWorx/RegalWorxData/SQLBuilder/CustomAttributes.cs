using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace RegalWorxData
{
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
	public class Table : Attribute
	{
		public string TableName;

    public Table(string table)
    {
			TableName = table;
    }
		public static List<Type> GetAllTypes()
		{
			return Assembly.GetExecutingAssembly().GetTypes().Where(t => IsDefined(t, typeof(Table))).ToList();
		}
		public static Table GetAll(Type t)
		{
			return (Table)t.GetCustomAttribute(typeof (Table));
		}
	}

	[DebuggerDisplay("{GetDebuggerDisplay()}")]
	[AttributeUsage(AttributeTargets.Property)]
	public class Column : Attribute, IDebuggerObject
	{
		public string Name;
		public DataType ColumnType;
		public PropertyInfo Property;
		public bool PrimaryKey = false;
		public bool Unique = false;
		public bool NotNull = false;
		public string OldName;

    public Column(string column, DataType columnType)
    {
	    Name = column;
	    ColumnType = columnType;
    }

		/// <summary>
		/// Returns all Column Data (Incl PropertyInfo) for each defined Property of given Type
		/// </summary>
		/// <param name="t">Model Class with defined Table and Column decorations</param>
		/// <returns></returns>
		public static List<Column> GetAll(Type t)
		{
			return t.GetProperties().Where(p => p.IsDefined(typeof(Column))).Select(p =>
				{
					Column c = (Column) p.GetCustomAttribute(typeof (Column));
					c.Property = p;
					return c;
				}).ToList();
		}

		public enum DataType
		{
			Guid, Integer, String, Double, DateTime, Text, LargeText, Custom
		}

		public string GetDebuggerDisplay()
		{
			return string.Format("{0}:[{1}]", PrimaryKey ? "Pk."+Name : Name, ColumnType);
		}
	}
}
