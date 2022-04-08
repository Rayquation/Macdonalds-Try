﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MacDonalds_Project
{
    internal class Program
    {
        public static int[] valg = new int[3];
        static void Main(string[] args)
        {
            Menu(args);
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
                Console.WriteLine($"{i}. {item} {MadMenu[tmp]:c}");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            int price = MadMenu[ran];
            Console.WriteLine($"Du har valgt {madMenu[ran]} som koster {MadMenu[ran]:c}");
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
                Console.WriteLine($"{i}. {item} {DivMenu[tmp]:c}");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            int price = DivMenu[ran];
            Console.WriteLine($"Du har valgt {divMenu[ran]} som koster {DivMenu[ran]:c}");
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
                Console.WriteLine($"{i}. {item} {DrikMenu[tmp]:c}");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            int price = DrikMenu[ran];
            Console.WriteLine($"Du har valgt {DrikMenu[ran]} som koster {drikMenu[ran]:c}");
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
        public static void Menu(string[] args)
        {
            Console.WriteLine($"Velkommen til Macdonalds Menuen");
            Console.WriteLine("Vælg hvilken menu du gerne vil ind til");
            string[] Menus = { "MadMenu", "DiverseMenu", "DrikMenu", "ÆndreBestiling", "Betal" };
            int i = 1;
            int tmp = 0;
            foreach (string item in Menus)
            {
                Console.WriteLine($"{i}. {item}");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            Console.WriteLine($"Du har valgt {Menus[ran]}");
            try
            {
                string con = "y";
                int customer = 1;
                int change = 0;
                do
                {
                        if (choice == 1)
                        {
                        valg[0] = FoodMenu();
                        Menu(args);
                        }
                        else if (choice == 2)
                        {
                        valg[1] = DiverseMenu();
                        Menu(args);
                        }
                        else if (choice == 3)
                        {
                        valg[2]=DrikMenu();
                            Menu(args);
                        }
                        else if (choice == 4)
                        {
                            change = MenuChange(valg);
                        if (change == 1)
                        {
                            valg[0] = FoodMenu();
                            Menu(args);
                        }else if (change == 2)
                        {
                            valg[1] = DiverseMenu();
                            Menu(args);
                        }
                        else
                        {
                            valg[2] = DrikMenu();
                            Menu(args);
                        }
                        }
                        else
                        {
                            customer = Pay(valg);
                            Menu(args);
                        }
                }
                while (con != "n");
                Console.WriteLine("Tryk på hvilken som helst knap når du har fået din bestilling");
                Console.ReadKey();
            }
            catch
            {
                Main(args);
            }
        }
        public static int MenuChange(int[] x)
        {
            Console.WriteLine("Hvad vil du gerne ændre i din menu");
            Console.WriteLine("Vælg hvilken menu du gerne vil ind til");
            string[] Menus = { "MadMenu", "DiverseMenu", "DrikMenu" };
            int i = 1;
            int tmp = 0;
            foreach (string item in Menus)
            {
                Console.WriteLine($"{i}. {item}");
                tmp++;
                i++;
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            int ran = choice - 1;
            Console.WriteLine($"Du har valgt {Menus[ran]}");
            return choice;

        }
        public static int Pay(int[] x)
        {
            string con = "y";
            int customer = 1;
            Console.WriteLine($"Du skal i alt betale {x.Sum():c}");
            Console.WriteLine($"efter du har betalt har du {1000 - x.Sum():c} tilbage");
            Console.WriteLine($"Du er nr.{customer} i køen");
            WaitTime(customer);
            Console.WriteLine("Vil du gerne fortsætte {y/n}");
            if (con != "n")
            {
                customer++;
            }
            con = Console.ReadLine();
            con = con.ToLower();
            return customer;
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