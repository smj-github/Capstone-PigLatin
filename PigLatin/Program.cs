using System;

namespace PigLatin
{
    class Program
    {
        static string[] strCaseInfo;
        static void Main(string[] args)
        {            
            char bRepeat = 'y';
            
            Console.WriteLine("Welcome to the Pig Latin Translator!");

            while (bRepeat == 'y')
            {
                // Get input from the user
                Console.WriteLine("\nEnter a word/line to be translated:");
                string wordOrSentence = Console.ReadLine();

                // Check if the user has entered a word before translation
                if (wordOrSentence.Trim() == string.Empty)
                {                    
                    Console.WriteLine("Sorry! can't translate. No word/line given!");
                }
                else
                {
                    // Get all the words in the sentence
                    string[] words = wordOrSentence.Split(" ");

                    string strTranslate = "";

                    for (int i= 0; i < words.Length; i++) 
                    {
                        // Get each word in the sentence
                        strCaseInfo = new string[words[i].Length];

                        // Remember the case of each letter in the word for future display
                        strCaseInfo = RememberCaseInfo(words[i]);

                        // Convert to PigLatin 
                        if (i == 0)
                        {
                            strTranslate = ConvertToPigLatin(words[i]);
                        }
                        else
                        {
                            strTranslate = strTranslate + " " + ConvertToPigLatin(words[i]);
                        }
                    }

                    // Display the translation
                    Console.WriteLine("\nYour PigLatin word/sentence is: \n" + strTranslate);
                }                

                // Check if the user wants to continue
                Console.WriteLine("\nTranslate another line? y/n");
                bRepeat = Console.ReadLine().ToLower()[0];
            }
        }

        public static string[] RememberCaseInfo(string word)
        {
            string[] caseInfo = new string[word.Length];
            // Loop through each character and find out if it is Uppercase/Lowercase
            // Store this info in a separate array
            for(int i=0; i<word.Length;i++)
            {
                if (char.IsUpper(word[i]))
                {
                    caseInfo[i] = "U";
                }
                else
                {
                    caseInfo[i] = "L";
                }
            }
            return caseInfo;
        }
        public static string ConvertToPigLatin(string word)
        {
            word = word.ToLower();
            string strPigLatin;

            // Check if the word contains a number
            if (!CheckIfContainsNumber(word))
            {
                // Check if the word has a special character
                if (!CheckIfContainsSpecialChar(word))
                {
                    //Check if first letter is a vowel
                    if (CheckIfVowel(word[0]))
                        strPigLatin = MakeVowelPigLatin(word);
                    else
                        strPigLatin = MakeConsonantPigLatin(word);

                    // Keep the case of the word
                    strPigLatin = KeepCase(strPigLatin);
                }
                else
                {
                    strPigLatin = KeepCase(word);
                }
            }
            else
            {
                strPigLatin = KeepCase(word);
            }

            return strPigLatin;
        }

        public static bool CheckIfContainsNumber(string word)
        {
            string numbers = "0123456789";
            // Check if the word contains any number
            foreach (var item in numbers)
            {
                if (word.Contains(item))
                    return true;
            }
            return false;
        }
        public static bool CheckIfContainsSpecialChar(string word)
        {
            string splChar = "~^*[]\\:|!#$%&/()=?><@£§€{}.-;'\"<>_,";
            // Check if the word contains any special character
            foreach (var item in splChar)
            {
                if (word.Contains(item)) 
                    return true;
            }
            return false;
        }
        public static string KeepCase(string word)
        {
            string newWord="";
            // Store the initial length of the word
            int initialWordLength = strCaseInfo.Length;
            // Get the length of the extra characters added beyond the original length
            string secondPart = word.Substring(initialWordLength, word.Length - initialWordLength);

            for (int i=0; i<= (initialWordLength - 1); i++)
            {
                // Restore all upper case characters
                if (strCaseInfo[i] == "U")
                {
                    // newWord = word.Replace(word[i], word[i].ToString().ToUpper()[0]);
                    newWord += word[i].ToString().ToUpper();
                }
                else
                {
                    newWord += word[i].ToString();
                }
            }
            // return the full word
            return newWord + secondPart;
        }

        public static bool CheckIfVowel(char firstLetter)
        {
            // Build a string containing all the vowels
            string vowelString = "aeiou";
            // Check if the first letter exists in this vowel word
            if (vowelString.Contains(firstLetter))
                return true;
            else
                return false;
        }

        public static string MakeVowelPigLatin(string word)
        {
            // Add 'way' to the end of the word
            return (word + "way");
        }

        public static string MakeConsonantPigLatin(string word)
        {
            string consonantSubstring = "";
            string newWord = "";
            foreach (char letter in word)
            {

                if (!CheckIfVowel(letter))
                {
                    consonantSubstring += letter;
                }
                else
                {
                    newWord = word.Substring(word.IndexOf(letter), (word.Length - word.IndexOf(letter)));
                    newWord = newWord + consonantSubstring + "ay";
                    break;
                }
            }
            return newWord;
        }
    }
}
