using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFlowV21.Structure
{
    public class Node
    {
		public int Id = 0;
		public int SignalProcessorId = 0;
		public int DataStorageId = 0;

		public int PosX = 0;
		public int PosY = 0;

		// Additional
		public string Name;

		//cross refs
		public int[] inputConnections;
		public int[] outputConnections;
	}
}
