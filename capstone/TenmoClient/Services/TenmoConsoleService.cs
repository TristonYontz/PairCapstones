using System;
using System.Collections.Generic;
using TenmoClient.Models;
using TenmoServer.Models;

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
        public void PrintListOfTransfer(List<Transfer> transferList)
        {
            Console.WriteLine("|---------------------------------------|");
            Console.WriteLine("| Transfers                             |");                       
            Console.WriteLine("| ID        From/To         Amount      |");  
            Console.WriteLine("|---------------------------------------|");
            for (int i = 0; i < transferList.Count; i++)
            {
                if (transferList[i].TransferTypeId == 2)
                {
                    Console.WriteLine($"| {Convert.ToString(transferList[i].TransferId).PadRight(8)}  {transferList[i].AccountToName.PadRight(15)} $ {Convert.ToString(transferList[i].Amount).PadRight(9)} |");
                }
                else
                {
                    Console.WriteLine($"| {Convert.ToString(transferList[i].TransferId).PadRight(8)}  {transferList[i].AccountFromName.PadRight(15)} ${Convert.ToString(transferList[i].Amount).PadRight(9)} |");
                }
            }
            Console.WriteLine("|---------------------------------------|");
        }

        public void PrintTransferDetail(List<Transfer> transferList,int transferId)
        {
            for (int i = 0; i < transferList.Count; i++)
            {
                if (transferList[i].TransferId == transferId)
                {
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine("Transfer Details");
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine($"Id: {transferId}");
                    Console.WriteLine($"From: {transferList[i].AccountFromName}");
                    Console.WriteLine($"To: {transferList[i].AccountToName}");

                    if (transferList[i].TransferTypeId == 2)
                    {
                          Console.WriteLine("Type: Send");
                    }
                    else
                    {
                        Console.WriteLine("Type: Request");
                    }

                    if (transferList[i].TransferSatusId == 1)
                    {
                        Console.WriteLine("Status: Pending");
                    }
                    else if (transferList[i].TransferSatusId == 2)
                    {
                        Console.WriteLine("Status: Approved");
                    }
                    else
                    {
                        Console.WriteLine("Status: Rejected");
                    }

                    Console.WriteLine($"Amount: {transferList[i].Amount}");
                    Console.WriteLine("---------------------------------------");
                    Pause();
                    break;
                }
            }
            Console.WriteLine("Please choose a valid transfer ID.");
        }
    }
}
