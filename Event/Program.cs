using System;

namespace Event
{ 
   // subscriber in event
    class Program
    {
        static void Main(string[] args)
        {

            var stock = new Stock("Facebook");
            stock.Price = 100;
            stock.onPriceChanged += Stock_onPriceChanged; //subscribing
            stock.changeStockPriceBy(-0.02d);
            stock.changeStockPriceBy(0.01d);

           // Console.WriteLine("-------------After Unsubscribing----------------");

            stock.onPriceChanged -= Stock_onPriceChanged; // unsubscribing
            stock.changeStockPriceBy(0.08d);
            stock.changeStockPriceBy(0.03d);

            Console.ReadKey();

        }

        // call when any cahnge happen on the price
        private static void Stock_onPriceChanged(Stock stock, double oldPrice)
        {
            string result = "";
            if (stock.Price > oldPrice)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                result = "up";
            }
            else if (oldPrice > stock.Price)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                result = "down";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine($"{stock.Name}: ${stock.Price} - {result} \n");
            
        }
    }



    public delegate void stockPriceChangeHandler(Stock s, double oldPrice);
    //publisher
    public class Stock
    {

        public event stockPriceChangeHandler onPriceChanged;

        private double _price;
        public double Price { get => this._price; set => this._price = value; }
        private string _name;
        public string Name { get => this._name; }


        public Stock(string name)
        {
            this._name = name;         
        }
        public void changeStockPriceBy(double percent)
        {
            double oldPrice = this._price;
            this._price += Math.Round(this._price * percent, 2);

            if (onPriceChanged != null) //check if there is any subscriber 
            {
                onPriceChanged(this, oldPrice); // fireing the event
            }
        
        }
    }
}
