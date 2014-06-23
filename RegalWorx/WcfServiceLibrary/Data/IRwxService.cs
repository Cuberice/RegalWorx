using System.Collections.Generic;
using System.ServiceModel;
using Models;


namespace RegalWorx
{
	[ServiceContract]
	public interface IRwxService
	{
		[OperationContract]
		List<User> GetAllUsers();

		[OperationContract]
		List<Equipment> GetAllEquipment();

		[OperationContract]
		void InsertUser(User user);

		[OperationContract]
		void InsertEquipment(Equipment equipment);
	}
}
