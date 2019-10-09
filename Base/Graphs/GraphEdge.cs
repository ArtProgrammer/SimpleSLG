using System;
using System.Collections.Generic;

namespace SimpleAI.Graphs
{
    public class GraphEdge
    {
        private int SrcIdx = 0;

        private int DstIdx = 0;

        private float EdgeCost = 0.0f;

        public int Src
        {
            set
            {
                SrcIdx = value;
            }
            get
            {
                return SrcIdx;
            }
        }

        public int Dst
        {
            set
            {
                DstIdx = value;
            }
            get
            {
                return DstIdx;
            }
        }

        public float Cost
        {
            set
            {
                EdgeCost = value;
            }
            get
            {
                return EdgeCost;
            }
        }

        public GraphEdge()
        {

        }

        public GraphEdge(int src, int dst, float cost)
        {
            SrcIdx = src;
            DstIdx = dst;
            EdgeCost = cost;
        }
    }
}
