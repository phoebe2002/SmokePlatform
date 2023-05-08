using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace SmokePlatform
{
    internal class Program
    {

        public static List<T> GetObjectsOfClass<T>() where T : class
        {
            List<T> objectsOfClass = new List<T>();
            foreach (object obj in objectsOfClass)
            {
                if (obj.GetType() == typeof(T))
                {
                    objectsOfClass.Add(obj as T);
                }
            }
            return objectsOfClass;
        }



        //method starting from the requirement arguments
        public static void Find_method<T>(List<T> filteredObjects, string[] arguments, Type classType, string className) where T : class
        {
            List<T> new_filtered = new List<T> { };
          //  Console.WriteLine($"there are {arguments.Length} arguments");
            if (arguments.Length < 2)
            {
                Console.WriteLine("Invalid number of arguments.");
                return;
            }

            string requirement = arguments[1];

          //  Console.WriteLine($"this requirement is {requirement}");
            string[] requirementParts = requirement.Split("=", StringSplitOptions.RemoveEmptyEntries);

            if (requirementParts.Length != 2)
            {
                Console.WriteLine($"Invalid requirement: {requirement}");
                return;
            }

          //  Console.WriteLine($"Testing seperation:  fieldname = {requirementParts[0]} and it is : {requirementParts[1]}");

            string fieldName = requirementParts[0];
            //string[] fieldParts = fieldName.Split(".", StringSplitOptions.RemoveEmptyEntries);

            //if (fieldParts.Length != 1)
            //{
            //    Console.WriteLine($"Invalid field name: {fieldName}");
            //    return;
            //}

          //  fieldName = fieldParts[0];
            PropertyInfo property = classType.GetProperty(fieldName);
           // Console.WriteLine($" The property type is {property.PropertyType}");

            if (property == null)
            {
                Console.WriteLine($"Field {fieldName} not found in class {className}.");
                return;
            }
           

            string fieldValueString = requirementParts[1];
            object fieldValue;

            try
            {
                fieldValue = Convert.ChangeType(fieldValueString, property.PropertyType);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invalid value for field {fieldName}: {fieldValueString}. {e.Message}");
                return;
            }

            new_filtered = filteredObjects.Where(obj =>
             {
                 object value = property.GetValue(obj);


                 if (value == null)
                 {
                     Console.WriteLine("Value is null, can't compare");
                     return false;
                 }

              //   Console.WriteLine($"Value is not null -- now comparing {value} with {fieldValue}");

                 if (value is string strValue)
                 {
                     if (strValue.CompareTo(fieldValue) == 0)
                     {
                         new_filtered.Add(obj);
                     }
                     return strValue.CompareTo(fieldValue) == 0; //return what??
                 }
                 else if (value is IComparable comparableValue)
                 {
                     switch (requirementParts[0][requirementParts[0].Length - 1])
                     {
                         case '<':
                             if (comparableValue.CompareTo(fieldValue) < 0) { new_filtered.Add(obj); }
                             return comparableValue.CompareTo(fieldValue) < 0;
                         case '>':
                             if (comparableValue.CompareTo(fieldValue) > 0) { new_filtered.Add(obj); }
                             return comparableValue.CompareTo(fieldValue) > 0;
                         default:
                             if (comparableValue.CompareTo(fieldValue) == 0) { new_filtered.Add(obj); }
                             return comparableValue.CompareTo(fieldValue) == 0;
                     }
                 }
                 else
                 {
                     return false;
                 }
             }).ToList();

            //foreach (var obj in new_filtered)
            //{
            //    // add filtered object to new_filtered
            //    new_filtered.Add(obj);
            //}

            if (new_filtered.Count == 0)
            {
                Console.WriteLine($"No objects of type {className} found that match the requirements.");
                return;
            }

            foreach (T obj in new_filtered)
            {
                Console.WriteLine(obj.ToString());
            }
        }


        public static void Add_method<T>(List<T> list, Type classType, string className) where T : class
        {
            className = className.ToLower();
            
            if (className == "game" || className == "game2_adapter")
            {
                Game new_game = new Game();
                PropertyInfo[] properties = classType.GetProperties();
                List<string> availableFields = new List<string>();
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    {
                        availableFields.Add(property.Name);
                    }
                }
                Console.WriteLine($"[Available fields: '{string.Join(", ", availableFields)}']");

                bool done = false;
                while (!done)
                {
                    string input = Console.ReadLine();
                    string[] inputParts = input.Split('=');
                    string field_name = inputParts[0];
                    string field_value = inputParts[1];

                    PropertyInfo property = classType.GetProperty(field_name);
                    if (property == null)
                    {
                        Console.WriteLine($"Invalid field name: {field_name}");
                        continue;
                    }

                    object fieldValue;
                    try
                    {
                        fieldValue = Convert.ChangeType(field_value, property.PropertyType);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Invalid value for field {field_name}: {field_value}. {e.Message}");
                        continue;
                    }

                    new_game.SetField(field_name, fieldValue);

                    Console.WriteLine("Enter another field or type 'DONE' or 'EXIT'");
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "done":
                            done = true;
                            break;
                        case "exit":
                            return;
                        default:
                            break;
                    }
                }
                list.Add(new_game as T);
                Console.WriteLine($"{className} created");
            }
            else if (className == "user" || className == "user2_adapter")
            {
                User new_user = new User();
                PropertyInfo[] properties = classType.GetProperties();
                List<string> availableFields = new List<string>();
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    {
                        availableFields.Add(property.Name);
                    }
                }
                Console.WriteLine($"[Available fields: '{string.Join(", ", availableFields)}']");

                bool done = false;
                while (!done)
                {
                    string input = Console.ReadLine();
                    string[] inputParts = input.Split('=');
                    string field_name = inputParts[0];
                    string field_value = inputParts[1];

                    PropertyInfo property = classType.GetProperty(field_name);
                    if (property == null)
                    {
                        Console.WriteLine($"Invalid field name: {field_name}");
                        continue;
                    }

                    object fieldValue;
                    try
                    {
                        fieldValue = Convert.ChangeType(field_value, property.PropertyType);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Invalid value for field {field_name}: {field_value}. {e.Message}");
                        continue;
                    }

                    new_user.SetField(field_name, fieldValue);

                    Console.WriteLine("Enter another field or type 'DONE' or 'EXIT'");
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "done":
                            done = true;
                            break;
                        case "exit":
                            return;
                        default:
                            break;
                    }
                }
                list.Add(new_user as T);
                Console.WriteLine($"{className} created");
            }
            else if (className == "mod" || className == "mod2_adapter")
            {
                Mod new_mod = new Mod();
                PropertyInfo[] properties = classType.GetProperties();
                List<string> availableFields = new List<string>();
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    {
                        availableFields.Add(property.Name);
                    }
                }
                Console.WriteLine($"[Available fields: '{string.Join(", ", availableFields)}']");

                bool done = false;
                while (!done)
                {
                    string input = Console.ReadLine();
                    string[] inputParts = input.Split('=');
                    string field_name = inputParts[0];
                    string field_value = inputParts[1];

                    PropertyInfo property = classType.GetProperty(field_name);
                    if (property == null)
                    {
                        Console.WriteLine($"Invalid field name: {field_name}");
                        continue;
                    }

                    object fieldValue;
                    try
                    {
                        fieldValue = Convert.ChangeType(field_value, property.PropertyType);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Invalid value for field {field_name}: {field_value}. {e.Message}");
                        continue;
                    }

                    new_mod.SetField(field_name, fieldValue);

                    Console.WriteLine("Enter another field or type 'DONE' or 'EXIT'");
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "done":
                            done = true;
                            break;
                        case "exit":
                            return;
                        default:
                            break;
                    }
                }
                list.Add(new_mod as T);
                Console.WriteLine($"{className} created");
            }
            else if (className == "review" || className == "review2_adapter")
            {
                Review new_review = new Review();
                PropertyInfo[] properties = classType.GetProperties();
                List<string> availableFields = new List<string>();
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                    {
                        availableFields.Add(property.Name);
                    }
                }
                Console.WriteLine($"[Available fields: '{string.Join(", ", availableFields)}']");

                bool done = false;
                while (!done)
                {
                    string input = Console.ReadLine();
                    string[] inputParts = input.Split('=');
                    string field_name = inputParts[0];
                    string field_value = inputParts[1];

                    PropertyInfo property = classType.GetProperty(field_name);
                    if (property == null)
                    {
                        Console.WriteLine($"Invalid field name: {field_name}");
                        continue;
                    }

                    object fieldValue;
                    try
                    {
                        fieldValue = Convert.ChangeType(field_value, property.PropertyType);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Invalid value for field {field_name}: {field_value}. {e.Message}");
                        continue;
                    }

                    new_review.SetField(field_name, fieldValue);

                    Console.WriteLine("Enter another field or type 'DONE' or 'EXIT'");
                    string command = Console.ReadLine().ToLower();
                    switch (command)
                    {
                        case "done":
                            done = true;
                            break;
                        case "exit":
                            return;
                        default:
                            break;
                    }
                }
                list.Add(new_review as T);
                Console.WriteLine($"{className} created");
            }
        }
           
        public static bool stringComparator(Game a, Game b)
        {
            int result = String.Compare(a.name, b.name);
            if (result < 0)
            {
                return false;
            }
            else if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }

        }

        public static bool stringComparator(Review a, Review b)
        {
            int result = String.Compare(a.author.nickname, b.author.nickname);
            if (result < 0)
            {
                return false;
            }
            else if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }

        }
        static void Main(string[] args)
        {

            List<Game> tempGame = new List<Game>()
            {
                new Game("temp Game", "temp Game Genre", null,null,null, "temp Game device" )
            };

            List<User> userList = new List<User>()
            {
                new User("Szredor", tempGame),
            new User("Driver", tempGame),
            new User("Pek", tempGame),
           new User("Commander Shepard", tempGame),
            new User("MLG", tempGame),
            new User("Rondo", tempGame),
           new User("lemon", tempGame ),
            new User("Bonet", tempGame )
        };

            List<Mod> tempmodList = new List<Mod>() {
             new Mod("temp mod", "temp mod", new List<User>() { userList[0] }, new List<Mod>() { })
            };


            List<Mod> modsList = new List<Mod>()
            {

            new Mod("Clouds", "Super clouds", new List<User>() { userList[3] }, tempmodList),
            new Mod("T-pose", "Cow are now T-posing",new List<User>() { userList[6] }, tempmodList),
            new Mod("Commander Shepard", "I’m Commander Shepard and this is my favorite mod on Smoke",new List<User>() { userList[4] }, tempmodList),
            new Mod("BTM", "You can now play in BTM’s trains and bytebuses",new List<User>() { userList[7] }, tempmodList),
            new Mod("Cosmic - black hole edition", "Adds REALISTIC black holes",new List<User>() { userList[2] },  tempmodList)

        };


            modsList[0].compatability.Add(modsList[1]);
            modsList[0].compatability.Add(modsList[2]);
            modsList[0].compatability.Add(modsList[3]);
            modsList[0].compatability.Add(modsList[4]);

            modsList[1].compatability.Add(modsList[0]);
            modsList[1].compatability.Add(modsList[2]);

            modsList[2].compatability.Add(modsList[0]);
            modsList[2].compatability.Add(modsList[1]);
            modsList[2].compatability.Add(modsList[3]);

            modsList[3].compatability.Add(modsList[0]);
            modsList[3].compatability.Add(modsList[2]);

            modsList[4].compatability.Add(modsList[0]);

            List<Review> reviewList = new List<Review>()
            {
                new Review("null", 0, userList[0]),
            new Review("I’m Commander Shepard and this is my favorite game on Smoke", 10, userList[4]),
           new Review("The Moo remake sets a new standard for the future of the survival horror series⁠, even if it isn't the sequel I've been pining for.", 12, userList[2]),
            new Review("Universe of Technology is a spectacular 4X game, that manages to shine even when the main campaign doesn't.", 15, userList[3]),
            new Review("Moo’s interesting art design can't save it from its glitches, bugs, and myriad terrible game design decisions, but I love its sound design", 2, userList[7]),
            new Review("I've played this game for years nonstop. Over 8k hours logged (not even joking). And I'm gonna tell you: at this point, the game's just not worth playing anymore. I think it hasn't been worth playing for a year or two now, but I'm only just starting to realize it. It breaks my heart to say that, but that's just the truth of the matter.", 5, userList[5])

        };

            List<Game> gamesList = new List<Game>()
            {
                new Game("temp game", "temp genre", new List<User>() { userList[0] }, new List<Review>() { reviewList[0] },new List<Mod>() { modsList[0] }, "fake device"),
                new Game("Garbage Collector", "simulation", new List<User>() { userList[1] }, new List<Review>() { reviewList[0] }, new List<Mod>() { modsList[0] }, "PC"),
                new Game("Universe of Technology", "4X", new List<User>() { userList[1] }, new List<Review>() { reviewList[3] }, new List<Mod>() { modsList[0], modsList[2] }, "bitnix"),
                new Game("Moo", "rogue-like", new List<User>() { userList[3] }, new List<Review>() { reviewList[2], reviewList[4] }, new List<Mod>() { modsList[0], modsList[1], modsList[2] }, "bitstation"),
                new Game("Tickets Please", "platformer", new List<User>() { userList[2] }, new List<Review>() { reviewList[1] }, new List<Mod>() { modsList[0], modsList[2], modsList[3] }, "bitbox"),
                new Game("Cosmic", "MOBA", new List<User>() { userList[5] }, new List<Review>() { reviewList[5] }, new List<Mod>() { modsList[0], modsList[4] }, "cross platform")

            };

            userList[0].ownedGames.Add(gamesList[0]);
            userList[0].ownedGames.Add(gamesList[1]);
            userList[0].ownedGames.Add(gamesList[2]);
            userList[0].ownedGames.Add(gamesList[3]);
            userList[0].ownedGames.Add(gamesList[4]);
            //MUST BE FIXED
            //userList[1].ownedGames.Add(gamesList[0]);
            //userList[1].ownedGames.Add(gamesList[1]);
            //userList[1].ownedGames.Add(gamesList[2]);
            //userList[1].ownedGames.Add(gamesList[3]);
            //userList[1].ownedGames.Add(gamesList[4]);
            //userList[2].ownedGames.Add(gamesList[0]);
            //userList[2].ownedGames.Add(gamesList[1]);
            //userList[2].ownedGames.Add(gamesList[2]);
            //userList[2].ownedGames.Add(gamesList[3]);
            //userList[2].ownedGames.Add(gamesList[4]);
            //userList[3].ownedGames.Add(gamesList[1]);
            //userList[3].ownedGames.Add(gamesList[2]);
            //userList[3].ownedGames.Add(gamesList[4]);
            //userList[4].ownedGames.Add(gamesList[0]);
            //userList[4].ownedGames.Add(gamesList[4]);
            //userList[5].ownedGames.Add(gamesList[0]);
            //userList[6].ownedGames.Add(gamesList[2]);
            //userList[6].ownedGames.Add(gamesList[3]);
            //userList[7].ownedGames.Add(gamesList[1]);




            Console.WriteLine("GAMES - BASE REPRESENTATION");
            foreach (var game in gamesList)
            {
                game.Print_Game();
            }

            //Console.WriteLine("USERS - BASE REPRESENTATION");
            //foreach (var user in userList)
            //{
            //    Console.WriteLine("User name:" + user.nickname);
            //    Console.WriteLine("Games:");
            //    foreach (Game game in user.ownedGames)
            //    {
            //        Console.WriteLine(" - " + game.name);
            //    }
            //}

            //Console.WriteLine("\nREVIEWS - BASE REPRESENTATION");
            //foreach (var rev in reviewList)
            //{
            //    rev.Print_Review();
            //}

            //Console.WriteLine("mods - base representation");
            //foreach (var mod in modsList)
            //{
            //    mod.Print_Mod();

            //}

            Console.WriteLine("-------------------SECOND REPRESENTATION ----------------------");

            
            List<Game2_Adaptor> adapted_GL = new List<Game2_Adaptor> {
                new Game2_Adaptor(gamesList[0]),
            new Game2_Adaptor(gamesList[1]),
            new Game2_Adaptor(gamesList[2]),
            new Game2_Adaptor(gamesList[3]),
             new Game2_Adaptor(gamesList[4]),
            new Game2_Adaptor(gamesList[5])

        };

            List<User2_Adaptor> adapted_UL = new List<User2_Adaptor>
            {
                new User2_Adaptor(userList[0]),
                new User2_Adaptor(userList[1]),
                new User2_Adaptor(userList[2]),
                new User2_Adaptor(userList[3]),
                new User2_Adaptor(userList[4]),
                new User2_Adaptor(userList[5]),
                new User2_Adaptor(userList[6]),
                new User2_Adaptor(userList[7])
            };

            List<Review2_adaptor> adapted_RL = new List<Review2_adaptor>
            {
                new Review2_adaptor(reviewList[0]),
                new Review2_adaptor(reviewList[1]),
                new Review2_adaptor(reviewList[2]),
                new Review2_adaptor(reviewList[3]),
                new Review2_adaptor(reviewList[4]),
                new Review2_adaptor(reviewList[5])

            };

            List<Mod2_Adaptor> adapted_ML = new List<Mod2_Adaptor>
            {
                new Mod2_Adaptor(modsList[0]),
                new Mod2_Adaptor(modsList[1]),
                new Mod2_Adaptor(modsList[2]),
                new Mod2_Adaptor(modsList[3]),
                new Mod2_Adaptor(modsList[4])

                };


            //Console.WriteLine("Game Second Representation");
            //foreach (var game in adapted_GL)
            //{
            //    game.Print_Game();
            //}

            //Console.WriteLine("User Second Representation");
            //foreach(var user in adapted_UL)
            //{
            //    user.Print_User();
            //}

            //Console.WriteLine("Review Second Representation");
            //foreach (var review in adapted_RL)
            //{
            //    review.Print_Review();
            //}

            //Console.WriteLine("Mod Second Representation");
            //foreach(var mod in adapted_ML)
            //{
            //    mod.Print_Mod();
            //}

            //Console.WriteLine("------------------THIRD REPRESENTATION-------------------");


            //SortedArray<Game> gameQueue = new SortedArray<Game>(stringComparator, gamesList);
            //SortedArray<Review> reviewQueue = new SortedArray<Review>(stringComparator, reviewList);
            //Console.WriteLine( "--------------------FIFTH REPRESENTATION-------------------");
            //Console.WriteLine("---------------------------------------------------\n");
            //Console.WriteLine("Printing game list in sorted order according to title\n");
            //Console.WriteLine("---------------------------------------------------\n");
            //foreach (var el in  gameQueue.Values)
            //{
            //    el.Print_Game();
            //}

            //Console.WriteLine("------------------------------\n");
            //Console.WriteLine("Sorted Array after 1st removal\n");
            //Console.WriteLine("------------------------------\n");
            //gameQueue.Remove(gameQueue.Values[4]);
            //foreach (var el in gameQueue.Values)
            //{
            //    el.Print_Game();
            //}

            //Console.WriteLine("------------------------------\n");
            //Console.WriteLine("Sorted Array after 2nd removal\n");
            //Console.WriteLine("------------------------------\n");
            //gameQueue.Remove(gameQueue.Values[4]);
            //foreach (var el in gameQueue.Values)
            //{
            //    el.Print_Game();
            //}

            //Console.WriteLine("----------------------------------------\n");
            //Console.WriteLine("Sorted Array after inserting 1st element\n");
            //Console.WriteLine("----------------------------------------\n");
            //gameQueue.Insert(gamesList[4]);
            //foreach (var el in gameQueue.Values)
            //{
            //    el.Print_Game();
            //}

            //var algo = new Algorithm<Game>();
            //Console.WriteLine("----------------------------------------------------------------------------\n");
            //Console.WriteLine("Using the Find Algorithm to get the first occurance of \"Please\" in game title\n");
            //Console.WriteLine("----------------------------------------------------------------------------\n");
            //Game found = algo.Find(gameQueue.Iterator, s => s != null && s.name.Contains("Please"));
            //found.Print_Game();

            //var rev_algo = new Algorithm<Review>();
            //Console.WriteLine("-----------------------------------------------------------------------------\n");
            //Console.WriteLine("Using the CountIf Algorithm to the rating is greater than 10\n");
            //Console.WriteLine("-----------------------------------------------------------------------------\n");
            //int count = rev_algo.CountIf(reviewQueue.Iterator, x => x != null && x.rating > 10);
            //Console.WriteLine(count);



            Console.WriteLine("---------------------------------------6th PART ----------------------------------------------");

            bool a = true;
            while (a)
            {
                // Read input from the user
                string input = Console.ReadLine();
                string[] inputParts = input.Split(' ');
                string command = inputParts[0];
                string[] arguments = inputParts.Skip(1).ToArray();

                switch (command)
                {
                    case "list":
                        string className_ = arguments[0];

                        switch (className_)
                        {
                            case "game":
                                foreach(var game in gamesList)
                                {
                                    game.ToString();
                                }
                                break;
                            case "user":
                                foreach(var user in userList)
                                {
                                    user.ToString();
                                }
                                break;
                            case "mod":
                                foreach(var mod in modsList)
                                {
                                    mod.ToString();
                                }
                                break;
                            case "review":
                                foreach(var rev in reviewList)
                                {
                                    rev.ToString();
                                }
                                break;
                            default:
                                Console.WriteLine($"Unknown class name: {className_}");
                                break;
                        }
                        break;
                    case "find":

                        string className = arguments[0];
                        switch (className)
                        {
                            case "game":
                                List<Game> gameObjects = gamesList;
                                Find_method(gameObjects, arguments, typeof(Game), "Game");
                                break;
                            case "user":
                                List<User> users = userList;
                                Find_method(users, arguments, typeof(User), "User");
                                break;
                            case "mod":
                                List<Mod> mods = modsList;
                                Find_method(mods, arguments, typeof(Mod), "Mod");
                                break;
                            case "review":
                                List<Review> reviews = reviewList;
                                Find_method(reviews, arguments, typeof(Review), "Review");
                                break;
                            default:
                                Console.WriteLine($"Unknown class name: {className}");
                                break;
                        }
                        break;
                    case "exit":
                        a = false;
                        break;
                    case "add":
                        string classname = arguments[0];
                        string class_in_use = arguments[1];
                        switch(classname)
                        {
                            case "game":
                                if(class_in_use == "base")
                                {
                                    List<Game> gameObjects = gamesList;
                                    Add_method(gameObjects, typeof(Game), classname);
                                }
                                if(class_in_use == "secondary")
                                {
                                    List<Game2_Adaptor> gameObjects_II = adapted_GL;
                                    Add_method(gameObjects_II, typeof(Game2_Adaptor), classname);
                                }
                                else
                                {
                                    Console.WriteLine("Class does not exist");
                                }
                                break;
                            case "user":

                                if (class_in_use == "base")
                                {
                                List<User> users = userList;
                                    Add_method(users, typeof(User), classname);
                                }
                                if (class_in_use == "secondary")
                                {
                                    List<User2_Adaptor> userObjects_II = adapted_UL;
                                    Add_method(userObjects_II, typeof(User2_Adaptor), classname);
                                }
                                else
                                {
                                    Console.WriteLine("Class does not exist");
                                }
                             
                                break;
                            case "mod":
                                if (class_in_use == "base")
                                {
                                List<Mod> mods = modsList;
                                    Add_method(mods, typeof(Mod), classname);
                                }
                                if (class_in_use == "secondary")
                                {
                                    List<Mod2_Adaptor> modObjects_II = adapted_ML;
                                    Add_method(modObjects_II, typeof(Mod2_Adaptor), classname);
                                }
                                else
                                {
                                    Console.WriteLine("Class does not exist");
                                }
                                break;
                            case "review":
                                
                                if (class_in_use == "base")
                                {
                                    List<Review> reviews = reviewList;
                                    Add_method(reviews, typeof(Review), classname);
                                }
                                if (class_in_use == "secondary")
                                {
                                    List<Review2_adaptor> revObjects_II = adapted_RL;
                                    Add_method(revObjects_II, typeof(Review2_adaptor), classname);
                                }
                                else
                                {
                                    Console.WriteLine("Class does not exist");
                                }
                                break;
                            default:
                                Console.WriteLine($"Unknown class name: {classname}");
                                break;
                        }
                        break;



                }
            }

        }


    }
}

