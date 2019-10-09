using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAI.Graphs
{
    public class SparseGraph<TNode, TEdge>
        where TNode : GraphNode, new()
        where TEdge : GraphEdge, new()
    {
        private int Invalid_idx = -1;

        private int NodesCount = 0;

        private Dictionary<int, TNode> Nodes = new Dictionary<int, TNode>();

        private int EdgesCount = 0;

        private Dictionary<int, List<TEdge>> Edges = new Dictionary<int, List<TEdge>>();

        private bool IsDirectionGraph = false;

        private int NextNodeIndex = 0;

        public int NumNodes
        {
            get
            {
                return NodesCount;
            }
        }

        public int NumEdges
        {
            get
            {
                return EdgesCount;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return NodesCount == 0;
            }
        }

        private bool UniqueEdge(int src, int dst)
        {
            if (!Edges.ContainsKey(src)) return true;

            for (int i = 0; i < Edges[src].Count; i++)
            {
                if (Edges[src][i].Dst == dst)
                {
                    return false;
                }
            }
            return true;
        }

        private void CullInvalidEdges()
        {
            foreach (var item in Edges)
            {
                if (item.Value.Count == 0)
                {
                    Edges.Remove(item.Key);
                }
                else
                {
                    var EdgeList = item.Value;
                    for (int i = 0; i < EdgeList.Count; i++)
                    {
                        if (Nodes[EdgeList[i].Dst].ID == Invalid_idx ||
                            Nodes[EdgeList[i].Src].ID == Invalid_idx)
                        {
                            EdgeList.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public SparseGraph(bool dirgraph)
        {
            IsDirectionGraph = dirgraph;
        }

        public TNode GetNode(int idx)
        {
            if (Nodes.ContainsKey(idx))
            {
                return Nodes[idx];
            }

            return null;
        }

        public TEdge GetEdge(int src, int dst)
        {
            if (Edges.ContainsKey(src))
            {
                return Edges[src][dst];
            }
            return null;
        }

        public List<TEdge> GetEdgesFrom(int id)
        {
            if (Edges.ContainsKey(id))
            {
                return Edges[id];
            }
            return null;
        }

        public int GetNextFreeID()
        {
            return NextNodeIndex;
        }

        public int AddNode(TNode node)
        {
            if (!Nodes.ContainsKey(node.ID))
            {
                Nodes[node.ID] = node;
                NodesCount++;
                NextNodeIndex++;

                return node.ID;
            }

            return Invalid_idx;
        }

        public void AddEdge(TEdge edge)
        {
            if (!System.Object.ReferenceEquals(edge, null))
            {
                if (Nodes[edge.Src].ID != Invalid_idx &&
                    Nodes[edge.Dst].ID != Invalid_idx)
                {
                    if (!Edges.ContainsKey(edge.Src))
                    {
                        Edges[edge.Src] = new List<TEdge>();
                    }

                    if (UniqueEdge(edge.Src, edge.Dst))
                    {
                        Edges[edge.Src].Add(edge);
                        EdgesCount++;
                    }
                    
                    if (IsDirectionGraph)
                    {
                        TEdge ed = new TEdge();
                        ed.Src = edge.Dst;
                        ed.Dst = edge.Src;
                        ed.Cost = edge.Cost;

                        if (!Edges.ContainsKey(ed.Src))
                        {
                            Edges[ed.Src] = new List<TEdge>();
                        }

                        Edges[ed.Src].Add(ed);
                        EdgesCount++;
                    }
                }
            }
        }

        public void SetEdgeCost(int src, int dst, float cost)
        {
            if (Edges.ContainsKey(src))
            {
                foreach (var edge in Edges[src])
                {
                    if (edge.Dst == dst)
                    {
                        edge.Cost = cost;
                        break;
                    }
                }
            }
        }

        public bool IsNodePresent(int idx)
        {
            return Nodes.ContainsKey(idx);
        }

        public bool IsEdgePresent(int src, int dst)
        {
            if (!Edges.ContainsKey(src)) return false;
            
            for (int i = 0; i < Edges[src].Count; i++)
            {
                if (Edges[src][i].Dst == dst)
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveNode(int idx)
        {
            if (Nodes.ContainsKey(idx))
            {
                Nodes[idx].ID = Invalid_idx;

                if (!IsDirectionGraph)
                {
                    foreach (var item in Edges)
                    {
                        foreach (var edge in item.Value)
                        {
                            if (edge.Dst == idx)
                            {
                                item.Value.Remove(edge);
                            }
                        }
                    }

                    Edges[idx].Clear();
                }
                else
                {
                    CullInvalidEdges();
                }

                Nodes.Remove(idx);
                NodesCount--;
            }
        }

        public void RemoveEdge(int src, int dst)
        {
            if (!Edges.ContainsKey(src)) return;

            for (int i = 0; i < Edges[src].Count; i++)
            {
                if (Edges[src][i].Dst == dst)
                {
                    Edges[src].RemoveAt(i);
                    EdgesCount--;
                    break;
                }
            }

            if (!IsDirectionGraph)
            {
                if (!Edges.ContainsKey(dst)) return;

                for (int i = 0; i < Edges[dst].Count; i++)
                {
                    if (Edges[dst][i].Dst == src)
                    {
                        Edges[dst].RemoveAt(i);
                        EdgesCount--;
                        break;
                    }
                }
            }
        }
    }
}
