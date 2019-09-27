using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAI.Graphs
{
    class GraphSearchDijkstra<GraphType>
            where GraphType : SparseGraph<GraphNode, GraphEdge>
    {
        private GraphType Graph = null;

        private List<GraphSearchState> Visited = new List<GraphSearchState>();

        private List<int> Route = new List<int>();

        private List<GraphEdge> SpanningTree = new List<GraphEdge>();

        private List<int> PathToTarget = new List<int>();

        private GraphEdge DummyEdge = new GraphEdge();

        private List<GraphEdge> ShortestPathTree = new List<GraphEdge>();

        private List<float> CostToThisNode = new List<float>();

        private List<GraphEdge> SearchFrontier = new List<GraphEdge>();

        private int SrcID = -1;

        private int DstID = -1;

        private bool IsFound = false;

        public bool Found
        {
            get
            {
                return IsFound;
            }
        }

        ~GraphSearchDijkstra()
        {
            //Clear();
        }

        public GraphSearchDijkstra(GraphType graph,
            int srcid,
            int dstid)
        {
            Graph = graph;
            SrcID = srcid;
            DstID = dstid;

            PrapareData();
        }

        public GraphSearchDijkstra(GraphType graph)
        {
            Graph = graph;

            PrapareData();
        }

        private void PrapareData()
        {
            for (int i = 0; i < Graph.NumNodes; i++)
            {
                Route.Add(-1);
                Visited.Add(GraphSearchState.UnVisited);

                ShortestPathTree.Add(null);
                SearchFrontier.Add(null);
                CostToThisNode.Add(0.0f);
            }
        }

        private void Reset()
        {
            SrcID = -1;
            DstID = -1;
            IsFound = false;

            //Visited.Clear();
            //Route.Clear();
            SpanningTree.Clear();
            PathToTarget.Clear();

            for (int i = 0; i < Graph.NumNodes; i++)
            {
                Route[i] = -1;
            }

            for (int i = 0; i < Graph.NumNodes; i++)
            {
                Visited[i] = GraphSearchState.UnVisited;
            }
        }

        private void Clear()
        {
            Route.Clear();
            Visited.Clear();
            Reset();
        }

        private bool DarkSearch()
        {
            if (SrcID != -1 && DstID != -1)
            {
                Stack<GraphEdge> stacks = new Stack<GraphEdge>();

                //SortedSet<float>

                DummyEdge.Src = SrcID;
                DummyEdge.Dst = SrcID;
                DummyEdge.Cost = 0;

                stacks.Push(DummyEdge);

                while (stacks.Count != 0)
                {
                    GraphEdge next = stacks.Pop();

                    Route[next.Dst] = next.Src;

                    if (next != DummyEdge)
                    {
                        SpanningTree.Add(next);
                    }

                    Visited[next.Dst] = GraphSearchState.Visited;

                    if (next.Dst == DstID)
                    {
                        return true;
                    }

                    foreach (var edge in Graph.GetEdgesFrom(next.Dst))
                    {
                        if (Visited[edge.Dst] == GraphSearchState.UnVisited)
                        {
                            stacks.Push(edge);
                        }
                    }
                }
            }

            return false;
        }

        public void Search()
        {
            Reset();

            IsFound = DarkSearch();
        }

        public void Search(int src, int dst)
        {
            Reset();

            SrcID = src;
            DstID = dst;

            IsFound = DarkSearch();
        }

        public List<int> GetPathToTarget()
        {
            if (!IsFound || DstID < 0) return PathToTarget;

            int nd = DstID;

            PathToTarget.Add(nd);

            while (nd != SrcID)
            {
                nd = Route[nd];

                PathToTarget.Add(nd);
            }

            return PathToTarget;
        }

        public List<GraphEdge> GetSearchTree()
        {
            return SpanningTree;
        }

        public List<GraphEdge> GetSPT()
        {
            return ShortestPathTree;
        }

        public float GetCostToTarget()
        {
            return CostToThisNode[DstID];
        }

        public float GetCostToNode(int id)
        {
            return CostToThisNode[id];
        }
    }
}
