using System;
using System.Linq;

namespace weasel
{
    class Program
    {
        public const string target = "METHINKS IT IS LIKE A WEASEL";
        public static readonly int best_score = target.Length;
        private static readonly Random rand = new Random();
        //
        public static int score = 0; // current score
        public static string sequence;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting Weasel Program"); // idea - https://en.wikipedia.org/wiki/Weasel_program
            sequence = randomChars(best_score); // creates the initial sequence
            int x = 0; // represents the current iteration
            while (score != best_score)
            {
                sequenceGenerator();
                Console.WriteLine($"{x}:{sequence}:{score}");
                x++;
            }

        }

        static string randomChars(int count)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ "; // the space *is* important
            return new string(Enumerable.Repeat(chars, count).Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        static void sequenceGenerator()
        {
            string new_seq = "";
            int new_score = -1;

            for (int i = 0; i < 101; i++)
            {

                string temp_seq = "";
                int temp_score = -1;
                char[] old_seq = sequence.ToCharArray();

                foreach (char l in sequence)
                {
                    bool mutated = oddsCalc(2); // 2% chance to mutate

                    if (mutated == true)
                    {
                        temp_seq = temp_seq + randomChars(1).ToString();
                    }
                    else
                    {
                        temp_seq = temp_seq + l.ToString();
                    }
                }

                temp_score = getScore(temp_seq);
                if (temp_score > new_score)
                {
                    new_score = temp_score;
                    new_seq = temp_seq;
                }
            }

            sequence = new_seq;
            score = new_score;
        }

        static bool oddsCalc(int i)
        {
            int randnum = rand.Next(1, 101);
            if (Enumerable.Range(1, i).Contains(randnum))
            {
                return true;
            }
            return false;
        }

        static int getScore(string seq)
        {
            char[] temp_seq = (seq.ToCharArray());
            char[] target_seq = (target.ToCharArray());
            int score = 0;

            try
            {
                for (int i = 0; i < target.Length; i++)
                {
                    if (temp_seq[0] == target_seq[0])
                    {
                        score++;
                    }
                    string temp_str = new string(temp_seq);
                    temp_str = temp_str.Remove(0, 1);
                    temp_seq = temp_str.ToCharArray();
                    temp_str = new string(target_seq);
                    temp_str = temp_str.Remove(0, 1);
                    target_seq = temp_str.ToCharArray();
                }
            }
            catch
            {
                return 0;
            }
            return score;
        }
    }
}