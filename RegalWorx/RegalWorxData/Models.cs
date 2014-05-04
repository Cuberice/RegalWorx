using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace RegalWorxData
{
	[DebuggerDisplay("{GetDebuggerDisplay()}")]
	[Table("TBL_USER")]
	public struct User : ITestObject, IDebuggerObject
	{
		[Column("ID", Column.DataType.Guid, PrimaryKey = true)]
		public Guid ID { get; set; }

		[Column("NAME", Column.DataType.String, NotNull = true)]
		public string Name { get; set; }

		[Column("TYPE", Column.DataType.Guid, NotNull = true)]
		public UserType Type { get; set; }

		public User(Guid id, string name, int typeid) : this()
		{
			ID = id;
			Name = "";
			Type = UserType.Admin;
		}
		
		public Object CreateTestObject()
		{
			ID = Guid.NewGuid();
			Name = Extentions.GetRandomString(8);
			Type = UserType.Normal;

			return this;
		}
		public string GetDebuggerDisplay()
		{
			return ToString();
		}
		public override string ToString()
		{
			return string.Format("User ID: {0}, Name: {1}, Type: {2}", ID, Name, Type);
		}
	}
	public enum UserType
	{
		Admin, Supervisor, Normal
	}

	[DebuggerDisplay("{GetDebuggerDisplay()}")]
	[Table("TBL_EQUIPMENT")]
	public struct Equipment : ITestObject, IDebuggerObject
	{
		[Column("ID", Column.DataType.Guid, PrimaryKey = true)]
		public Guid ID { get; private set; }

		[Column("NAME", Column.DataType.String, NotNull = true)]
		public string Name { get; private set; }

		[Column("MODEL", Column.DataType.String)]
		public string Model { get; private set; }

		[Column("SERIALNUMBER", Column.DataType.String)]
		public string SerialNumber { get; private set; }

		[Column("COST", Column.DataType.Double)]
		public double Cost { get; private set; }

		[Column("PURCHASEDATE", Column.DataType.DateTime)]
		public DateTime PurchaseDate { get; private set; }

		[Column("DECOMMISIONDATE", Column.DataType.DateTime)]
		public DateTime DecommisionDate { get; private set; }

		[Column("MAKE", Column.DataType.Guid)]
		public EquipmentMake Make { get; private set; }

		[Column("TYPE", Column.DataType.Guid)]
		public EquipmentType Type { get; private set; }

		public object CreateTestObject()
		{
			ID = Guid.NewGuid();
			Name = Extentions.GetRandomString(8);
			PurchaseDate = DateTime.Now;
			Make = (EquipmentMake)new EquipmentMake().CreateTestObject();
			Type = (EquipmentType)new EquipmentType().CreateTestObject();

			return this;
		}
		public string GetDebuggerDisplay()
		{
			return ToString();
		}

		public override string ToString()
		{
			return string.Format("Equipment Name: {0}, Type: {1}, Make: {2}", Name, Type, Make);
		}
	}

	[Table("TBL_EQUIPMENT_MAKE")]
	public struct EquipmentMake : ITestObject
	{
		[Column("ID", Column.DataType.Guid, PrimaryKey = true)]
		public Guid ID { get; private set; }

		[Column("NAME", Column.DataType.String, NotNull = true)]
		public string Name { get; private set; }

		public object CreateTestObject()
		{
			ID = Guid.NewGuid();
			Name = "Test Equipment Make";

			return this;
		}
		public override string ToString()
		{
			return Name;
		}
	}

	[Table("TBL_EQUIPMENT_TYPE")]
	public struct EquipmentType : ITestObject
	{
		[Column("ID", Column.DataType.Guid, PrimaryKey = true)]
		public Guid ID { get; private set; }

		[Column("DESCRIPTION", Column.DataType.String, NotNull = true)]
		public string Description { get; private set; }

		public object CreateTestObject()
		{
			ID = Guid.NewGuid();
			Description = "Test Equipment Type";

			return this;
		}
		public override string ToString()
		{
			return Description;
		}
	}

	public static class ModelUtils
	{
		public static T CreateInstance<T>(IAdapterReader reader) where T : new()
		{
			List<Column> columns = Column.GetAll(typeof(T));
			object t = new T();
			foreach (Column c in columns)
			{
				try
				{
					object value = reader.GetValue(c.Name);
					c.Property.SetValue(t, value);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			return (T)t;
		}		
	}
}
