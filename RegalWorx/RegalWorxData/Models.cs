using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegalWorxData
{
	public interface ITestObject
	{
		Object CreateTestObject();
	}

	public struct User : ITestObject
	{
		public Guid ID { get; private set; }
		public string Name { get; private set; }
		public UserType Type { get; private set; }

		public Object CreateTestObject()
		{
			ID = Guid.NewGuid();
			Name = Extentions.GetRandomString(8);
			Type = UserType.Normal;

			return this;
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

	public struct Equipment
	{
		public Guid ID { get; private set; }
		public string Name { get; private set; }
		public EquipmentMake Make { get; private set; }
		public string Model { get; private set; }
		public string SerialNumber { get; private set; }
		public double Cost { get; private set; }
		public DateTime PurchaseDate { get; private set; }
		public DateTime DecommisionDate { get; private set; }
		public EquipmentType Type { get; private set; }

		public override string ToString()
		{
			return string.Format("Equipment Name: {0}, Type: {1}, Make: {2}", Name, Type, Make);
		}
	}
	public struct EquipmentMake
	{
		public Guid ID { get; private set; }

		[DisplayName]
		public string Name { get; private set; }
	}
	public struct EquipmentType
	{
		public Guid ID { get; private set; }

		[DisplayName]
		public string Description { get; private set; }
	}
}
