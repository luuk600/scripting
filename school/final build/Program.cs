using System;
using System.Collections.Generic;
using System.Threading;

class SimpleChatbot
{
    // Indexes to cycle through jokes and fun facts
    static int jokeIndex = 0;
    static int factIndex = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Hello! I'm your chatbot. Feel free to ask me anything or say 'exit' to end the conversation.");

        // Initialize a dictionary to hold potential responses
        Dictionary<string, string> responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Responses to "How are you?"
            { "how are you", "I'm fine, thank you for asking!" },
            { "how r u", "I'm doing well, thank you!" },
            { "how are u", "I'm great, thanks for asking!" },
            { "hru", "I'm good, how about you?" },

            // Additional responses
            { "hi", "Hello! How can I help you today?" },
            { "hello", "Hi there! What can I do for you?" },
            { "are you a computer", "Yes, I am a computer program designed to assist you." },
            { "are you ai", "Yes, I am an AI chatbot here to help you with your questions." },
            { "are you a robot", "You could say that. I'm a virtual assistant created to answer your queries." },
            { "what is your name", "I'm just a simple chatbot!" },
            { "what time is it", $"It's currently {DateTime.Now.ToShortTimeString()}." },
            { "what is 2 + 2", "2 + 2 equals 4." },
            { "what is the capital of France", "The capital of France is Paris." },
            { "what is the weather like today", "I'm not sure, you might want to check a weather app!" },
            { "what is your favorite color", "I don't have a favorite color, unfortunately." },
            { "who is the president of the United States", "As of my last update, the president of the United States is Joe Biden." },
            { "what is the meaning of life", "The meaning of life is a profound question that has intrigued humanity for centuries. Philosophers, scientists, and theologians have all pondered its significance. Some believe that the meaning of life is to seek happiness and fulfillment through relationships, achievements, and personal growth. Others find meaning in religious or spiritual beliefs, suggesting that life is a journey towards understanding, connection, and perhaps an afterlife. Evolutionary biologists might argue that the meaning of life is to survive and reproduce, ensuring the continuation of our species. Existentialists, on the other hand, propose that life has no inherent meaning, and it is up to each individual to create their own purpose through actions, choices, and experiences. Ultimately, the meaning of life may be subjective, varying greatly from person to person. It could be the pursuit of knowledge, the quest for love, the desire to make a positive impact, or simply the experience of living each moment to its fullest. In essence, the meaning of life is a deeply personal journey that each person must navigate in their own way." },
            { "what is the speed of light", "The speed of light in a vacuum is approximately 299,792 kilometers per second." },
            { "tell me about yourself", "I'm just a chatbot created to help answer your questions! Feel free to ask me anything." },
            { "what is the largest mammal", "The blue whale is the largest mammal, both in terms of length and weight." },
            { "what is the currency of Japan", "The currency of Japan is the yen." },
            { "what is the tallest mountain in the world", "Mount Everest, located in the Himalayas, is the tallest mountain in the world measured from sea level." },
            { "what is the boiling point of water", "The boiling point of water at sea level is 100 degrees Celsius or 212 degrees Fahrenheit." },
            { "what is the capital of Canada", "The capital of Canada is Ottawa." },
            { "what is the meaning of love", "Love is a complex and profound emotion that can mean different things to different people." },
            { "what is the distance to the moon", "The average distance from the Earth to the Moon is about 384,400 kilometers (238,900 miles)." },
            { "ok","Glad to help you ;)" },

            // More basic responses
            { "what can you do", "I can answer questions, tell jokes, share fun facts, and chat with you!" },
            { "info", "I'm a chatbot created to assist with various questions and provide information." },
            { "how old are you", "I don't have an age like humans do, but I was created recently to assist you!" },
            { "what is the date", $"Today's date is {DateTime.Now.ToShortDateString()}." },
            { "why are you alive", "I exist to help answer your questions and provide assistance." },
            { "who made you", "I was created by a team of developers at OpenAI." },
            { "what do you like to do", "I enjoy helping people by answering their questions and having conversations with them." }
        };

        // List of jokes
        List<string> jokes = new List<string>
        {
            "Why don't scientists trust atoms? Because they make up everything!",
            "Why did the scarecrow win an award? Because he was outstanding in his field!",
            "Why don't skeletons fight each other? They don't have the guts.",
            "What do you call fake spaghetti? An impasta!",
            "Why did the bicycle fall over? Because it was two-tired!",
            "What do you call cheese that isn't yours? Nacho cheese!",
            "Why was the math book sad? Because it had too many problems.",
            "Why did the golfer bring two pairs of pants? In case he got a hole in one!",
            "Why did the tomato turn red? Because it saw the salad dressing!",
            "Why don't programmers like nature? It has too many bugs."
        };

        // List of fun facts
        List<string> funFacts = new List<string>
        {
            "Did you know that honey never spoils? Archaeologists have found pots of honey in ancient Egyptian tombs that are over 3,000 years old and still perfectly edible!",
            "The shortest war in history lasted only 38 minutes. It was between Britain and Zanzibar on August 27, 1896.",
            "A single cloud can weigh more than a million pounds.",
            "Octopuses have three hearts.",
            "Bananas are berries, but strawberries aren't.",
            "A day on Venus is longer than a year on Venus.",
            "Humans share 50% of their DNA with bananas.",
            "There are more stars in the universe than grains of sand on all the world's beaches.",
            "Wombat poop is cube-shaped.",
            "A group of flamingos is called a 'flamboyance'."
        };

        // Response for unknown inputs
        string unknownResponse = "I'm not sure how to respond to that.";

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine().Trim();

            if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                PrintInGreen("Goodbye! Have a great day!");
                break;
            }

            // Find the best match among all possible responses
            string bestMatch = GetBestMatch(userInput, responses.Keys);
            double bestMatchScore = CalculateSimilarity(userInput, bestMatch);

            // Find the best match for joke requests
            string jokeMatch = GetBestMatch(userInput, new List<string> { "tell me a joke" });
            double jokeMatchScore = CalculateSimilarity(userInput, jokeMatch);

            // Find the best match for fun fact requests
            string factMatch = GetBestMatch(userInput, new List<string> { "tell me a fun fact" });
            double factMatchScore = CalculateSimilarity(userInput, factMatch);

            double minimumAcceptableScore = 0.5;

            if (jokeMatchScore > bestMatchScore && jokeMatchScore > factMatchScore && jokeMatchScore > minimumAcceptableScore)
            {
                if (jokeMatchScore == 1.0)
                {
                    PrintInGreen(GetNextJoke(jokes));
                    AskForAnother("joke", jokes);
                }
                else
                {
                    PrintInGreen($"Did you mean: \"{jokeMatch}\"? (yes/no): ");
                    string confirmation = Console.ReadLine().Trim().ToLower();
                    if (confirmation == "yes")
                    {
                        PrintInGreen(GetNextJoke(jokes));
                        AskForAnother("joke", jokes);
                    }
                    else
                    {
                        PrintInGreen(unknownResponse);
                    }
                }
            }
            else if (factMatchScore > bestMatchScore && factMatchScore > jokeMatchScore && factMatchScore > minimumAcceptableScore)
            {
                if (factMatchScore == 1.0)
                {
                    PrintInGreen(GetNextFunFact(funFacts));
                    AskForAnother("fun fact", funFacts);
                }
                else
                {
                    PrintInGreen($"Did you mean: \"{factMatch}\"? (yes/no): ");
                    string confirmation = Console.ReadLine().Trim().ToLower();
                    if (confirmation == "yes")
                    {
                        PrintInGreen(GetNextFunFact(funFacts));
                        AskForAnother("fun fact", funFacts);
                    }
                    else
                    {
                        PrintInGreen(unknownResponse);
                    }
                }
            }
            else if (bestMatchScore > minimumAcceptableScore)
            {
                if (bestMatchScore == 1.0)
                {
                    PrintInGreen(responses[bestMatch]);
                }
                else
                {
                    PrintInGreen($"Did you mean: \"{bestMatch}\"? (yes/no): ");
                    string confirmation = Console.ReadLine().Trim().ToLower();
                    if (confirmation == "yes")
                    {
                        PrintInGreen(responses[bestMatch]);
                    }
                    else
                    {
                        PrintInGreen(unknownResponse);
                    }
                }
            }
            else
            {
                PrintInGreen(unknownResponse);
            }
        }
    }

    // Method to ask if the user wants another joke or fun fact
    static void AskForAnother(string type, List<string> list)
    {
        while (true)
        {
            PrintInGreen($"Would you like to hear another {type}? (yes/no): ");
            string response = Console.ReadLine().Trim().ToLower();
            if (response == "yes")
            {
                if (type == "joke")
                {
                    PrintInGreen(GetNextJoke(list));
                }
                else if (type == "fun fact")
                {
                    PrintInGreen(GetNextFunFact(list));
                }
            }
            else
            {
                break;
            }
        }
    }

    // Method to cycle through jokes
    static string GetNextJoke(List<string> jokes)
    {
        if (jokes.Count == 0) return "I don't have any jokes right now.";

        string joke = jokes[jokeIndex];
        jokeIndex = (jokeIndex + 1) % jokes.Count;
        return joke;
    }

    // Method to cycle through fun facts
    static string GetNextFunFact(List<string> funFacts)
    {
        if (funFacts.Count == 0) return "I don't have any fun facts right now.";

        string funFact = funFacts[factIndex];
        factIndex = (factIndex + 1) % funFacts.Count;
        return funFact;
    }

    // Method to find the closest match to the input from a list of options
    static string GetBestMatch(string input, IEnumerable<string> options)
    {
        double bestMatchScore = 0;
        string bestMatch = "";

        foreach (string option in options)
        {
            double score = CalculateSimilarity(input, option);
            if (score > bestMatchScore)
            {
                bestMatchScore = score;
                bestMatch = option;
            }
        }

        return bestMatch;
    }

    // Method to calculate similarity between two strings using Levenshtein Distance
    static double CalculateSimilarity(string str1, string str2)
    {
        int n = str1.Length;
        int m = str2.Length;

        int[,] dp = new int[n + 1, m + 1];

        for (int i = 0; i <= n; i++)
            dp[i, 0] = i;
        for (int j = 0; j <= m; j++)
            dp[0, j] = j;

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                if (str1[i - 1] == str2[j - 1])
                    dp[i, j] = dp[i - 1, j - 1];
                else
                    dp[i, j] = 1 + Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]), dp[i - 1, j - 1]);
            }
        }

        return 1.0 - (double)dp[n, m] / Math.Max(n, m);
    }

    // Method to print text in green, one character at a time
    static void PrintInGreen(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(50); // Adjust the delay to control typing speed
        }
        Console.WriteLine();
        Console.ResetColor();
    }
}
