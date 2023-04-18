using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP01_Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, Dictionary<string, string>> addressBook
                = new Dictionary<int, Dictionary<string, string>>();
            Dictionary<string, string> entry = new Dictionary<string, string>();

            // FixedCategories are the fields in an address book that has to be added.
            List<string> fixedEntryCategories = new List<string> { "Name", "Address", "Contact Number"};
            
            // cont is what keeps the application looping
            bool cont = true;
            // option is the user input based on the question with fixed answers
            string option = "";

            // user input involving free text
            string uInput = "";
            // lastKey contains the last key in the dictionary + 1
            int lastKey = 0;
            // allows the user to add custom fields to the dictionary per entry
            bool customCats = true;

            // keys is the variable that will contain all the keys in the dictionary
            int[] keys = new int[] { };
            // the index of the entry that will be displayed
            int dispKey = 0;
            // when dispMode is true, the application will only display values from the dictionary
            bool dispMode = true;
            
            while(cont)
            {
                // start up
                Console.WriteLine("Welcome user!" +
                    "\nThere are currently {0} entries in the address book!", addressBook.Count());

                Console.Write("What would you like to do? " +
                    "\nKey in one of the following commands: " +
                    "\n\t\"ADD\" to add new entries " +
                    "\n\t\"VIEW\" to view all entries." +
                    "\n\t\"QUIT\" or \"Q\" to quit" +
                    "\n\nCommand:\t");
                option = Console.ReadLine().ToUpper();
                Console.WriteLine("\n\n");
                switch(option)
                {
                    case "ADD":
                        Console.Clear();

                        // get the last key
                        if (addressBook.Count > 0)
                            lastKey = addressBook.Keys.Last(); // only works IF there is more than 0 keys
                        // if I dont use the keys.Last() function
                        //foreach (int x in addressBook.Keys)
                        //    lastKey = x;
                        
                        lastKey++;
                        Console.WriteLine("Adding entry {0}. . .", lastKey);

                        // reset the dictionary
                        entry = new Dictionary<string, string>();

                        // get all the fixed categories
                        for(int x = 0; x < fixedEntryCategories.Count(); x++)
                        {
                            Console.Write("What is the {0} of this new entry? ", fixedEntryCategories[x]);
                            uInput = Console.ReadLine();

                            entry[fixedEntryCategories[x]] = uInput;
                        }

                        ///============================================================================
                        /// This custom feature allows the user of the application to add custom fields
                        /// to each individual entry in the address book
                        ///============================================================================
                        #region Custom Feature
                        Console.WriteLine("Would you like to add custom feilds? [YES/NO] ");
                        option = Console.ReadLine().ToUpper();
                        switch (option)
                        {
                            case "YES":
                                customCats = true;
                                break;
                            default:
                                customCats = false;
                                break;
                        }

                        while (customCats)
                        {
                            Console.WriteLine("What would be the custom feild? ");
                            option = Console.ReadLine();
                            Console.Write("What is the {0} of this new entry? ", option);
                            uInput = Console.ReadLine();

                            entry[option] = uInput;

                            Console.WriteLine("Would you like to add another custom feild? [YES/NO] ");
                            option = Console.ReadLine().ToUpper();
                            switch (option)
                            {
                                case "YES":
                                    customCats = true;
                                    break;
                                default:
                                    customCats = false;
                                    break;
                            }
                        } 
                        #endregion

                        addressBook[lastKey] = entry;

                        break;
                    case "VIEW":
                        if (addressBook.Count > 0)
                        {
                            dispMode = true;
                            dispKey = 0;

                            // gathering all the keys
                            keys = addressBook.Keys.ToArray();

                            while (dispMode)
                            {
                                Console.Clear();
                                Console.WriteLine("Viewing all entries...");

                                Console.WriteLine("Displaying entry {0}. . .", keys[dispKey]);
                                foreach(KeyValuePair<string, string> kvp in addressBook[keys[dispKey]])
                                {
                                    Console.WriteLine("{0}\t-\t{1}", kvp.Key, kvp.Value);
                                }

                                ///============================================================================
                                /// This custom feature allows the program to display 1 set of contact 
                                /// information at a time
                                ///============================================================================

                                #region CustomFeature
                                Console.Write("Key in \"L\" for left, \"R\" for right and \"S\" to stop viewing: ");
                                option = Console.ReadLine().ToUpper();
                                
                                switch (option)
                                {
                                    case "R":
                                        dispKey++;
                                        if (dispKey >= keys.Length)
                                            dispKey = 0;
                                        break;
                                    case "L":
                                        dispKey--;
                                        if (dispKey < 0)
                                            dispKey = keys.Length - 1;
                                        break;
                                    case "S":
                                        dispMode = false;
                                        break;
                                    default:
                                        Console.WriteLine("I'm sorry, I am not familiar with that command. Please try again...");
                                        break;
                                } 
                                #endregion

                                if(dispMode)
                                    Console.WriteLine("\n\nPress Any Key To Continue...");
                                    Console.ReadKey();
                            }
                        }
                        else
                            Console.WriteLine("But... There is nothing to display... ");                       

                        break;
                    case "QUIT":
                    case "Q":
                        Console.WriteLine("Alright, goodbye...");
                        cont = false;
                        break;
                    default:
                        Console.WriteLine("I'm sorry, I am not familiar with that command. Please try again...");
                        break;
                }

                Console.WriteLine("\n\nPress Any Key To Continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
