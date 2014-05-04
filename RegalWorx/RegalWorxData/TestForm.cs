using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using RegalWorxData.Service;

namespace RegalWorxData
{
	public partial class TestForm : Form
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new TestForm());
		}
		public TestForm()
		{
			InitializeComponent();
		}

		protected DataGridView Grid { get { return grid; } }
		protected IRWDataService DataService { get; private set; }
		protected SQLBuilder Builder { get; private set; }

		private void TestForm_Shown(object sender, EventArgs e)
		{
			DataService = new RWDataService();
			Builder = new SQLBuilder(DataService.Adapter);
			Builder.CreateStructure();

			Grid.DataSource = DataService.GetAllUsers();
		}

		private void GenerateTestData_Click(object sender, EventArgs e)
		{
			List<User> users = new Array[5].Cast<object>().Select(i => new User().CreateTestObject()).Cast<User>().ToList();
			List<Equipment> equipment = new Array[3].Cast<object>().Select(i => new Equipment().CreateTestObject()).Cast<Equipment>().ToList();

			users.ForEach(u => DataService.InsertUser(u));
			equipment.ForEach(eq => DataService.InsertEquipment(eq));
		}
	}
}
