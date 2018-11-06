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

        public Node SetYes(string value)
        {
            Yes = new Node(value)
            {
                Parent = this
            };
            return this;
        }

        public Node SetNo(string value)
        {
            No = new Node(value)
            {
                Parent = this
            };
            return this;
        }
    }
}
