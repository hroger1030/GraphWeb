namespace GraphWeb
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public eNodeType NodeType { get; set; }
        public int[] Edges { get; set; }
        public bool Visited { get; set; }

        public Node() { }
    }
}