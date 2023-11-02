using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoConsoleService : ConsoleService
    {
        /************************************************************
            Print methods
        ************************************************************/
        public void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }

        public void PrintMainMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Hello, {username}!");
            Console.WriteLine("1: View your current balance");
            Console.WriteLine("2: View your past transfers");
            Console.WriteLine("3: View your pending requests");
            Console.WriteLine("4: Send TE bucks");
            Console.WriteLine("5: Request TE bucks");
            Console.WriteLine("6: Log out");
            Console.WriteLine("0: Exit");
            Console.WriteLine("---------");
        }
        public LoginUser PromptForLogin()
        {
            string username = PromptForString("User name");
            if (String.IsNullOrWhiteSpace(username))
            {
                return null;
            }
            string password = PromptForHiddenString("Password");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        // Add application-specific UI methods here...

        public void PrintAccountBalance(TenmoApiService tenmoApiService)
        {
            decimal balance = tenmoApiService.GetAccount().Balance;
            Console.WriteLine();
            Console.WriteLine($"Your current account balance is: ${balance}");
            Console.ReadLine();
        }
        public void PrintListOfUsers(List<ApiUser> userList)
        {
            //______________________________________________CLEAN CODE HERE_______________________________________________________________MOVE METHODS________________________________________________________
            Console.WriteLine("|-----------------Users-----------------|");
            Console.WriteLine("|   Id   |  Username                    |");
            Console.WriteLine("|--------+------------------------------|");
            for(int i = 0; i < userList.Count; i++)
            {
                Console.WriteLine($"|  {userList[i].UserId}  |  {userList[i].Username.PadRight(28)}|");
            }
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine();
        }
    }
}
