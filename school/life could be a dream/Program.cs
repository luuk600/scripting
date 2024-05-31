using System;

class SimpleChatbot
{
    static void Main(string[] args)
    {
        string userInput = string.Empty;
        Console.WriteLine("Hallo! Ik ben je eenvoudige chatbot. Type 'exit' om het programma te sluiten.");

        while (true)
        {
            Console.Write("Jij: ");
            userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == "exit")
            {
                Console.WriteLine("Chatbot: Tot ziens!");
                break;
            }

            switch (userInput)
            {
                case "hallo":
                case "hoi":
                    Console.WriteLine("Chatbot: Hallo! Hoe kan ik je helpen?");
                    break;
                case "hoe gaat het":
                case "hoe gaat het met je":
                    Console.WriteLine("Chatbot: Met mij gaat het goed, dank je! En met jou?");
                    break;
                case "wat is je naam":
                    Console.WriteLine("Chatbot: Ik ben een eenvoudige chatbot, ik heb geen naam.");
                    break;
                case "wat kan je doen":
                    Console.WriteLine("Chatbot: Ik kan eenvoudige gesprekken voeren. Probeer eens iets te vragen!");
                    break;
                default:
                    Console.WriteLine("Chatbot: Sorry, dat begrijp ik niet. Probeer iets anders.");
                    break;
            }
        }
    }
}