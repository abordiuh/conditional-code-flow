using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFlowV21.Structure
{
    public class Node
    {
		public int nodeId = 0;
		public int signalProcessorId = 0;
		public int dataStorageId = 0;

		public int posX = 0;
		public int posY = 0;

		// Additional
		public string Name;

		//cross refs
		public int[] inputConnections;
		public int[] outputConnections;
	}
}
