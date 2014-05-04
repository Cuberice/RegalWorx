using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RegalWorxData
{
	[ServiceContract]
	public interface IRWDataService
	{
		[OperationContract]
		List<User> GetAllUsers();

		[OperationContract]
		void InsertUser(User user);		
		
		[OperationContract]
		void InsertEquipment(Equipment equipment);
	}
}
