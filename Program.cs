class Program
{
    static void Login()
    {
        try
        {
            Console.WriteLine("SIlahkan Login!");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            if (username == "admin" && password == "admin")
            {
                Console.WriteLine("Login Berhasil!");
                // menuAdmin();
            }
            else if (username == "penyewa" && password == "p")
            {
                Console.WriteLine("Login Berhasil!");
                // menuPenyewa();
            }
            else
            {
                Console.WriteLine("Username atau Password salah, silahkan coba lagi!");
                Login();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Terjadi Kesalahan: {e.Message}");
            Login();
        }

    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Selamat Datang di Penyewaan Lapangan Futsal");
        Login();
    }
}
