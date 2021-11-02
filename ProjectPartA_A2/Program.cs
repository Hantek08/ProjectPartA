using System;
using System.Globalization;

namespace ProjectPartA_A2
{
    class Program
    {
        struct Article
        {
            public string Name;
            public decimal Price;
        }

        const int _maxNrArticles = 3;
        const int _maxArticleNameLength = 20;
        const decimal _vat = 0.25M;

        static Article[] articles = new Article[_maxNrArticles];
        static int nrArticles = 0;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to Project Part A\n");
            int menuSel = 5;
            do
            {
                menuSel = MenuSelection();
                MenuExecution(menuSel);

            } while (menuSel != 5);
           
        }

        private static int MenuSelection()
        {
            int menuSel = 5;

            //Your code for menu selection
            Console.WriteLine($"{nrArticles} articles entered.");
            Console.WriteLine("Menu:\n" + 
                "1 - Enter an article\n" + 
                "2 - Remove an article\n" + 
                "3 - Print recept sorted by price\n" + 
                "4 - Print recipt by name\n" +
                "5 - Quit\n" );
            string input = Console.ReadLine();
            bool valid = int.TryParse(input, out menuSel);

            return menuSel;
        }
        private static void MenuExecution(int menuSel)
        {  
            //Your code for execution based on the menu selection

            try
             {
                switch(menuSel)
                {
                    case 1: 
                        ReadAnArticle();
                        break;
                    case 2: 
                        RemoveAnArticle();
                        break;
                    case 3:
                        PrintReciept("Price");
                        break;
                    case 4:
                        PrintReciept("Name");
                        break;
                    default:
                        Console.WriteLine("Wrong selection, please try again.");
                        break;
                }
            }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error: {ex.Message}");
             }
        }

        private static void ReadAnArticle()
        {
            //Your code to enter an article
            
                Console.WriteLine($"Please enter name and price for article (example Beer; 2,25):");
                string inputStr = Console.ReadLine().Trim();
                string[] inArticle = inputStr.Split(';');


            while (string.IsNullOrWhiteSpace(inputStr) || inArticle[0].Length == 0 || inArticle[1].Contains('.')) 
                    {
                        if (string.IsNullOrWhiteSpace(inputStr))
                        {
                            Console.WriteLine("Error: Article input format error.");
                            //inputStr = Console.ReadLine();
                        }
                        else if (inArticle[0].Length == 0 && inArticle[1].Contains(','))
                        {
                            Console.WriteLine("Name error");
                        }
                        else
                        {
                            Console.WriteLine("Price error");
                        }
                        inputStr = Console.ReadLine();
                        
                    }

                    articles[nrArticles].Name = inArticle[0];
                    articles[nrArticles].Price = Convert.ToDecimal(inArticle[1]);
                    nrArticles++;

        }
        private static void RemoveAnArticle()
        {
            //Your code to remove an article
            Console.WriteLine("Please enter name of article to remove (Example Beer)");
            string articleToRemove = Console.ReadLine();
            int removeIndex = -1;

           for (int i = 0; i < nrArticles; i++)
               {
                   if(articles[i].Name == articleToRemove)
                   {
                       removeIndex = i;
                       break;
                   }

               }
               if (removeIndex >=0)
               {
                   for (int i = removeIndex; i < nrArticles - 1; i++)
                   {
                       articles[i] = articles[i + 1];
                   }
                   nrArticles--;
               }
               else
                Console.WriteLine($"Error: Article {articleToRemove} not found. Cannot remove.\n");
        }

       



        private static void PrintReciept(string title)
        {
            //Your code to print a receipt
            if(title == "Name")
            {
                SortArticles(true);
            }
            else if(title == "Price")
            {
                SortArticles(false);
            }


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
            Console.WriteLine($"Includes VAT (25%): {vat.ToString("C2", new CultureInfo("sv-SE")),33}");
            Console.WriteLine("-----------------------------------------------------");
        }

        private static void SortArticles( bool sortByName = false)
        {
            // SelectionSort
            Article temp; 
            int smallest;
            {
                for (int i = 0; i < nrArticles - 1; i++)
                {
                    smallest = i;
                    for (int j = i+1; j < nrArticles; j++)
                    {
                        if (sortByName == true)
                        {
                            if (articles[j].Name.CompareTo(articles[smallest].Name) < 0)
                            {
                                // keep track of the index that smallest is in
                                smallest = j;
                            }
                        }
                        else
                        {

                            if (articles[j].Price < articles[smallest].Price)
                            {
                                smallest = j;
                            }

                        }

                    }
                    //Swap if smallest no longer equals i
                    if (smallest != i)
                    {
                        temp = articles[smallest];
                        articles[smallest] = articles[i];
                        articles[i] = temp;
                    }
                    
                }
            }
        }

}
}

