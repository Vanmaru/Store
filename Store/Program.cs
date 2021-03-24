using System;
using System.Linq;
using System.Text;

namespace Store
{
    class Program
    {
        static void Main(string[] args)
        {
            Product firstGame = new SingleVideoGame("Dark Souls",404,"PC","RPG",60);
            Product secondGame = new MultiVideoGame("Valorant", 0, "PC", "FPS-shooter", "5vs5");
            Product firstBoardGame = new BoardGameForAdult("Poker", 1680, "Card-game", "Cards&chips", "Gambling");
            Product secondBoardGame = new BoardGameForKids("Monopoly", 50, "Game with field", "Field&cube", "Education&Entertaiment");
            
            //ListContainer basket = new ListContainer();
            //basket.Add(firstGame);
            //basket.Add(firstBoardGame);
            //basket.Add(secondBoardGame);
            //basket.Add(secondGame);
            //basket.SortByPrice();
            //Console.WriteLine(basket.ToString());
            //Console.WriteLine(basket[1]);

            //ArrayContainer kr = new ArrayContainer();
            //kr.Add(firstGame);
            //kr.Add(secondGame);
            //kr.Add(firstBoardGame);
            //kr.Add(secondBoardGame);
            //kr.Sort();
            //Console.WriteLine(kr.ToString());
            //Console.WriteLine(kr[1]);
            //Console.WriteLine(kr[1680M]);
            //Console.WriteLine(kr["Dark Souls"]);

            HashTableContainer hashTabCont = new HashTableContainer();
            hashTabCont.Add(firstGame);
            hashTabCont.Add(firstGame);
            hashTabCont.Add(firstGame);
            hashTabCont.Add(secondBoardGame);
            Console.WriteLine(hashTabCont.ToString());
            Console.WriteLine(hashTabCont[1]);
            Console.WriteLine(hashTabCont[1680M]);
            Console.WriteLine(hashTabCont["Dark Souls"]);
            Console.ReadKey();
        }
    }
}