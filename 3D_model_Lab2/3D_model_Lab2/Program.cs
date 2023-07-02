using System.Collections.Generic;

namespace Lab2 { 
    internal class Program
    {
        public interface IQueue<T>
        {
            void Enqueue(T item);
            //добавление элемента в очередь
            T Dequeue(); //изъятие из очереди
            int Count { get; }
            //количество элементов в очереди
        }

        class MQueue<T> : IQueue<T>
        {
            Queue<T> q = new Queue<T>();

            public MQueue(Queue<T> q)
            {
                this.q = q;
            }
            public MQueue()
            {
            }
            public void Enqueue(T item)
            {
                q.Enqueue(item);
            }

            public T Dequeue()
            {
                return q.Dequeue();
            }

            public int Count
            {
                get { return q.Count; }
            }

        }

        class MStack<T> : IQueue<T>
        {
            Stack<T> s = new Stack<T>();

            public MStack(Stack<T> stack)
            {
                s = stack;
            }

            public void Enqueue(T item)
            {
                Stack<T> tmp =  new Stack<T>();
                while (s.Count != 0)
                {
                    tmp.Push(s.Pop());
                }
                while (s.Count != 0)
                {
                    s.Pop();
                }
                s.Push(item);
                while (tmp.Count != 0)
                {
                    s.Push(tmp.Pop());
                }
                tmp.Clear();
            }

            public T Dequeue() 
            {
                Stack<T> tmp = new Stack<T>();
                while (s.Count != 0)
                {
                    tmp.Push(s.Pop());
                }
                while (tmp.Count != 1)
                {
                    s.Push(tmp.Pop());
                }
                return tmp.Pop();
            }

            public int Count
            {
                get { return s.Count; }
            }
        }

        class HotPotato
        {
            IQueue<string> Players;

            public HotPotato(IQueue<string> players)
            {
                this.Players = players;
            }

            public string Play(int n)
            {
                for (int i = 0; i < n; i++)
                {
                    Players.Enqueue(Players.Dequeue());
                }
                return Players.Dequeue();
            }

            public bool GameOver()
            {
                if (Players.Count > 1)
                {
                    return false;
                }
                return true;
            }

            public string Winner()
            {
                if (GameOver())
                {
                    return Players.Dequeue();
                }
                return "Ещё не доиграли";
            }
        }

        static void Main(string[] args)
        {
            MQueue<string> players = new MQueue<string>(new Queue<string>(new List<string> { "Игрок1", "Игрок2", "Игрок3", "Игрок4", "Игрок5", "Игрок6", "Игрок7", "Игрок8" }));
            MStack<string> playersStack = new MStack<string>(new Stack<string>(new List<string> { "Игрок1", "Игрок2", "Игрок3", "Игрок4", "Игрок5", "Игрок6", "Игрок7", "Игрок8" }));
            MStack<string> Out = new MStack<string>(new Stack<string>(new List<string> { "Игрок1", "Игрок2", "Игрок3", "Игрок4", "Игрок5", "Игрок6", "Игрок7", "Игрок8" })); ;
            Console.WriteLine("Игроки:");
            while (Out.Count != 0) 
            {
                Console.Write(Out.Dequeue()+' ');
            }
            HotPotato Game = new HotPotato(players);
            Random n = new Random();
            int r=n.Next(0,100);
            while (!Game.GameOver())
            {
                Console.WriteLine("\nВыпало число {0}", r);
                Console.WriteLine("Погорел игрок {0}", Game.Play(r));
                r = n.Next(0, 100);
            }
            Console.WriteLine("Выиграл {0}!", Game.Winner());
            Console.WriteLine("а теперь стэком:\n");
            HotPotato Game2 = new HotPotato(playersStack);
            r = n.Next(0, 100);
            while (!Game2.GameOver())
            {
                Console.WriteLine("\nВыпало число {0}", r);
                Console.WriteLine("Погорел игрок {0}", Game2.Play(r));
                r = n.Next(0, 100);
            }
            Console.WriteLine("Выиграл {0}!", Game2.Winner());
        }
    }
}