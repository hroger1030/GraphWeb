using System;
using System.Collections.Generic;

using log4net;
using RandomNumbers;

namespace GraphWeb
{
    public class NodeFactory
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(NodeFactory));

        private const int MIN_NODES = 5;

        private RandomGenerator _Rand;
        private int _NodeId;
        private Dictionary<int, Node> _Graph;

        public NodeFactory(int nodeCount)
        {
            if (nodeCount < MIN_NODES)
                throw new ArgumentException($"Graph must have at least {MIN_NODES} nodes");

            if (_Log.IsInfoEnabled)
                _Log.Info($"Initializing Node Factory object with {nodeCount} nodes");

            _Rand = new RandomGenerator();
            _NodeId = 0;
            _Graph = new Dictionary<int, Node>();

            while (nodeCount > 0)
            {
                var new_node = GenerateRandomUnexploredNode();
                _Graph.Add(new_node.Id, new_node);
                nodeCount--;
            }
        }

        private Node GenerateRandomUnexploredNode()
        {
            var output = new Node();

            output.Id = _NodeId;
            output.Name = _Rand.String(4, 8);
            output.NodeType = _Rand.EnumValue<eNodeType>();
            output.Edges = new int[_Rand.Int(0, 4)];
            output.Visited = false;

            _NodeId++;

            return output;
        }

        public Node ExploreNode(int nodeId)
        {
            if (!_Graph.TryGetValue(nodeId, out Node node))
                return null;

            if (node.Visited == false)
            {
                for (int i = 0; i < node.Edges.Length; i++)
                {
                    // connect node to existing edge
                    node.Edges[i] = _Rand.Int(0, _NodeId - 1);
                }

                node.Visited = true;
            }

            return node;
        }
    }
}