using System;
using System.Globalization;

namespace ProjectPartA_A1
{
    class Program
    {
        struct Article
        {
            public string Name;
            public decimal Price;
        }

        const int _maxNrArticles = 10;
        const int _maxArticleNameLength = 20;
        const decimal _vat = 0.25M;

        static Article[] articles = new Article[_maxNrArticles];
        static int nrArticles;

        static void Main(string[] args)
        {
            ReadArticles();
            PrintReciept();
        }

        private static void ReadArticles()
        {
            //Your code to enter the articles
           Console.WriteLine("How many articles do you want (between 1 and 10)? ");
            String input = Console.ReadLine();

            //looping until user provides valid input
            while(!int.TryParse(input, out nrArticles) || nrArticles < 1 || nrArticles > 10)
            {
                Console.WriteLine("Wrong input, please try again.");
                input = Console.ReadLine();
            }

            for (int i = 0; i < nrArticles; i++)
            {
                Console.WriteLine($"Please enter name and price for the article #{i} in the format name; price (example Beer; 2,25):");
                string inArticle = Console.ReadLine();
                bool inputComplit = false;
          
           while(!inputComplit)
                { 

            try {
                    while (string.IsNullOrWhiteSpace(inArticle) || string.IsNullOrEmpty(inArticle.Substring(0, inArticle.IndexOf(";"))) || inArticle.Substring(inArticle.IndexOf(";")).Contains('.'))
                    {
                        if (string.IsNullOrWhiteSpace(inArticle))
                        {
                            Console.WriteLine("Format error");
                            inArticle = Console.ReadLine();
                        }
                        else if (string.IsNullOrEmpty(inArticle.Substring(0, inArticle.IndexOf(";"))) && inArticle.Substring(inArticle.IndexOf(";")).Contains(','))
                        {
                            Console.WriteLine("Name error");
                            inArticle = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Price error");

                            inArticle = Console.ReadLine();
                        }
  
                    }
                        inputComplit = true;  


                    }


              
                catch
                {
                    Console.WriteLine("Wrong input, please try again");
                    continue;
                }
                   
                  
                }
                articles[i].Name = inArticle.Substring(0, inArticle.IndexOf(";"));
                articles[i].Price = Convert.ToDecimal((inArticle.Substring(inArticle.IndexOf(";") + 1).Trim()));


            }
          
          
        }
        private static void PrintReciept()
        {
            //Your code to print out a reciept

            Console.WriteLine();
            Console.WriteLine("Reciept Purchased Articles");

            Console.WriteLine($"Purchased date: {DateTime.Now}");

            Console.WriteLine($"Number of items purchased: {nrArticles}");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("#  | Name                                       Price");
            Console.WriteLine("-----------------------------------------------------");

            //Console.WriteLine($"{"#",-4} {"Name",-30} {"Price",10}");

            decimal total = 0;
            for (int i = 0; i < nrArticles; i++)
            {
                Console.WriteLine($"{i,-2} | {articles[i].Name,-20}   {articles[i].Price.ToString("C2", new CultureInfo("sv-SE")),25} ");
                total += articles[i].Price;
            }
            decimal vat = 0.25M * total;
            Console.WriteLine();
            Console.WriteLine($"Total purchase: {total.ToString("C2", new CultureInfo("sv-SE")),37}");
           // Console.WriteLine($"{"Total purchase: ", -52} {total,-50:C2}");
            Console.WriteLine($"Includes VAT (25%): {vat.ToString("C2", new CultureInfo("sv-SE")),33}");
            Console.WriteLine("-----------------------------------------------------");


        }
    }
}
