using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace RegalWorxData.Service
{
	public class RWDataService : IRWDataService
	{
		public IDbAdapter Adapter { get; set; }

		public List<User> GetAllUsers()
		{
			List<User> list = new List<User>();
			Adapter.PerformWithDataReader("Select * from TBL_USER", reader =>
				{
					list.Add(ModelUtils.CreateInstance<User>(reader));
					return null;
				});			
					
//			PerformWithDataReader("Select * from TBL_USER", reader =>
//			{
//				SQLiteAdapterReader r = (SQLiteAdapterReader) reader;
//				SQLiteDataReader s = r.Reader;
//				
//				return null;
//			});
					
			return list;
		}
			
		public void InsertUser(User user)
		{
			Adapter.ExecuteNonQuery(() =>
			{
				IAdapterCommand cmd = Adapter.CreateCommand("INSERT INTO TBL_USER(ID, NAME, TYPE)	VALUES(@id, @name, @type_id)");
				cmd.AddParameter("@id", user.ID);
				cmd.AddParameter("@name", user.Name);
				cmd.AddParameter("@type_id", (int)user.Type);
				return cmd;
			});
		}
		
		public void InsertEquipment(Equipment equipment)
		{
			Adapter.ExecuteNonQuery(() =>
			{
				IAdapterCommand cmd = Adapter.CreateCommand("INSERT INTO TBL_EQUIPMENT(ID, NAME, TYPE)	VALUES(@id, @name, @type_id)");
				cmd.AddParameter("@id", equipment.ID);
				cmd.AddParameter("@name", equipment.Name);
				cmd.AddParameter("@type_id", equipment.Type.ID);
				return cmd;
			});
		}
	}
}
