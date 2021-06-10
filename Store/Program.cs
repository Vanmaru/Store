using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.helper;
namespace Store
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleVideoGame firstGame = new SingleVideoGame("Dark Souls", 404, "PC", "RPG", 60);
            MultiVideoGame secondGame = new MultiVideoGame("Valorant", 10, "PC", "FPS-shooter", "5vs5");
            BoardGameForAdult firstBoardGame = new BoardGameForAdult("Poker", 1680, "Card-game", "Cards&chips", true);
            BoardGameForKids secondBoardGame = new BoardGameForKids("Monopoly", 50, "Game with field", "Field&cube", "Education&Entertaiment");

            //ListContainer basket = new ListContainer();
            //basket.Add(firstGame);
            //basket.Add(firstBoardGame);
            //basket.Add(secondBoardGame);
            //basket.Add(secondGame);
            //Console.WriteLine(basket.ToString());

            ArrayContainer<Product> kr = new ArrayContainer<Product>
            {
                firstGame,
                secondGame,
                firstBoardGame,
                secondBoardGame,
                new SingleVideoGame("Dark Souls 3", 1680, "PC", "RPG", 100),
                new SingleVideoGame("The Long Dark", 1680, "PC", "Survival", 80)
            };
            foreach (var item in kr.MaxPrice())
            {
                Console.WriteLine(item);
            }
            //Console.WriteLine(kr.TotalPrice);
            //firstGame.Price = 1000000;

            //Console.WriteLine(kr.ToString());
            
            //kr.Sort((first, second) => (first.Price> second.Price ));
            //Console.WriteLine(kr.ToString());

            //kr.Sort((first, second) => first.Price > second.Price);
            //Console.WriteLine(kr.ToString());
            //Console.WriteLine(kr.Find((a) => a.Name.Contains("Poker")));
            //Serializer.Save("test.bin", kr);
            //var t = Serializer.Load("test.bin");

            //Console.WriteLine(t.ToString());

            //kr.TotalEvent += kr.totalPrice;
            //kr.FromBinaryFile("qwe.bin");

            //object cont = Serializer<ICustomSerializable<ArrayContainer<Product>>>.Load("test.bin");

            //Console.WriteLine(cont.ToString());
            //Console.WriteLine(kr.ToString());
            //foreach (var item in kr)
            //{
            //    Console.WriteLine(item);
            //}
            //ListContainer<Product> lc = new ListContainer<Product>
            //{
            //    new BoardGameForAdult("XXX", 100, "kinda", "some", true),
            //    new BoardGameForAdult("XL", 99, "kinda", "some", true)
            //};

            //Console.WriteLine(lc.ToString());
            //HashTableContainer<Product> h = new HashTableContainer<Product>(50)
            //{
            //    firstGame,
            //    secondGame,
            //    secondBoardGame,
            //    firstBoardGame
            //};
            //Console.WriteLine(h.ToString());

            //ICustomSerializable<Product> t = default;
            //t = Serializer<Product>.Load("test.bin", t);
            //Console.WriteLine(t.ToString());
            //Serializer<Product>.Save("hashT.bin", h);

            //List<Product> ls = new List<Product>();
            ////for (int i = 0; i < 20000000; i++)
            ////    ls.Add(new Product(price: i));
            //decimal sum = 0;
            //var ts = DateTime.Now;
            //for (int i = 0; i < ls.Count; i++)
            //    sum += ls[i].Price;
            //Console.WriteLine($"Sum={sum},time={(DateTime.Now - ts).TotalMilliseconds}");
            //sum = 0;
            //ts = DateTime.Now;
            //foreach (var d in ls)
            //    sum += d.Price;
            //Console.WriteLine($"Sum={sum},time={(DateTime.Now - ts).TotalMilliseconds}");
            Console.ReadKey();
        }
    }
}