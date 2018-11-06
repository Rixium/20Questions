namespace _20Questions
{
    public class Node
    {

        public Node Yes;
        public Node No;
        public Node Parent;
        public string Value;

        public Node(string value)
        {
            Value = value;
        }

        public bool HasChild()
        {
            return Yes != null || No != null;
        }

        public void SetYes(Node node)
        {
            node.Parent = this;
            Yes = node;
        }

        public void SetNo(Node node)
        {
            node.Parent = this;
            No = node;
        }
    }
}
