public class BankOperations
{
    public void Deposit(User user)
    {
        Console.WriteLine("Medaxil edilecek miqdari daxil edin:");
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Yeniden cehd edin:");
        }
        user.Balance += amount;
        Console.WriteLine($"Medaxil ugurla tamamlandi. Balansiniza {user.Balance} AZN elave olundu");
    }

    public void Withdraw(User user)
    {
        Console.WriteLine("Mexaric edilecek miqdari elave edin:");
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Yeniden cehd edin:");
        }
        if (user.Balance >= amount)
        {
            user.Balance -= amount;
            Console.WriteLine($"Mexaric ugurla tamamlandi. Yeni balansiniz: {user.Balance} AZN");
        }
        else
        {
            Console.WriteLine("Kifayet qeder vesait movcut deyil.");
        }
    }

    public void Credit(User user)
    {
        const decimal maxCreditAmount = 1000;
        const int maxCreditMonths = 12;

        if (user.CreditLimit <= 0)
        {
            Console.WriteLine("Kredit limitine catmisiniz.");
            return;
        }

        Console.WriteLine("Kredit miqdarini daxil edin:");
        decimal creditAmount;
        while (!decimal.TryParse(Console.ReadLine(), out creditAmount) || creditAmount <= 0 || creditAmount > user.CreditLimit)
        {
            Console.WriteLine($"Kredit miqdari sehvdir. Maksimum miqdar {user.CreditLimit} AZN. Yeniden cehd edin:");
        }

        Console.WriteLine("Krediti goturmek istediyiniz ay (ma):");
        int creditMonths;
        while (!int.TryParse(Console.ReadLine(), out creditMonths) || creditMonths <= 0 || creditMonths > maxCreditMonths)
        {
            Console.WriteLine($" Maksimum ay miqdari {maxCreditMonths}. Yeniden cehd edin:");
        }

        decimal monthlyPayment = creditAmount / creditMonths;
        user.Balance += creditAmount;
        user.CreditLimit -= creditAmount;
        Console.WriteLine($"Kredit tesdiqlendi. Her ay odemeli oldugunuz miqdar {monthlyPayment} AZN . Yeni Balansiniz: {user.Balance} AZN");
    }

    public void ShowBalance(User user)
    {
        Console.WriteLine($"Balansinizdaki vesait miqdari: {user.Balance} AZN");
    }
}
