using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;

public class Jadwal
{
    public string NamaPemesan { get; set; }
    public string NomorTelepon { get; set; }
    public string TanggalPesan { get; set; } // "dd-MM-yyyy"
    public string Waktu { get; set; }         // "HH:mm"
    public string NomorLapangan { get; set; }
    public int Durasi { get; set; }
    public double Harga { get; set; }

    public Jadwal(string namaPemesan, string nomorTelepon, string tanggalPesan,
                  string waktu, string nomorLapangan, int durasi, double harga)
    {
        NamaPemesan = namaPemesan;
        NomorTelepon = nomorTelepon;
        TanggalPesan = tanggalPesan;
        Waktu = waktu;
        NomorLapangan = nomorLapangan;
        Durasi = durasi;
        Harga = harga;
    }
}

class Program
{
    
    private static string csvFileName = "jadwal.csv";
    private static List<Jadwal> listJadwal = new List<Jadwal>();

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
                menuAdmin();
            }
            else if (username == "" && password == "")
            {
                Console.WriteLine("Login Berhasil!");
                // menuPenyewa();
            }
            else
            {
                Console.WriteLine("Username atau Password salah, silahkan coba lagi!");
                SaveToCSV();
                Login();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Terjadi Kesalahan: {e.Message}");
            Login();
        }

    }

    public static void menuAdmin()
    {
        Console.WriteLine("\n========================= Tabel Penyewa =======================");
        TampilkanJadwal(); // Menampilkan semua jadwal yang sudah di-load

        Console.WriteLine("\nPilihan:");
        Console.WriteLine("1. Tambah Jadwal");
        Console.WriteLine("2. Edit Jadwal");
        Console.WriteLine("3. Hapus Jadwal");
        Console.WriteLine("4. Logout");
        Console.Write("Masukkan pilihan: ");
        string pilihan = Console.ReadLine();

        if (pilihan == "1")
        {
            TambahJadwal(); // Panggil method TambahJadwal
        }
        else if (pilihan == "2")
        {
            EditJadwal();// Panggil method EditJadwal
        }
        else if (pilihan == "3")
        {
            HapusJadwal();// Panggil Method HapusJadwal
        }
        else if (pilihan == "4")
        {
            Console.WriteLine("Anda telah logout.");
            Login();
        }
        else
        {
            Console.WriteLine("Pilihan tidak valid!");
        }
    }
    public static void TampilkanJadwal()
    {
        Console.WriteLine("No | Nama Pemesan | Nomor Telepon | Tanggal Pesan | Waktu | Lapangan | Durasi | Harga");
        if (listJadwal.Count == 0)
        {
            Console.WriteLine("Belum ada jadwal.");
        }
        else
        {
            for (int i = 0; i < listJadwal.Count; i++)
            {
                Jadwal j = listJadwal[i];
                Console.WriteLine($"{(i+1)}.  {j.NamaPemesan} | {j.NomorTelepon} | {j.TanggalPesan} | {j.Waktu} | {j.NomorLapangan} | {j.Durasi} | {j.Harga}");
            }
        }
    }
    public static void TambahJadwal()
    {
        Console.WriteLine("\n=== Tambah Jadwal ===");

        Console.Write("Nama Pemesan: ");
        string namaPemesan = Console.ReadLine();

        Console.Write("Nomor Telepon: ");
        string nomorTelepon = Console.ReadLine();

        Console.Write("Tanggal Pesan (ddMMyyyy): ");
        string tanggalInput = Console.ReadLine();
        string tanggalPesan = DateTime.ParseExact(tanggalInput, "ddMMyyyy", CultureInfo.InvariantCulture)
                                     .ToString("dd-MM-yyyy");

        Console.Write("Waktu (HHmm): ");
        string waktuInput = Console.ReadLine();
        string waktu = DateTime.ParseExact(waktuInput, "HHmm", CultureInfo.InvariantCulture)
                               .ToString("HH:mm");

        Console.Write("Nomor Lapangan: ");
        string nomorLapangan = Console.ReadLine();

        Console.Write("Durasi Sewa (dalam jam): ");
        int durasi = int.Parse(Console.ReadLine());

        double harga = hitungHarga(durasi);
        
        Jadwal jadwalBaru = new Jadwal(namaPemesan, nomorTelepon, tanggalPesan,
                                       waktu, nomorLapangan, durasi, harga);
        listJadwal.Add(jadwalBaru);
    
        SaveToCSV();
        menuAdmin();

    }
    public static void EditJadwal()
    {
        TampilkanJadwal();
        if (listJadwal.Count == 0)
        {
            menuAdmin();
            return;
        }

        Console.Write("\nMasukkan nomor jadwal yang ingin diedit: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index < 0 || index >= listJadwal.Count)
        {
            Console.WriteLine("Nomor jadwal tidak valid!");
            menuAdmin();
            return;
        }

        Jadwal jadwal = listJadwal[index];

        Console.WriteLine($"\n=== Edit Jadwal ke-{index + 1} ===");
        
        Console.Write($"Nama Pemesan (sebelumnya: {jadwal.NamaPemesan}): ");
        string namaPemesanBaru = Console.ReadLine();
        if (!string.IsNullOrEmpty(namaPemesanBaru))
        {
            jadwal.NamaPemesan = namaPemesanBaru;
        }

        Console.Write($"Nomor Telepon (sebelumnya: {jadwal.NomorTelepon}): ");
        string nomorTeleponBaru = Console.ReadLine();
        if (!string.IsNullOrEmpty(nomorTeleponBaru))
        {
            jadwal.NomorTelepon = nomorTeleponBaru;
        }

        Console.Write($"Tanggal Pesan (ddMMyyyy) (sebelumnya: {jadwal.TanggalPesan}): ");
        string tanggalInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(tanggalInput))
        {
            jadwal.TanggalPesan = DateTime.ParseExact(tanggalInput, "ddMMyyyy", CultureInfo.InvariantCulture)
                                       .ToString("dd-MM-yyyy");
        }

        Console.Write($"Waktu (HHmm) (sebelumnya: {jadwal.Waktu}): ");
        string waktuInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(waktuInput))
        {
            jadwal.Waktu = DateTime.ParseExact(waktuInput, "HHmm", CultureInfo.InvariantCulture)
                                  .ToString("HH:mm");
        }

        Console.Write($"Nomor Lapangan (sebelumnya: {jadwal.NomorLapangan}): ");
        string lapanganBaru = Console.ReadLine();
        if (!string.IsNullOrEmpty(lapanganBaru))
        {
            jadwal.NomorLapangan = lapanganBaru;
        }

        Console.Write($"Durasi Sewa (sebelumnya: {jadwal.Durasi}): ");
        string durasiBaruStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(durasiBaruStr))
        {
            int durasiBaru = int.Parse(durasiBaruStr);
            jadwal.Durasi = durasiBaru;
            jadwal.Harga = hitungHarga(durasiBaru); // Re-calculate harga
        }

        Console.WriteLine("\nJadwal berhasil di-edit!");

        // Simpan perubahan ke CSV
        SaveToCSV();

        menuAdmin();

    }
    public static void HapusJadwal()
    {
        TampilkanJadwal();
        if (listJadwal.Count == 0)
        {
            menuAdmin();
            return;
        }

        Console.Write("\nMasukkan nomor jadwal yang ingin dihapus: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        if (index < 0 || index >= listJadwal.Count)
        {
            Console.WriteLine("Nomor jadwal tidak valid!");
            menuAdmin();
            return;
        }

        listJadwal.RemoveAt(index);
        Console.WriteLine("Jadwal berhasil dihapus!");

        // Simpan perubahan ke CSV
        SaveToCSV();

        menuAdmin();
    }

    // static void menuPenyewa()
    // {

    // }

    static double hitungHarga(int durasi)
    {
        double hargaPerJam = 10000;


        double totalHarga = durasi * hargaPerJam;

        return totalHarga;

    }

    static void SaveToCSV()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(csvFileName))
            {
                // Tulis header (bisa disesuaikan)
                writer.WriteLine("NamaPemesan,NomorTelepon,TanggalPesan,Waktu,NomorLapangan,Durasi,Harga");

                // Tulis setiap baris data
                foreach (var j in listJadwal)
                {
                    writer.WriteLine($"{j.NamaPemesan},{j.NomorTelepon},{j.TanggalPesan},{j.Waktu},{j.NomorLapangan},{j.Durasi},{j.Harga}");
                }
            }

            Console.WriteLine("\n[INFO] Data jadwal berhasil disimpan ke CSV!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Gagal menyimpan file CSV: " + ex.Message);
        }
    }
    static void LoadFromCSV()
    {
        if (!File.Exists(csvFileName))
        {
            // Kalau file tidak ada, skip saja
            Console.WriteLine("[INFO] File CSV belum ada, lanjut tanpa load.");
            return;
        }

        try
        {
            using (StreamReader reader = new StreamReader(csvFileName))
            {
                // Baca header
                string headerLine = reader.ReadLine();  
                // Pastikan header tidak kosong
                if (string.IsNullOrEmpty(headerLine))
                {
                    Console.WriteLine("[INFO] File CSV kosong, tidak ada data untuk di-load.");
                    return;
                }

                // Bersihkan isi listJadwal agar data tidak menumpuk
                listJadwal.Clear();

                // Baca baris-baris berikutnya (data)
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) 
                        continue; // skip baris kosong

                    string[] values = line.Split(',');
                    if (values.Length < 7)
                    {
                        // Format CSV tidak sesuai, skip baris ini
                        continue;
                    }

                    // Buat object Jadwal dari data CSV
                    string namaPemesan    = values[0];
                    string nomorTelepon   = values[1];
                    string tanggalPesan   = values[2];
                    string waktu          = values[3];
                    string nomorLapangan  = values[4];
                    int durasi            = int.Parse(values[5]);
                    double harga          = double.Parse(values[6]);

                    Jadwal jadwal = new Jadwal(namaPemesan, nomorTelepon, tanggalPesan,
                                               waktu, nomorLapangan, durasi, harga);

                    // Masukkan ke list
                    listJadwal.Add(jadwal);
                }
            }

            Console.WriteLine("[INFO] Data jadwal berhasil di-load dari CSV!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Gagal memuat file CSV: " + ex.Message);
        }
    }



    public static void Main(string[] args)
    {

        Console.WriteLine("Selamat Datang di Penyewaan Lapangan Futsal");
        LoadFromCSV();
        Login();
    }
}
