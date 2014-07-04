using System.Collections.Generic;
using Core.Data;
using Core.Service;
using Models;

namespace RegalWorx
{
	public class RwxDataService : DataService, IRwxService
	{
		public List<User> GetAllUsers()
		{
			List<User> list = SelectForModel<User>();
			return list;
		}

		public List<Equipment> GetAllEquipment()
		{
			List<Equipment> list = SelectForModel<Equipment>();
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
