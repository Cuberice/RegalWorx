using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegalWorxData
{
    public interface IRWDataAdapter
    {
	    void GetConnection();
	    void CreateStructure();
    }
}
