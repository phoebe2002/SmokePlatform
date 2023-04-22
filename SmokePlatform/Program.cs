using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SmokePlatform
{
    internal class Program
    {

      
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

            Console.WriteLine("------------------THIRD REPRESENTATION-------------------");


            SortedArray<Game> gameQueue = new SortedArray<Game>(stringComparator, gamesList);
            SortedArray<Review> reviewQueue = new SortedArray<Review>(stringComparator, reviewList);
            Console.WriteLine( "--------------------FIFTH REPRESENTATION-------------------");
            Console.WriteLine("---------------------------------------------------\n");
            Console.WriteLine("Printing game list in sorted order according to title\n");
            Console.WriteLine("---------------------------------------------------\n");
            foreach (var el in  gameQueue.Values)
            {
                el.Print_Game();
            }

            Console.WriteLine("------------------------------\n");
            Console.WriteLine("Sorted Array after 1st removal\n");
            Console.WriteLine("------------------------------\n");
            gameQueue.Remove(gameQueue.Values[4]);
            foreach (var el in gameQueue.Values)
            {
                el.Print_Game();
            }

            Console.WriteLine("------------------------------\n");
            Console.WriteLine("Sorted Array after 2nd removal\n");
            Console.WriteLine("------------------------------\n");
            gameQueue.Remove(gameQueue.Values[4]);
            foreach (var el in gameQueue.Values)
            {
                el.Print_Game();
            }

            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine("Sorted Array after inserting 1st element\n");
            Console.WriteLine("----------------------------------------\n");
            gameQueue.Insert(gamesList[4]);
            foreach (var el in gameQueue.Values)
            {
                el.Print_Game();
            }

            var algo = new Algorithm<Game>();
            Console.WriteLine("----------------------------------------------------------------------------\n");
            Console.WriteLine("Using the Find Algorithm to get the first occurance of \"Please\" in game title\n");
            Console.WriteLine("----------------------------------------------------------------------------\n");
            Game found = algo.Find(gameQueue.Iterator, s => s != null && s.name.Contains("Please"));
            found.Print_Game();

            var rev_algo = new Algorithm<Review>();
            Console.WriteLine("-----------------------------------------------------------------------------\n");
            Console.WriteLine("Using the CountIf Algorithm to the rating is greater than 10\n");
            Console.WriteLine("-----------------------------------------------------------------------------\n");
            int count = rev_algo.CountIf(reviewQueue.Iterator, x => x != null && x.rating > 10);
            Console.WriteLine(count);
        }

       
    }
}






//userList.Add(new User("null", new List<Game>() { gamesList[0] }));
//    userList.Add(new User("Szredor", new List<Game>() { gamesList[5], gamesList[1], gamesList[2], gamesList[3], gamesList[4] }));
//    userList.Add(new User("Driver", new List<Game>() { gamesList[5], gamesList[1], gamesList[2], gamesList[3], gamesList[4] }));
//    userList.Add(new User("Pek", new List<Game>() { gamesList[5], gamesList[1], gamesList[2], gamesList[3], gamesList[4] }));
//    userList.Add(new User("Commander Shepard", new List<Game>() { gamesList[1], gamesList[2], gamesList[4] }));
//    userList.Add(new User("MLG", new List<Game>() { gamesList[1], gamesList[5] }));
//    userList.Add(new User("Rondo", new List<Game>() { gamesList[1] }));//0
//    userList.Add(new User("lemon", new List<Game>() { gamesList[3], gamesList[4] }));//2,3
//    userList.Add(new User("Bonet", new List<Game>() { gamesList[2] }));//1

//reviewList.Add(new Review("null", 0, userList[0]));
//reviewList.Add(new Review("I’m Commander Shepard and this is my favorite game on Smoke", 10, userList[4]));
//reviewList.Add(new Review("The Moo remake sets a new standard for the future of the survival horror series⁠, even if it isn't the sequel I've been pining for.", 12, userList[2]));
//reviewList.Add(new Review("Universe of Technology is a spectacular 4X game, that manages to shine even when the main campaign doesn't.", 15, userList[3]));
//reviewList.Add(new Review("Moo’s interesting art design can't save it from its glitches, bugs, and myriad terrible game design decisions, but I love its sound design", 2, userList[8]));
//reviewList.Add(new Review("I've played this game for years nonstop. Over 8k hours logged (not even joking). And I'm gonna tell you: at this point, the game's just not worth playing anymore. I think it hasn't been worth playing for a year or two now, but I'm only just starting to realize it. It breaks my heart to say that, but that's just the truth of the matter.", 5, userList[5]));


//modsList.Add(new Mod("null", "null", new List<User>() { userList[0] }, new List<Mod> { modsList[0] }));
//modsList.Add(new Mod("Clouds", "Super clouds", new List<User>() { userList[3] }, new List<Mod>() { modsList[5], modsList[2], modsList[3], modsList[4] })); //1 2 3 4
//modsList.Add(new Mod("T-pose", "Cow are now T-posing", new List<User>() { userList[6] }, new List<Mod>() { modsList[1], modsList[3] }));//0 2
//modsList.Add(new Mod("Commander Shepard", "I’m Commander Shepard and this is my favorite mod on Smoke", new List<User>() { userList[4] }, new List<Mod>() { modsList[1], modsList[2], modsList[4] })); //0 1 3
//modsList.Add(new Mod("BTM", "You can now play in BTM’s trains and bytebuses", new List<User>() { userList[7] }, new List<Mod>() { modsList[1], modsList[3] }));//0 2
//modsList.Add(new Mod("Cosmic - black hole edition", "Adds REALISTIC black holes", new List<User>() { userList[2] }, new List<Mod>() { modsList[1] })); //0


//gamesList.Add(new Game("no game", "null", new List<User>() { userList[0] }, new List<Review>() { reviewList[0] }, new List<Mod>() { modsList[0] }, "null"));
//gamesList.Add(new Game("Garbage Collector", "simulation", new List<User>() { userList[0] }, new List<Review>() { reviewList[0] }, new List<Mod>() { modsList[1] }, "PC"));
//gamesList.Add(new Game("Universe of Technology", "4X", new List<User>() { userList[0] }, new List<Review>() { reviewList[3] }, new List<Mod>() { modsList[1], modsList[3] }, "bitnix"));
//gamesList.Add(new Game("Moo", "rogue-like", new List<User>() { userList[2] }, new List<Review>() { reviewList[2], reviewList[4] }, new List<Mod>() { modsList[1], modsList[2], modsList[3] }, "bitstation"));
//gamesList.Add(new Game("Tickets Please", "platformer", new List<User>() { userList[1] }, new List<Review>() { reviewList[1] }, new List<Mod>() { modsList[1], modsList[3], modsList[4] }, "bitbox"));
//gamesList.Add(new Game("Cosmic", "MOBA", new List<User>() { userList[4] }, new List<Review>() { reviewList[5] }, new List<Mod>() { modsList[1], modsList[5] }, "cross platform"));



//userList.Add(new User("null", new List<Game>() { gamesList[0] }));
//userList.Add(new User("Szredor", new List<Game>() { gamesList[5], gamesList[1], gamesList[2], gamesList[3], gamesList[4] }));
//userList.Add(new User("Driver", new List<Game>() { gamesList[5], gamesList[1], gamesList[2], gamesList[3], gamesList[4] }));
//userList.Add(new User("Pek", new List<Game>() { gamesList[5], gamesList[1], gamesList[2], gamesList[3], gamesList[4] }));
//userList.Add(new User("Commander Shepard", new List<Game>() { gamesList[1], gamesList[2], gamesList[4] }));
//userList.Add(new User("MLG", new List<Game>() { gamesList[1], gamesList[5] }));
//userList.Add(new User("Rondo", new List<Game>() { gamesList[1] }));//0
//userList.Add(new User("lemon", new List<Game>() { gamesList[3], gamesList[4] }));//2,3
//userList.Add(new User("Bonet", new List<Game>() { gamesList[2] }));//1

//modsList.Add(new Mod("null", "null", new List<User>() { userList[0] },new List<Mod> { modsList[0] }));  
//modsList.Add(new Mod("Clouds", "Super clouds", new List<User>() { userList[3] }, new List<Mod>() { modsList[5], modsList[2], modsList[3], modsList[4] })); //1 2 3 4
//modsList.Add(new Mod("T-pose", "Cow are now T-posing", new List<User>() { userList[6] }, new List<Mod>() { modsList[1], modsList[3] }));//0 2
//modsList.Add(new Mod("Commander Shepard", "I’m Commander Shepard and this is my favorite mod on Smoke", new List<User>() { userList[4] }, new List<Mod>() { modsList[1], modsList[2], modsList[4] })); //0 1 3
//modsList.Add(new Mod("BTM", "You can now play in BTM’s trains and bytebuses", new List<User>() { userList[7] }, new List<Mod>() { modsList[1], modsList[3] }));//0 2
//modsList.Add(new Mod("Cosmic - black hole edition", "Adds REALISTIC black holes", new List<User>() { userList[2] }, new List<Mod>() { modsList[1]})); //0
