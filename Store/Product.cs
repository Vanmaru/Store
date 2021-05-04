using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Store.helper;

namespace Store
{
    public abstract class Product : Object,IName, IName<Product>
    {
        private string name;
        private decimal price;

        public Product(string name = "Sample", decimal price = 404)
        {
            Name = name;
            Price = price;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public decimal Price
        {
            get
            {
                if (price < 0)
                    throw new NegativeValueException("negative price value");
                return price;
            }
            set { price = value; }
        }
        // by price, then by name
        public virtual int CompareTo(object obj)
        {
            Product other = obj as Product;
            return this.CompareTo(other);
        }

        public virtual int CompareTo(Product other)
        {
            
            int res = this.Price.CompareTo(other.Price);
            if (res == 0)
            {
                res = this.name.CompareTo(other.name);
            }
            return res;
        }
        public override string ToString()
        {
            return $"Name {name}, Price {price}";
        }
    }
    public class VideoGame:Product, IName, IName<VideoGame>
    {
        public VideoGame(string name, decimal price, string platform, string genre) : base(name, price)
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

        public override int CompareTo(object obj)
        {
            int res = base.CompareTo(obj);
            if (res == 0)
                res = this.platform.CompareTo((obj as VideoGame).platform);
            if (res == 0)
                res = this.genre.CompareTo((obj  as VideoGame).genre);
            return res;
        }

        public int CompareTo(VideoGame other)
        {
            int res = base.CompareTo(other);
            if (res == 0)
                res = this.platform.CompareTo(other.platform);
            if (res == 0)
                res = this.genre.CompareTo(other.genre);
            throw new NotImplementedException();
        }
    }

    public class BoardGame : Product, IName, IName<BoardGame>
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
        public override int CompareTo(object obj)
        {
            int res = base.CompareTo(obj);
            if (res == 0)
                res = this.kind.CompareTo((obj as BoardGame).kind);
            if (res == 0)
                res = this.attributes.CompareTo((obj as BoardGame).attributes);
            return res;
        }
        public int CompareTo(BoardGame other)
        {
            int res = base.CompareTo(other);
            if (res == 0)
                res = this.kind.CompareTo(other.kind);
            if (res == 0)
                res = this.attributes.CompareTo(other.attributes);
            throw new NotImplementedException();
        }
    }
    public class BoardGameForAdult : BoardGame, IName, IName<BoardGameForAdult>
    {
        public BoardGameForAdult(string name, decimal price, string kind, string attributes, bool isGambling) : base(name, price, kind, attributes)
        {
            this.IsGambling = isGambling;
        }
        private bool isGambling;
        public bool IsGambling
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
        public override int CompareTo(object obj)
        {
            int res = base.CompareTo(obj);
            if (res == 0)
                res = this.isGambling.CompareTo((obj as BoardGameForAdult).isGambling);
            return res;
        }
        public int CompareTo(BoardGameForAdult other)
        {
            int res = base.CompareTo(other);
            if (res == 0)
                res = this.isGambling.CompareTo(other.isGambling);
            throw new NotImplementedException();
        }
    }

    public class BoardGameForKids : BoardGame, IName, IName<BoardGameForKids>
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

        public int CompareTo(BoardGameForKids other)
        {
            throw new NotImplementedException();
        }
    }

    public class SingleVideoGame : VideoGame, IName, IName<SingleVideoGame>
    {
        public SingleVideoGame(string name, decimal price, string platform, string genre, int plotDuration) : base(name, price, platform, genre)
        {
            this.PlotDuration = plotDuration;
        }
        private int plotDuration;
        public int PlotDuration
        {
            get
            {
                if (plotDuration <= 0)
                    throw new NegativeValueException("negative plot duration");
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

        public int CompareTo(SingleVideoGame other)
        {
            throw new NotImplementedException();
        }
    }

    public class MultiVideoGame : VideoGame, IName, IName<MultiVideoGame>
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

        public int CompareTo(MultiVideoGame other)
        {
            throw new NotImplementedException();
        }
    }
}