using System;
using System.Globalization;

namespace _20Questions
{

    public class Program
    {

        public static void Main()
        {
            new Program().Run();
            Console.ReadLine();
        }

        /* This function runs until the player decides they no longer want to keep playing.
         * When the player ends the game, the constructed tree will be outputted to the console.
         */
        public void Run()
        {
            var keepPlaying = true;
            var head = InitialiseTree();
            
            while (keepPlaying)
            {
                PlayRound(head);
                Console.WriteLine();
                keepPlaying = AskQuestion("Have you thought of an animal?");
            }

            PrintNode(head, 1);
        }

        /* Setup function for the tree.
         * Creates a new node with a question, and two leaves.
         */
        public static Node InitialiseTree()
        {
            return new Node("Four Legs")
                .SetYes("Dog")
                .SetNo("Snake");
        }

        /* Recursively prints each level of the tree,
         * and pads the strings appropriately depending on the nodes depth.
         */
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

        /* Game loop will run until a node has no child.
         * Once the node has no child, it will be given as the answer to 20 questions, and if the answer is
         * incorrect, a new question and answer will be added to the tree, ready for the next round.
         */
        public static void PlayRound(Node startingNode)
        {
            var currentNode = startingNode;
            while (currentNode.HasChild())
            {
                Console.WriteLine("Does it have {0}?", currentNode.Value);
                var answer = Console.ReadLine();
                currentNode =
                    answer.Equals("Y", StringComparison.CurrentCultureIgnoreCase)
                        ? currentNode.Yes
                        : currentNode.No;
            }

            var correct = AttemptGuess(currentNode);

            if (!correct)
                AddNewQuestion(currentNode);
            else
                Console.WriteLine("I win!");
        }

        /* Asks the player what they were thinking of, and a trait of the animal,
         * which are then added to the tree.
         */
        private static void AddNewQuestion(Node currentNode)
        {
            Console.WriteLine("What was it?");
            var newAnimal = ConvertToTitleCase(Console.ReadLine());
            Console.WriteLine("What does a {0} have that a {1} doesn't?", newAnimal, currentNode.Value);
            var trait = ConvertToTitleCase(Console.ReadLine());

            currentNode.SetYes(newAnimal);
            currentNode.SetNo(currentNode.Value);
            currentNode.Value = trait;
        }

        /* When the computer arrives to a leaf node, it will attempt to guess correctly depending
         * on the nodes value, and return whether it was correct or not.
         */
        private static bool AttemptGuess(Node node)
        {
            Console.WriteLine("Is it a {0}?", node.Value);
            var answer = Console.ReadLine();
            return answer.Equals("Y", StringComparison.CurrentCultureIgnoreCase);
        }

        /* Converts player input to title case,
         * so the tree has consistently named values.
         */
        private static string ConvertToTitleCase(string word)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(word);
        }

        /* Asks a passed string as a yes/no question to the user, and returns a boolean value
         * depending on the answer given.
         */
        public static bool AskQuestion(string question)
        {
            Console.WriteLine(question + " (Y/N) ");
            return Console.ReadLine()
                .Equals("Y", StringComparison.CurrentCultureIgnoreCase);
        }

    }
}
