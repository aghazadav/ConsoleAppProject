class Program
{
    static void Main()
    {
        Console.WriteLine("Xos geldiniz");
        ShowInitialMenu();
    }

    static void ShowInitialMenu()
    {
        UserManager userManager = new UserManager();
        BankOperations bankOperations = new BankOperations();

        while (true)
        {
            Console.WriteLine("Daxil olmaq ucun 1-i, Qeydiyatddan kecmek ucun 2-ni daxil edin:");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                User user = userManager.Login();
                if (user != null)
                {
                    ShowUserMenu(user, bankOperations);
                }
            }
            else if (choice == "2")
            {
                userManager.Register();
            }
            else
            {
                Console.WriteLine("Yeniden cehd edin. 1 veya 2 ");
            }
        }
    }

    static void ShowUserMenu(User user, BankOperations bankOperations)
    {
        while (true)
        {
            Console.WriteLine("/n Emeliyyati secin:");
            Console.WriteLine("1. Medaxil");
            Console.WriteLine("2. Mexaric");
            Console.WriteLine("3. Kredit");
            Console.WriteLine("4. Balansin gosterilmesi");
            Console.WriteLine("5. Cixis");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bankOperations.Deposit(user);
                    break;
                case "2":
                    bankOperations.Withdraw(user);
                    break;
                case "3":
                    if (user.HasJob)
                    {
                        bankOperations.Credit(user);
                    }
                    else
                    {
                        Console.WriteLine("Is yeri olmadan kredit muracieti ede bilmezsiniz.");
                    }
                    break;
                case "4":
                    bankOperations.ShowBalance(user);
                    break;
                case "5":
                    Console.WriteLine("Programdan cixis edilir. Sizi yeniden gozleyirik...");
                    return;
                default:
                    Console.WriteLine("Yenidən cehd edin. 1 2, 3, 4 veya 5");
                    break;
            }
        }
    }
}
