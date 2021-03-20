using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store
{
    public abstract class Product
    {
        private string name;
        private decimal price;

        public Product(string name="Sample", decimal price=404)
        {
            Name = name;
            Price = price;
        }

        public string Name
        {
            get {return name;}
            set{ name = value;}
        }

        public decimal Price
        {
            get{ return price;}
            set{price = value;}
        }
        public override string ToString()
        {
            return $"Name {name}, Price {price}";
        }
    }

    public class VideoGame : Product
    {
        public VideoGame(string name, decimal price, string platform, string genre):base(name,price)
        {
            this.Platform = platform;
            this.Genre = genre;
        }
        private string platform;
        private string genre;

        public string Platform
        {
            get
            {
                return platform;
            }
            set
            {
                platform = value;
            }
        }

        public string Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Platform: {platform}, Genre: {genre}";
        }
    }

    public class BoardGame : Product
    {
        public BoardGame(string name, decimal price, string kind, string attributes) : base(name, price)
        {
            this.Kind = kind;
            this.Attributes = attributes;
        }
        private string kind;
        private string attributes;

        public string Kind
        {
            get
            {
                return kind;
            }
            set
            {
                kind = value;
            }
        }

        public string Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                attributes = value;
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Kind: {kind}, Attributes: {attributes}";
        }
    }
    public class BoardGameForAdult : BoardGame
    {
        public BoardGameForAdult(string name, decimal price, string kind, string attributes, string isGambling) : base(name, price, kind, attributes)
        {
            this.IsGambling = isGambling;
        }
        private string isGambling;
        public string IsGambling
        {
            get
            {
                return isGambling;
            }
            set
            {
                isGambling = value;
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Is gambling: {isGambling}";
        }
    }

    public class BoardGameForKids : BoardGame
    {
        public BoardGameForKids(string name, decimal price, string kind, string attributes, string goal) : base(name, price, kind, attributes)
        {
            this.Goal = goal;
        }
        private string goal;
        public string Goal
        {
            get
            {
                return goal;
            }
            set
            {
                goal = value;
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Goal: {goal}";
        }
    }

    public class SingleVideoGame : VideoGame
    {
        public SingleVideoGame(string name, decimal price, string platform, string genre, int plotDuration) : base(name, price,platform,genre)
        {
            this.PlotDuration = plotDuration;
        }
        private int plotDuration;
        public int PlotDuration
        {
            get
            {
                return plotDuration;
            }
            set
            {
                plotDuration = value;
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Plot duration: {plotDuration}";
        }
    }

    public class MultiVideoGame : VideoGame
    {
        public MultiVideoGame(string name, decimal price, string platform, string genre, string typeOfMultiplayer) : base(name, price, platform, genre)
        {
            this.TypeOfMultiplayer = typeOfMultiplayer;
        }
        private string typeOfMultiplayer;
        public string TypeOfMultiplayer
        {
            get
            {
                return typeOfMultiplayer;
            }
            set
            {
                typeOfMultiplayer = value;
            }
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Type of multiplayer: {typeOfMultiplayer}";
        }
    }
}