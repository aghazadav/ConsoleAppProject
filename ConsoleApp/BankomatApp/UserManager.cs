public class UserManager
{
    private Dictionary<string, User> users = new Dictionary<string, User>();
    private int loginAttempts = 0;

    public void Register()
    {
        Console.WriteLine("Adinizi daxil edin:");
        string firstName = Console.ReadLine();
        while (firstName.Length < 2)
        {
            Console.WriteLine("Ad minimum 2 herf den ibaret olmalidir:");
            firstName = Console.ReadLine();
        }

        Console.WriteLine("Soyadinizi daxil edin:");
        string lastName = Console.ReadLine();
        while (lastName.Length < 2)
        {
            Console.WriteLine("Soyadiniz 2 herfden ibaret olmalidir:");
            lastName = Console.ReadLine();
        }

        Console.WriteLine($"Dogum tarixinizi numunede oldugu kimi (1963-09-03) (09 ay, 03 ise gunu temsil edir) daxil edin:");
        DateTime birthDate;
        while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
        {
            Console.WriteLine("Sehv tarix formatı. Yenidən cehd edin:");
        }

        if ((DateTime.Now.Year - birthDate.Year) < 18 ||
            (DateTime.Now.Year - birthDate.Year) == 18 && DateTime.Now.DayOfYear < birthDate.DayOfYear)
        {
            Console.WriteLine("Qeydiyyat ucun minimum yas heddi 18 olmalidir.");
            return;
        }
        Console.WriteLine("Emailinizi daxil edin:");
        string email = Console.ReadLine();
        while (!IsValidEmail(email))
        {
            Console.WriteLine("Sehv email formati. Yeniden daxil edin:");
            email = Console.ReadLine();
        }

        Console.WriteLine("Sifrenizi daxil edin:");
        string password = Console.ReadLine();

        Console.WriteLine("Sifrenizi yeniden daxil edin:");
        string confirmPassword = Console.ReadLine();
        while (password != confirmPassword)
        {
            Console.WriteLine("Sifreler uygun gelmir. Yeniden daxil edin:");
            confirmPassword = Console.ReadLine();
        }

        password = EncryptPassword(password);

        Console.WriteLine("Is yeriniz varmi? (beli/xeyr):");
        string hasJobInput = Console.ReadLine();
        bool hasJob = hasJobInput.ToLower() == "beli";

        User user = new User(firstName, lastName, birthDate, email, password, hasJob);
        users[email] = user;

        Console.WriteLine("Qeydiyyat ugurla tamamlandi!");
    }

    public User Login()
    {
        if (loginAttempts >= 3)
        {
            Console.WriteLine("Sehv daxil etme limitine catdiniz. Proqram baglanilir.");
            Environment.Exit(0);
        }

        Console.WriteLine("Emailinizi daxil edin:");
        string email = Console.ReadLine();
        while (!IsValidEmail(email))
        {
            Console.WriteLine("Email formati duzgun deyil. Do you want to continue to login or exit? (davam/cixis)");
            string choice = Console.ReadLine().ToLower();
            if (choice == "cixis")
            {
                Environment.Exit(0);
            }
            else if (choice != "davam")
            {
                Console.WriteLine("Sehv secim etdiniz. Devam etmek ucun 'davam' cixmaq ucun 'cixis' daxil edin.");
                continue;
            }
            Console.WriteLine("Emailinizi daxil edin:");
            email = Console.ReadLine();
        }

        Console.WriteLine("Sifrenizi daxil edin:");
        string password = Console.ReadLine();
        password = EncryptPassword(password);

        if (users.ContainsKey(email) && users[email].Password == password)
        {
            if ((DateTime.Now.Year - users[email].BirthDate.Year) >= 18)
            {
                loginAttempts = 0;
                Console.WriteLine("Daxil olma ugurldur!");
                return users[email];
            }
            else
            {
                Console.WriteLine("18 yasindan kicikler sisteme daxil ola bilmez.");
            }
        }
        else
        {
            loginAttempts++;
            Console.WriteLine($"Email ve ya sifre sehvdir. {3 - loginAttempts} ugursuz daxil olma haqqiniz qaldi.");
        }
        return null;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private string EncryptPassword(string password)
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(data);
    }
}
