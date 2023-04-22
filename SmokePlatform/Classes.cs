using System;
using System.Data;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SmokePlatform
{
    public class Game
    {
        public string name { get; set; }
        public string genre { get; set; }
        public List<User> authors { get; set; }
        public List<Review> review { get; set; }
        public List<Mod> mods { get; set; }
        public string devices { get; set; }


        public Game(string _name = " ", string _genre = " ", List<User> _author = null, List<Review> _review = null, List<Mod> _mods = null, string _devices = " ")
        {
            name = _name;
            genre = _genre;
            authors = _author;
            review = _review;
            mods = _mods;
            devices = _devices;

        }
        public void Print_Game()
        {
            Console.WriteLine($"Name: {name}");
            //Console.WriteLine($"Genre: {genre}");
            //Console.WriteLine($"\nAuthors: ");
            //foreach (User author in authors)
            //{
            //    Console.WriteLine($"{author.nickname}");
            //}
            //Console.WriteLine("\nReviews");

            //foreach (Review rev in review)
            //{
            //    Console.WriteLine($"{rev.rating} by {rev.author.nickname}");
            //}

            //Console.WriteLine($"\nMods:");
            //foreach (Mod mod in mods)
            //{
            //    Console.WriteLine($"{mod.name}");
            //}
            //Console.WriteLine($"\nDevices: {devices} ");

            Console.WriteLine("\n");
        }
    }


    public class Review
    {
        public string text { get; set; }
        public int rating { get; set; }
        public User author { get; set; }

        public Review(string _text = " ", int _rating = 0, User _author = null)
        {
            text = _text; ;
            rating = _rating;
            author = _author;
        }


        public void Print_Review()
        {
            Console.WriteLine($"Author: {author.nickname}");
            Console.WriteLine($"Review: {text}");
            Console.WriteLine($"Rating: {rating}");
            Console.WriteLine("\n");
        }


    }

    public class Mod
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<User> authors { get; set; }
        public List<Mod> compatability { get; set; }
        public Mod(string _name /*= " "*/, string _description /*= " "*/, List<User> _authors /*= null*/, List<Mod> _compatability /*= null */)
        {
            this.name = _name;
            this.description = _description;
            this.authors = _authors;
            this.compatability = _compatability;

        }

        public void Print_Mod()
        {
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine($"\nAuthor/s:");
            foreach (User author in authors)
            {
                Console.WriteLine($"{author.nickname}");
            }
            Console.WriteLine("\nCompatability With:");
            foreach (Mod mod in compatability)
            {
                Console.WriteLine($"  {mod.name}");
            }
        }
    }

    public class User
    {
        public string nickname { get; set; }
        public List<Game> ownedGames { get; set; }

        public void Print_User()
        {
            Console.WriteLine($"\nNickname: {nickname}");
            Console.WriteLine("\nOwned Games:");
            foreach (Game game in ownedGames)
            {
                Console.WriteLine($"  {game.name} ");
            }
        }


        public User(string _nickname, List<Game> _ownedGames)
        {
            nickname = _nickname;
            ownedGames = _ownedGames;
        }

        public User()
        {
            this.nickname = null;
            this.ownedGames = new List<Game>();
        }
    }

    /* - ----------------------------------SECOND REPRESENTATION IDEAS - ----------------------------------*/

    public interface IGame
    {
        public void Print_Game();
    }

    public class Game2_Adaptor : IGame
    {
        private readonly Game game;
        private readonly List<Tuple<string, object>> tupleList;
        public Game2_Adaptor(Game game)
        {
            this.game = game;
            this.tupleList = new List<Tuple<string, object>>();

            tupleList.Add(Tuple.Create("name", (object)game.name));
            tupleList.Add(Tuple.Create("genre", (object)game.genre));
            tupleList.Add(Tuple.Create("authors", (object)game.authors));
            tupleList.Add(Tuple.Create("review", (object)game.review));
            tupleList.Add(Tuple.Create("mods", (object)game.mods));
            tupleList.Add(Tuple.Create("devices", (object)game.devices));
        }

        public void Print_Game()
        {
            game.Print_Game();
        }
    }


    public interface IReview
    {
        public void Print_Review();
    }

    public class Review2_adaptor : IReview
    {
        public readonly Review rev;
        private readonly List<Tuple<string, object>> tupleList;

        public Review2_adaptor(Review rev)
        {
            this.rev = rev;
            this.tupleList = new List<Tuple<string, object>>();
            tupleList.Add(Tuple.Create("text", (object)rev.text));
            tupleList.Add(Tuple.Create("rating", (object)rev.rating));
            tupleList.Add(Tuple.Create("author", (object)rev.author));

        }
        public void Print_Review()
        {
            rev.Print_Review();
        }
    }



    public interface IMod
    {
        public void Print_Mod();
    }

    public class Mod2_Adaptor : IMod
    {
        public readonly Mod mod;
        private readonly List<Tuple<string, object>> tupleList;

        public Mod2_Adaptor(Mod mod)
        {
            this.mod = mod;
            this.tupleList = new List<Tuple<string, object>>();
            tupleList.Add(Tuple.Create("name", (object)mod.name));
            tupleList.Add(Tuple.Create("descripion", (object)mod.description));
            tupleList.Add(Tuple.Create("authors", (object)mod.authors));
            tupleList.Add(Tuple.Create("compatability", (object)mod.compatability));


        }

        public void Print_Mod()
        {
            mod.Print_Mod();
        }

    }



    public interface IUser
    {
        public void Print_User();
    }

    public class User2_Adaptor : IUser
    {
        public readonly User user;
        private readonly List<Tuple<string, object>> tupleList;

        public User2_Adaptor(User user)
        {
            this.user = user;
            this.tupleList = new List<Tuple<string, object>>();
            tupleList.Add(Tuple.Create("nickname", (object)user.nickname));
            tupleList.Add(Tuple.Create("ownedGames", (object)user.ownedGames));

        }

        public void Print_User()
        {
            user.Print_User();
        }

    }

    /*-----------------------------------------THIRD REPRESENTATION------------------------------------------*/
    //work from here
    public class Game2
    {
        public int id { get; set; }
        public Dictionary<string, string> fields { get; }

        public Game2(int id, Dictionary<string, string> fields)
        {
            id = id;
            fields = fields;
        }

        //public void Print_Game()
        //{
        //    Console.WriteLine($"ID: {id}");
        //    Console.WriteLine($"Name: {fields["name"]}");
        //    Console.WriteLine($"Genre: {fields["genre"]}");
        //    Console.WriteLine($"\nAuthors: ");
        //    string[] authorIds = fields["authors"].Split(',');
        //    foreach (string authorId in authorIds)
        //    {
        //        User author = userList[int.Parse(authorId)];
        //        Console.WriteLine($"{author.nickname}");
        //    }
        //    Console.WriteLine("\nReviews");
        //    string[] reviewKeys = fields["reviews"].Split(',');
        //    foreach (string reviewKey in reviewKeys)
        //    {
        //        Review rev = reviewMap[reviewKey];
        //        Console.WriteLine($"{rev.rating} by {rev.author.nickname}");
        //    }
        //    Console.WriteLine($"\nMods:");
        //    string[] modKeys = fields["mods"].Split(',');
        //    foreach (string modKey in modKeys)
        //    {
        //        Mod mod = modMap[modKey];
        //        Console.WriteLine($"{mod.name}");
        //    }
        //    Console.WriteLine($"\nDevices: {fields["devices"]}");

        //    Console.WriteLine("\n");
        //}
   

}

    public class User2
    {
        public int id { get; set; }
        public Dictionary<string, string> Map { get;}

        public User2(int id, Dictionary<string, string> Map)
        {
            id = id;
            Map = Map;
        }
    }

    public class Review2
    {
        public int id { get; set; }
        public Dictionary<string, string> Map { get; }

        public Review2(int id, Dictionary<string, string> Map)
        {
            id = id;
            Map = Map;
        }
    }

    public class Mod2
    {
        public int id { get; set; }
        public Dictionary<string, string> Map { get; }

        public Mod2(int id, Dictionary<string, string> Map)
        {
            id = id;
            Map = Map;
        }
    }

    /*----------------------------------------------FOURTH TASK -------------------------------------------------------*/
    //Create two collections of the same interface:
    // - doubly linked list,
    // - vector(similar to c# list and c++ std::vector)
    //Each collection should allow:
    // - adding objects regardless of their representation,
    // - deleting objects regardless of their representation,
    // - getting forward and reverse iterator.

    // Define the interface for the collections
    //public interface ICollection<T>
    //{
    //    // void Add(T item);
    //    // bool Remove(T item);
    //    //// void Clear();//?
    //    //// int Count { get; }//?
    //    // IEnumerator<T> GetEnumerator();
    //    // IEnumerator<T> GetReverseEnumerator();

    //    IEnumerator<T> Iterator { get; set; }
    //    IEnumerator<T> BackIterator { get; set; }
    //    public void Insert(T element, int? index);
    //    public void Remove(T? element, int? index);
    //}

    //public class ListNode<T>
    //{
    //    public T Value { get; set; }
    //    public ListNode<T> Prev { get; set; }
    //    public ListNode<T> Next { get; set; }

    //    public ListNode(T value)
    //    {
    //        Value = value;
    //        Prev = null;
    //        Next = null;
    //    }
    //}

    //public class DoublyLinkedList<T> : ICollection<T>
    //{
    //    private ListNode<T> head;
    //    private ListNode<T> tail;
    //    private int count;

    //    public DoublyLinkedList()
    //    {
    //        head = null;
    //        tail = null;
    //        count = 0;
    //    }

    //    public void Add(T item)
    //    {
    //        ListNode<T> newNode = new ListNode<T>(item);

    //        if (head == null)
    //        {
    //            head = newNode;
    //            tail = newNode;
    //        }
    //        else
    //        {
    //            tail.Next = newNode;
    //            newNode.Prev = tail;
    //            tail = newNode;
    //        }

    //        count++;
    //    }

    //    public bool Remove(T item)
    //    {
    //        ListNode<T> current = head;

    //        while (current != null)
    //        {
    //            if (current.Value.Equals(item))
    //            {
    //                if (current.Prev == null)
    //                {
    //                    head = current.Next;
    //                }
    //                else
    //                {
    //                    current.Prev.Next = current.Next;
    //                }

    //                if (current.Next == null)
    //                {
    //                    tail = current.Prev;
    //                }
    //                else
    //                {
    //                    current.Next.Prev = current.Prev;
    //                }

    //                count--;
    //                return true;
    //            }

    //            current = current.Next;
    //        }

    //        return false;
    //    }

    //    public void Clear()
    //    {
    //        head = null;
    //        tail = null;
    //        count = 0;
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        ListNode<T> current = head;
    //        while (current != null)
    //        {
    //            yield return current.Value;
    //            current = current.Next;
    //        }
    //    }

    //    public IEnumerator<T> GetReverseEnumerator()
    //    {
    //        ListNode<T> current = tail;
    //        while (current != null)
    //        {
    //            yield return current.Value;
    //            current = current.Prev;
    //        }
    //    }
    //}




    //public interface ILinkedList<T> : ICollection<T>
    //{
    //    void AddFirst(T item);
    //    void AddLast(T item);
    //    void RemoveFirst();
    //    void RemoveLast();
    //    T First { get; }
    //    T Last { get; }
    //}

    //public interface IVector<T> : ICollection<T>
    //{
    //    T this[int index] { get; set; }
    //    void Insert(int index, T item);
    //    void RemoveAt(int index);
    //}



    /* -----------------------------------------  TASK FIVE ------------------------------------------------------------ */

    public interface ICollection<T>
    {
        // void Add(T item);
        // bool Remove(T item);
        //// void Clear();//?
        //// int Count { get; }//?
        // IEnumerator<T> GetEnumerator();
        // IEnumerator<T> GetReverseEnumerator();

        IEnumerator<T> Iterator { get; set; }
        IEnumerator<T> BackIterator { get; set; }
        public void Insert(T element, int? index);
        public void Remove(T? element, int? index);
    }

    public class SortedArray<T> : ICollection<T>
    {
        public Func<T, T, bool> Comparator { get; set; }
        public List<T> Values { get; set; }
        public IEnumerator<T> Iterator
        {
            get
            {
                for (int i = 0; i < Values.Count; i++)
                {
                    yield return this[i];
                }
            }
            set { }
        }
        public IEnumerator<T> BackIterator
        {
            get
            {
                for (int i = Values.Count - 1; i >= 0; i--)
                {
                    yield return this[i];
                }
            }
            set { }
        }

        public T this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        public void Sort()
        {
            for (int i = 1; i < Values.Count; i++)
            {
                int j = i;
                while (j > 0 && Comparator(Values[j - 1], Values[j]))
                {
                    var temp = Values[j - 1];
                    Values[j - 1] = Values[j];
                    Values[j] = temp;
                    j--;
                }
            }
        }

        public void Insert(T element, int? index = null)
        {
            int start = 0;
            int end = Values.Count - 1;
            int middle;

            while (start <= end)
            {
                middle = (start + end) / 2;
                if (Comparator(element, Values[middle]))
                {
                    start = middle + 1;
                }
                else
                {
                    end = middle - 1;
                }
            }
            Values.Insert(start, element);
        }


        public void Remove(T? element, int? index = null)
        {
            List<T> newValues = new List<T>();
            for (int i = 0; i < Values.Count; i++)
            {
                if (!Values[i].Equals(element))
                {
                    newValues.Add(Values[i]);
                }
            }
            Values = newValues;
        }

        public SortedArray(Func<T, T, bool> comparator, List<T> values)
        {
            Comparator = comparator;
            Values = Extensions<T>.Copy(values);
            Sort();
        }

    }

    public static class Extensions<T>
    {
        public static List<T> Copy<T>(List<T> source)
        {
            List<T> copy = new List<T>();

            foreach (T item in source)
            {
                copy.Add(item);
            }

            return copy;
        }
    }

    public class Algorithm<T>
    {
        public T? Find(IEnumerator<T> iterator, Predicate<T> predicate)
        {
            do
            {
                if (predicate(iterator.Current)) return iterator.Current;
            }
            while (iterator.MoveNext());
            return default(T?);
        }
        public void ForEach(IEnumerator<T> iterator, Action<T> action)
        {
            do
            {
                T newItem = iterator.Current;
                action(newItem);
            }
            while (iterator.MoveNext());
        }
        public int CountIf(IEnumerator<T> iterator, Predicate<T> predicate)
        {
            int count = 0;
            do
            {
                if (predicate(iterator.Current))
                {
                    count++;
                }
            }
            while (iterator.MoveNext());
            return count;
        }


    }


}




