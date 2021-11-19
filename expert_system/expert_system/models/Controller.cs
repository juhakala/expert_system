using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expert_system.models
{
	class Controller
	{
		private Input _input;
		public Input input { get { return _input; } private set { _input = value; } }
		public Controller()
		{
			input = new Input();
		}
	}
}
