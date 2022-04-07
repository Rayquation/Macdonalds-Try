using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MacDonalds_Project
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int[] choice = new int[3];
            string con = "y";
            int customer = 1;
            do
            {
                Console.WriteLine("Velkommen Til Macdonalds");
                choice[0] = FoodMenu();
                choice[1] = DiverseMenu();
                choice[2] = DrikMenu();
                Console.WriteLine($"Du skal i alt betale {choice.Sum()}.kr");
                Console.WriteLine($"efter du har betalt har du {1000 - choice.Sum()}.kr tilbage");
                Console.WriteLine($"Du er nr.{customer} i køen");
                WaitTime(customer);
                Console.WriteLine("Vil du gerne fortsætte {y/n}");
                if (con != "n")
                {
                    customer++;
                }
                con = Console.ReadLine();
                con = con.ToLower();
            }
            while (con != "n");
            Console.WriteLine("Tryk på hvilken som helst knap når du har fået din bestilling");
            Console.ReadKey();
        }
        static int FoodMenu()
        {
            string[] madMenu = { "CheeseBurger", "Hamburger", "TastyBacon" };
            int[] MadMenu = { 58, 60, 79 };
            Console.WriteLine("Vælg din menu:");
            int i = 1;
            int tmp = 0;
            foreach (string item in madMenu)
            {
                Console.WriteLine($"{i}. {item} {MadMenu[tmp]}.kr");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            int price = MadMenu[ran];
            Console.WriteLine($"Du har valgt {madMenu[ran]} som koster {MadMenu[ran]}.kr");
            return price;
        }
        static int DiverseMenu()
        {
            string[] divMenu = { "BBQ", "Maynoaise", "Ketchup" };
            int[] DivMenu = { 9, 5, 4 };
            Console.WriteLine("Vælg dypelse: ");
            int i = 1;
            int tmp = 0;
            foreach (string item in divMenu)
            {
                Console.WriteLine($"{i}. {item} {DivMenu[tmp]}.kr");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            int price = DivMenu[ran];
            Console.WriteLine($"Du har valgt {divMenu[ran]} som koster {DivMenu[ran]}.kr");
            return price;
        }
        static int DrikMenu()
        {
            string[] drikMenu = { "Cola", "Fanta", "Sport" };
            int[] DrikMenu = { 16, 19, 21 };
            Console.WriteLine("Vælg din drikkelse: ");
            int i = 1;
            int tmp = 0;
            foreach (string item in drikMenu)
            {
                Console.WriteLine($"{i}. {item} {DrikMenu[tmp]}.kr");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            int price = DrikMenu[ran];
            Console.WriteLine($"Du har valgt {DrikMenu[ran]} som koster {drikMenu[ran]}.kr");
            return price;
        }
        public static void WaitTime(int customer)
        {
            EventMenuSleep sleep = new EventMenuSleep();
            sleep.Kitchen += async (s, args) =>
            {
                Random rand = new Random();
                int random = rand.Next(100);
                await Task.Delay(random * 1000);
                Console.WriteLine($"Din mad er klar {customer} og det tog {random} sekunder");
                Thread.Sleep(5000);
            };
            sleep.TimeLimit();
        }
    }
    public class EventMenuSleep
    {
        public event EventHandler Kitchen;
        public void TimeLimit()
        {
            EventHandler handler = Kitchen;
            Kitchen.Invoke(this, EventArgs.Empty);
        }
    }
}