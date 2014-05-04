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
		protected IDbAdapter Adapter { get; private set; }
		protected SQLBuilder Builder { get; private set; }

		private void TestForm_Shown(object sender, EventArgs e)
		{
			Adapter = new SQLiteDataAdapter();
			Builder = new SQLBuilder(Adapter);
			Builder.CreateStructure();

			Grid.DataSource = Adapter.GetAllUsers();
		}

		private void GenerateTestData_Click(object sender, EventArgs e)
		{
			List<User> users = new Array[5].Cast<object>().Select(i => new User().CreateTestObject()).Cast<User>().ToList();
			List<Equipment> equipment = new Array[3].Cast<object>().Select(i => new Equipment().CreateTestObject()).Cast<Equipment>().ToList();

			users.ForEach(u => Adapter.InsertUser(u));
			equipment.ForEach(eq => Adapter.InsertEquipment(eq));
		}
	}
}
