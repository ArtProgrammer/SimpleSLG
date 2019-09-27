using System;
using System.Collections.Generic;

namespace SimpleAI.Graphs
{
    class GraphNode
    {
        private List<GraphEdge> InEdges = new List<GraphEdge>();

        private int IDx = -1;

        public GraphNode()
        {

        }

        public GraphNode(int idx)
        {
            IDx = idx;
        }

        public int ID
        {
            set
            {
                IDx = value;
            }
            get
            {
                return IDx;
            }
        }
        
        public void AddEdge(GraphEdge edge)
        {
            InEdges.Add(edge);
        }

        public void RemoveEdge(GraphEdge edge)
        {
            InEdges.Remove(edge);
        }
    }
}
