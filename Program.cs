using System;

City city = new City();
int manticoreDistance = GameController.InitiateDistance();
Manticore manticore = new Manticore(manticoreDistance);
GameController.GameLoop(city, manticore);

public class Entity
{
    public int Health { get; set; }
}

public class City : Entity
{
    public City() { Health = 15; }
}

public class Manticore : Entity
{
    public int Distance { get; set; }
    public Manticore(int distance) { Health = 10; Distance = distance; }
}

public static class GameController
{
    public static int Round { get; set; } = 1;
    
    public static int InitiateDistance()
    {
        /*
        Console.Write("Please enter distance of Manticore, from 0 to 100. ");
        int input = 101;
        while (input == 101)
        {
            input = Convert.ToInt32(Console.ReadLine());
            if (input < 0 || input > 100) { Console.WriteLine("Invalid entry."); input = 101; }
        }
        Console.Clear();
        return input;
        */

        //Console.WriteLine("The computer is now picking a distance for the manticore. Press any kew to continue.");
        Random random = new Random();
        int input = random.Next(101);
        return input;

    }

    public static void GameLoop(City city, Manticore manticore)
    {
        while (city.Health > 0 && manticore.Health > 0)
        {
            Display(city, manticore);
            int input = 101;
            while (input < 0 || input > 100)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nCity guard, how far away should we fire? Select between 0 and 100. ");
                input = Convert.ToInt32(Console.ReadLine());
                if (input < 0 || input > 100) Console.WriteLine("Invalid entry.");
            }

            if (input == manticore.Distance)
            {
                manticore.Health = manticore.Health - CannonShot();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Clear();
                Console.WriteLine($"Direct hit! {CannonShot()} damage!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                city.Health--;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Clear();
                if (input < manticore.Distance) { Console.WriteLine("Miss! Too low!"); Console.ForegroundColor = ConsoleColor.White; }
                else if (input > manticore.Distance) { Console.WriteLine("Miss! Too high!"); Console.ForegroundColor = ConsoleColor.White; }
            }

            Round++;
        }

        GameOver(city, manticore);

    }

    public static void GameOver(City city, Manticore manticore)
    {
        if (city.Health <= 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Oh no! You lost, you freakin' loser!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (manticore.Health <= 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You won! Hooray for you!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public static void Display(City city, Manticore manticore)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Round: {Round}  City HP: ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{city.Health} / 15  ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Manticore HP: ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{manticore.Health} / 10");
        // vvDebugvv
        //Console.Write($" Manticore distance: {manticore.Distance}");
    }

    public static int CannonShot()
    {
        if (Round % 3 == 0 && Round % 5 == 0) return 10;
        else if (Round % 3 == 0 || Round % 5 == 0) return 3;
        else return 1;
    }

}