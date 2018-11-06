using System;

namespace _20Questions
{

    public class Program
    {

        public static void Main()
        {
            new Program().Run();
        }

        public void Run()
        {
            var keepPlaying = true;

            var head = new Node("Four Legs");
            head.SetYes(new Node("Dog"));
            head.SetNo(new Node("Snake"));
            
            while (keepPlaying)
            {
                PlayRound(head);
                keepPlaying = AskQuestion("Have you thought of an animal?");
            }

            PrintNode(head, 1);
            Console.ReadLine();
        }
        

        public static void PrintNode(Node node, int level)
        {
            if (!node.HasChild())
            {
                var endString = " * " + node.Value;
                Console.WriteLine(endString.PadLeft(endString.Length + level, ' '));
            }

            if (node.Yes != null)
            {
                var yesString = " ->  " + node.Value + " (YES) ";
                Console.WriteLine(yesString.PadLeft(yesString.Length + level, ' '));
                PrintNode(node.Yes, level + 2);
            }

            if (node.No != null)
            {
                var noString = " ->  " + node.Value + " (NO) ";
                Console.WriteLine(noString.PadLeft(noString.Length + level, ' '));
                PrintNode(node.No, level + 2);
            }
        }

        public static void PlayRound(Node startingNode)
        {
            var currentNode = startingNode;

            while (true)
            {
                var answer = "";
                if (currentNode.HasChild())
                {
                    Console.WriteLine("Does it have {0}?", currentNode.Value);
                    answer = Console.ReadLine();
                    currentNode =
                        answer.Equals("Y", StringComparison.CurrentCultureIgnoreCase)
                            ? currentNode.Yes
                            : currentNode.No;
                    continue;
                }

                Console.WriteLine("Is it a {0}?", currentNode.Value);
                answer = Console.ReadLine();
                if (answer.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("I was correct!");
                    break;
                }

                Console.WriteLine("What was it?");
                var newAnimal = Console.ReadLine();
                Console.WriteLine("What does a {0} have that a {1} doesn't?", newAnimal, currentNode.Value);
                var trait = Console.ReadLine();

                currentNode.SetYes(new Node(newAnimal));
                currentNode.SetNo(new Node(currentNode.Value));
                currentNode.Value = trait;
                break;
            }
        }

        public static bool AskQuestion(string question)
        {
            Console.WriteLine(question + " (Y/N) ");
            return Console.ReadLine()
                .Equals("Y", StringComparison.CurrentCultureIgnoreCase);
        }

    }
}
