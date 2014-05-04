using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RegalWorxData
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RWDataService" in both code and config file together.
	public class RWDataService : IRWDataService
	{
		public List<User> GetAllUsers()
		{
			IDbAdapter adapter = new SQLiteDataAdapter();

			return adapter.GetAllUsers();
		}

		public void InsertUser(User user)
		{
			IDbAdapter adapter = new SQLiteDataAdapter();

			adapter.InsertUser(user);
		}

		public void InsertEquipment(Equipment equipment)
		{
			throw new NotImplementedException();
		}
	}
}
