using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

		private void TestForm_Shown(object sender, EventArgs e)
		{
			List<User> list = new Array[5].Cast<object>().Select(i => new User().CreateTestObject()).Cast<User>().ToList();

			Grid.DataSource = list;
		}
	}
}
