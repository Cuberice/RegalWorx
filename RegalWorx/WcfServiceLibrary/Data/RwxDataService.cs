using System.Collections.Generic;
using Core.Data;
using Core.Models;
using Core.Service;
using Models;

namespace RegalWorx
{
	public class RwxDataService : DataService, IRwxService
	{
		public List<User> GetAllUsers()
		{
			List<User> list = new List<User>();
			Adapter.PerformWithDataReader(SelectCommandString<User>(), r => list.Add(ModelExtensions.CreateInstance<User>(r)));

			return list;
		}

		public List<Equipment> GetAllEquipment()
		{
			List<Equipment> list = new List<Equipment>();
			Adapter.PerformWithDataReader(SelectCommandString<Equipment>(), r => list.Add(ModelExtensions.CreateInstance<Equipment>(r)));

			return list;
		}

		public void InsertUser(User user)
		{
			Adapter.ExecuteNonQuery(() => Adapter.CreateInsertCommand(user));
		}
		public void InsertEquipment(Equipment equipment)
		{
			Adapter.ExecuteNonQuery(() => Adapter.CreateInsertCommand(equipment));
		}
	}
}
