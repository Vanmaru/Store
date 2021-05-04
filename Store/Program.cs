using System;
using System.Linq;
using System.Text;

namespace Store
{
    class Program
    {
        static void Main(string[] args)
        {
            SingleVideoGame firstGame = new SingleVideoGame("Dark Souls",404,"PC","RPG",60);
            MultiVideoGame secondGame = new MultiVideoGame("Valorant", 0, "PC", "FPS-shooter", "5vs5");
            BoardGameForAdult firstBoardGame = new BoardGameForAdult("Poker", 1680, "Card-game", "Cards&chips", true);
            BoardGameForKids secondBoardGame = new BoardGameForKids("Monopoly", 50, "Game with field", "Field&cube", "Education&Entertaiment");

            //ListContainer basket = new ListContainer();
            //basket.Add(firstGame);
            //basket.Add(firstBoardGame);
            //basket.Add(secondBoardGame);
            //basket.Add(secondGame);
            //basket.SortByPrice();
            //Console.WriteLine(basket.ToString());
            //Console.WriteLine(basket[1]);

            ArrayContainer<Product> kr = new ArrayContainer<Product>();
            kr.Add(firstGame);
            kr.Add(secondGame);
            kr.Add(firstBoardGame);
            kr.Add(secondBoardGame);
            kr.Sort();
            //Console.WriteLine(kr.ToString());
            //Console.WriteLine(kr[1]);
            //Console.WriteLine(kr["Dark Souls"]);

            IOrdereableContainer<Product> lc = new ListContainer<Product>();
            lc.Add(new BoardGameForAdult("XXX", 100, "kinda", "some", true));
            lc.Add(new BoardGameForAdult("XL", 99, "kinda", "some", true));
            Console.WriteLine(lc.ToString());
            lc.Add(kr);
            lc.Sort();
            Console.WriteLine(lc.ToString());
            try
            {
                Console.WriteLine(lc[20].ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //HashTableContainer hashTabCont = new HashTableContainer(50);
            //try
            //{
            //    hashTabCont.Add(firstGame);
            //    hashTabCont.Add(firstGame);
            //    hashTabCont.Add(firstGame);
            //    hashTabCont.Add(secondBoardGame);
            //    Console.WriteLine(hashTabCont.ToString());
            //    Console.WriteLine(hashTabCont[1]);
            //    Console.WriteLine(hashTabCont[1680M]);
            //    Console.WriteLine(hashTabCont["Dark Souls"]);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            Console.ReadKey();
        }
    }
}