using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Reflection;

public class Jadwal // ada kelas namanya Jadwal, kelas itu blueprint/rancangan untuk membangun sebuah objek (rumah) "Jadwal" nanti
{
    // bagian dibawah ini adalah properti/atribut (detail dari sebuah blueprint) untuk kelas jadwal
    // mirip kayak variabel, tapi ini versi penyimpanan yg lebih aman
    private string namaPemesan, nomorTelepon, tanggalPesan, waktu;
    private int nomorLapangan, durasi;
    private double harga;
    
    // bagian dibawah ini adalah konstuktor (tukang yang membangun rumah) untuk inisialisasi awal objek "jadwal" nanti 
    // konstruktor punya nama sama seperti nama kelasnya
    // tujuan konstruktor adalah untuk memberikan nilai awal properti saat objek Jadwal dibuat.
    public Jadwal(string namaPemesan, string nomorTelepon, string tanggalPesan,
              string waktu, int nomorLapangan, int durasi, double harga) // Tukang (konstruktor) menerima parameter dari klien (misalnya namaPemesan, nomorTelepon, dll.).
    {
        NamaPemesan = namaPemesan; // baris ini mengisi properti NamaPemesan dengan nilai dari parameter namaPemesan yang dikirim saat pembuatan objek.
        NomorTelepon = nomorTelepon;
        TanggalPesan = tanggalPesan;
        Waktu = waktu;
        NomorLapangan = nomorLapangan;// properti NomorLapangan diisi dengan nilai nomorLapangan. contoh nomorLapangan nanti diisi 1
        Durasi = durasi;
        Harga = harga;
    }
    public string NamaPemesan
    {
        get { return namaPemesan; }  // Mengambil nilai
        set
        {
            if (!string.IsNullOrEmpty(value))  // Validasi: Tidak boleh kosong
            {
                namaPemesan = value;
            }
            else
            {
                throw new Exception("Nama pemesan tidak boleh kosong!");
            }
        }
    }
    public string NomorTelepon
    {
        get { return nomorTelepon; }
        set
        {
            if (value.Length >= 10)
            {
                nomorTelepon = value;
            }
            else
            {
                throw new Exception("Nomor telepon minimal 10 digit!");
            }
        }
    }
    public string TanggalPesan
    {
        get { return tanggalPesan; }  // Mengambil nilai
        set
        {
            if (!string.IsNullOrEmpty(value))  // Validasi: Tidak boleh kosong
            {
                tanggalPesan = value;
            }
            else
            {
                throw new Exception("Tanggal Pesan tidak boleh kosong!");
            }
        }
    }
    public string Waktu
    {
        get { return waktu; }
        set
        {
            if (!string.IsNullOrEmpty(value) && value.Length == 5 && value.Contains(":"))
            {
                waktu = value;
            }
            else
            {
                throw new Exception("Format waktu harus HH:mm (Contoh: 14:00)!");
            }
        }
    }
    public int NomorLapangan
    {
        get { return nomorLapangan; }
        set
        {
            if (value > 0)
            {
                nomorLapangan = value;
            }
            else
            {
                throw new Exception("Nomor lapangan harus lebih dari 0!");
            }
        }
    }
    public int Durasi
    {
        get { return durasi; }
        set
        {
            if (value > 0)
            {
                durasi = value;
            }
            else
            {
                throw new Exception("Durasi tidak boleh nol atau negatif!");
            }
        }
    }
    public double Harga
    {
        get { return harga; }
        set
        {
            if (value > 0)
            {
                harga = value;
            }
            else
            {
                throw new Exception("Harga tidak boleh nol atau negatif!");
            }
        }
    }

}

class Program // ada kelas namanya Program, berfungsi sebagai titik awal eksekusi program
{
    // DEKLARASI
    private static string csvFileName = "jadwal.csv";
    private static List<Jadwal> listJadwal = new List<Jadwal>(); // adalah list yang menyimpan objek Jadwal

    /// <summary>
    /// 
    /// private:
    /// 
    /// Akses modifier yang berarti hanya bisa diakses dari dalam kelas Program saja.
    /// Tidak bisa diakses langsung dari luar kelas ini.
    /// 
    /// static:
    /// Milik kelas (bukan objek tertentu).
    /// Field ini berlaku untuk seluruh kelas, bukan instance atau objek yang dibuat dari kelas tersebut.
    /// Semua metode static dalam kelas ini bisa langsung mengakses csvFileName tanpa harus membuat objek Program terlebih dahulu.
    /// 
    /// csvFileName: Menyimpan nama file CSV yang akan digunakan untuk menyimpan atau memuat data.
    /// listJadwal: Menyimpan daftar jadwal yang dikelola oleh program. Data ini bisa ditambah, dihapus, diedit, dan disimpan ke dalam file CSV.
    /// 
    /// </summary>


    static void login() // ada fungsi namanya login, isi programnya dibawah ini
    {
        try // coba eksekusi program ini 
        {
            Console.WriteLine("Silahkan login!"); // tampilkan teks yang dalam "", buat baris sendiri
            Console.Write("Username: "); // tampilkan teks yang dalam "", tidak membuat baris sendiri
            string username = Console.ReadLine(); // baca input dari user

            Console.Write("Password: ");
            string password = Console.ReadLine(); // baca input dari user

            // cek kondisi berdasarkan inputan 2 diatas

            if (username == "admin" && password == "admin") // cek apakah input username adalan "admin" dan password adalah "admin"
            {
                Console.WriteLine("login Berhasil!"); // jika keduanya sesuai maka tampilkan ini
                menuAdmin(); // kemudian panggil method ini
            }
            else if (username == "" && password == "") // jika inputannya ini
            {
                Console.WriteLine("login Berhasil!");
                menuPenyewa(); 
            }
            else // jika inputan tidak sesuai yang didefinisikan diatas maka ekseskusi kode didalam kurung ini
            {
                Console.WriteLine("Username atau Password salah, silahkan coba lagi!");
                saveToCSV(); // panggil method untuk menyimpan file
                login(); // panggil fungsi login
            }
        }
        catch (Exception e) // Jika ada error, program langsung lompat ke blok catch.
        {
            Console.WriteLine($"Terjadi Kesalahan: {e.Message}"); // cetak pesan error
            login(); // memanggil kembali fungsi login() agar pengguna bisa mencoba lagi.
        }

    }

    public static void menuAdmin() // method 
    {
        Console.WriteLine("\n========================= Tabel Penyewa =======================");
        tampilkanJadwal(); // panggil method ini untuk menampilkan jadwal

        Console.WriteLine("\nPilihan:");
        Console.WriteLine("1. Tambah Jadwal");
        Console.WriteLine("2. Edit Jadwal");
        Console.WriteLine("3. Hapus Jadwal");
        Console.WriteLine("4. Cari Berdasarkan Nama");
        Console.WriteLine("5. Filter Jadwal Berdsarkan Tanggal");
        Console.WriteLine("6. Logout");
        Console.Write("Masukkan pilihan: ");
        string pilihan = Console.ReadLine();

        // cek kondisi inputan

        if (pilihan == "1")
        {
            tambahJadwal(); // Panggil method tambahJadwal
        }
        else if (pilihan == "2")
        {
            editJadwal();// Panggil method editJadwal
        }
        else if (pilihan == "3")
        {
            hapusJadwal();// Panggil method hapusJadwal
        }
        else if (pilihan == "4")
        {
            search();// Panggil method search
        }
        else if (pilihan == "5")
        {
            filter();// Panggil method filter
        }
        else if (pilihan == "6")
        {
            Console.WriteLine("Anda telah logout.");
            login();// Panggil fungsi login
        }
        else
        {
            Console.WriteLine("Pilihan tidak valid!");
        }
    }

    /// <summary>
    /// Menampilkan daftar jadwal penyewaan lapangan futsal.  
    /// Jika tidak ada jadwal yang tersimpan, pesan "Belum ada jadwal" akan ditampilkan.  
    /// Jadwal yang ada akan ditampilkan dalam format tabel dengan informasi pemesan, tanggal, waktu, dan detail lapangan.  
    /// </summary>
    public static void tampilkanJadwal()
    {
        Console.WriteLine("No | Nama Pemesan | Nomor Telepon | Tanggal Pesan | Waktu | Lapangan | Durasi | Harga");
        if (listJadwal.Count == 0)
        {
            Console.WriteLine("Belum ada jadwal.");
        }
        else
        {
            for (int i = 0; i < listJadwal.Count; i++) // perulangan akan terus berjalan selama i kurang dari jumlah item dalam listJadwal
            {
                Jadwal j = listJadwal[i]; // mengambil elemen ke-i dari listJadwal dan menyimpannya dalam variabel j bertipe Jadwal
                Console.WriteLine($"{(i + 1)}.  {j.NamaPemesan} | {j.NomorTelepon} | {j.TanggalPesan} | {j.Waktu} | {j.NomorLapangan} | {j.Durasi} | {j.Harga}"); // mengakses atribut Jadwal (NamaPemesan, NomorTelepon, dll.) dari objek j
            }
        }
    }
    public static void tambahJadwal()
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
        int nomorLapangan = int.Parse(Console.ReadLine());

        Console.Write("Durasi Sewa (dalam jam): ");
        int durasi = int.Parse(Console.ReadLine());

        double harga = hitungHarga(durasi);

        Jadwal jadwalBaru = new Jadwal(namaPemesan, nomorTelepon, tanggalPesan,
                                       waktu, nomorLapangan, durasi, harga);
        listJadwal.Add(jadwalBaru);

        saveToCSV();
        return;


    }

    /// <summary>
    /// Mengedit jadwal yang sudah ada di dalam daftar.  
    /// Pengguna memilih jadwal berdasarkan nomor, lalu diperbolehkan untuk mengubah informasi tertentu seperti nama, telepon, tanggal, waktu, lapangan, dan durasi.  
    /// Perubahan jadwal akan disimpan ke CSV dan pengguna diarahkan kembali ke menu admin.  
    /// Jika daftar kosong, pengguna langsung dikembalikan ke menu admin.
    /// </summary>
    public static void editJadwal()
    {
        tampilkanJadwal();
        if (listJadwal.Count == 0)
        {
            menuAdmin();
            return; // mencegah kode di bawahnya untuk dieksekusi jika kondisi if terpenuhi. mengembalikan ke method pemanggilnya
        }

        Console.Write("\nMasukkan nomor jadwal yang ingin diedit: ");
        int index = int.Parse(Console.ReadLine()) - 1; // nilai dikurangi 1, karena list menggunakan indeks berbasis 0
        // kalau pengguna ingin mengedit item nomor 3, sebenarnya itu adalah indeks ke-2 di dalam list (3 - 1 = 2).

        if (index < 0 || index >= listJadwal.Count) // Jika salah satu kondisi benar, maka blok if akan dijalankan.

        {
            Console.WriteLine("Nomor jadwal tidak valid!");
            menuAdmin(); // pengguna langsung dikembalikan ke menu utama admin
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
        string lapanganBaruStr = Console.ReadLine();

        if (!string.IsNullOrEmpty(lapanganBaruStr))
        {
            int lapanganBaru = int.Parse(lapanganBaruStr);  // Konversi setelah validasi
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
        saveToCSV();

        menuAdmin();

    }

    /// <summary>
    /// Menghapus jadwal yang dipilih pengguna dari daftar jadwal yang ada.  
    /// Jadwal dihapus berdasarkan nomor yang dipilih dari daftar yang ditampilkan.  
    /// Setelah penghapusan, perubahan disimpan ke CSV dan pengguna kembali ke menu admin.  
    /// Jika tidak ada jadwal yang tersedia, pengguna langsung kembali ke menu admin.
    /// </summary>
    public static void hapusJadwal()
    {
        tampilkanJadwal();
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
        saveToCSV();

        menuAdmin();
    }

    /// <summary>
    /// Mencari jadwal penyewaan berdasarkan nama pemesan.  
    /// Pengguna diminta memasukkan nama (atau sebagian nama) untuk melakukan pencarian.  
    /// Jadwal yang mengandung kata kunci tersebut akan ditampilkan dalam bentuk tabel.  
    /// Jika tidak ada hasil, pesan pemberitahuan akan ditampilkan.  
    /// Setelah pencarian selesai, pengguna akan diarahkan kembali ke menu admin.  
    /// </summary>
    public static void search()
    {
        Console.Write("Masukkan nama pemesan yang ingin dicari: ");
        string keyword = Console.ReadLine();

        // Filter jadwal berdasarkan nama (mengandung kata kunci)
        List<Jadwal> hasilPencarian = new List<Jadwal>();

        foreach (var jadwal in listJadwal)
        {
            if (jadwal.NamaPemesan.ToLower().Contains(keyword.ToLower()))
            {
                hasilPencarian.Add(jadwal);
            }
        }

        // Cek apakah ada hasil
        if (hasilPencarian.Count == 0)
        {
            Console.WriteLine("Tidak ada jadwal dengan nama pemesan tersebut.");
        }
        else
        {
            Console.WriteLine("No | Nama Pemesan | Nomor Telepon | Tanggal Pesan | Waktu | Lapangan | Durasi | Harga");
            for (int i = 0; i < hasilPencarian.Count; i++)
            {
                Jadwal j = hasilPencarian[i];
                Console.WriteLine($"{i + 1}.  {j.NamaPemesan} | {j.NomorTelepon} | {j.TanggalPesan} | {j.Waktu} | {j.NomorLapangan} | {j.Durasi} | {j.Harga}");
            }
        }
        menuAdmin();
    }

    /// <summary>
    /// Menyaring jadwal penyewaan berdasarkan rentang tanggal yang dimasukkan pengguna.  
    /// Pengguna diminta memasukkan tanggal awal dan tanggal akhir dalam format (dd-MM-yyyy).  
    /// Jadwal yang berada dalam rentang tanggal tersebut akan ditampilkan dalam bentuk tabel.  
    /// Jika tidak ada jadwal yang sesuai, pesan pemberitahuan akan muncul.  
    /// Program akan kembali ke menu admin jika terjadi kesalahan format atau input tanggal tidak valid.  
    /// </summary>
    public static void filter()
    {
        Console.Write("Masukkan tanggal awal (dd-MM-yyyy): ");
        string inputStart = Console.ReadLine();
        Console.Write("Masukkan tanggal akhir (dd-MM-yyyy): ");
        string inputEnd = Console.ReadLine();

        // Validasi input tanggal
        if (!DateTime.TryParseExact(inputStart, "dd-MM-yyyy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tMulai)
         || !DateTime.TryParseExact(inputEnd, "dd-MM-yyyy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tAkhir))
        {
            Console.WriteLine("Format tanggal salah!");
            menuAdmin();
            return;
        }

        // tanggal awal <= tanggal akhir
        if (tMulai > tAkhir)
        {
            Console.WriteLine("Tanggal awal tidak boleh melebihi tanggal akhir!");
            menuAdmin();
            return;
        }

        List<Jadwal> hasilFilter = new List<Jadwal>();

        foreach (var j in listJadwal)
        {
            if (DateTime.TryParseExact(j.TanggalPesan, "dd-MM-yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tJadwal))
            {
                if (tJadwal >= tMulai && tJadwal <= tAkhir)  // Dalam rentang
                {
                    hasilFilter.Add(j);
                }
            }
        }

        if (hasilFilter.Count == 0)
        {
            Console.WriteLine($"Tidak ada jadwal di rentang {inputStart} sampai {inputEnd}");
        }
        else
        {
            Console.WriteLine($"Jadwal di rentang {inputStart} - {inputEnd}:");
            Console.WriteLine("No | Nama Pemesan | Nomor Telepon | Tanggal Pesan | Waktu | Lapangan | Durasi | Harga");
            for (int i = 0; i < hasilFilter.Count; i++)
            {
                Jadwal j = hasilFilter[i];
                Console.WriteLine($"{(i + 1)}. {j.NamaPemesan} | {j.NomorTelepon} | {j.TanggalPesan} | {j.Waktu} | {j.NomorLapangan} | {j.Durasi} | {j.Harga}");
            }
            Console.WriteLine("tekan x untuk kembali");
        }

    }

    /// <summary>
    /// Menghitung total harga penyewaan lapangan berdasarkan durasi (dalam jam).  
    /// Harga per jam adalah 10.000.  
    /// Method ini mengembalikan total harga sebagai nilai double.  
    /// </summary>
    static double hitungHarga(int durasi)

    {
        double hargaPerJam = 10000;


        double totalHarga = durasi * hargaPerJam;

        return totalHarga;

    }

    /// <summary>
    /// Menyimpan seluruh data jadwal ke dalam file CSV.  
    /// Data diambil dari listJadwal dan ditulis ke file dengan format CSV.  
    /// Jika proses penyimpanan berhasil, pesan konfirmasi akan ditampilkan.  
    /// Jika terjadi kesalahan, pesan error akan ditampilkan dengan informasi detail.  
    /// </summary>
    static void saveToCSV()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(csvFileName))
            {

                writer.WriteLine("NamaPemesan,NomorTelepon,TanggalPesan,Waktu,NomorLapangan,Durasi,Harga");


                foreach (var j in listJadwal)
                {
                    writer.WriteLine($"{j.NamaPemesan},{j.NomorTelepon},{j.TanggalPesan},{j.Waktu},{j.NomorLapangan},{j.Durasi},{j.Harga}"); // loop ini mengakses setiap item Jadwal yang tersimpan dalam list listJadwal satu per satu.
                }
            }

            Console.WriteLine("\n[INFO] Data jadwal berhasil disimpan!"); // tanda . untuk memanggil method
        }
        catch (Exception e)
        {
            Console.WriteLine("Gagal menyimpan file: " + e.Message);
        }
    }

    /// <summary>
    /// Membaca dan memuat data jadwal dari file CSV ke dalam listJadwal.  
    /// Jika file CSV tidak ditemukan atau kosong, proses loading akan dilewati.  
    /// Data yang berhasil dibaca akan diubah menjadi objek Jadwal dan ditambahkan ke list.  
    /// Setiap baris dalam file CSV dikonversi menjadi objek Jadwal dan ditambahkan ke dalam listJadwal
    /// Jika terjadi error saat proses pembacaan, pesan error akan ditampilkan.  
    /// </summary>
    static void loadFromCSV()
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
                // read header
                string headerLine = reader.ReadLine();
                // memastikan header tidak kosong
                if (string.IsNullOrEmpty(headerLine))
                {
                    Console.WriteLine("[INFO] File CSV kosong, tidak ada data untuk di-load.");
                    return;
                }

                // membersihkan isi listJadwal agar data tidak menumpuk
                listJadwal.Clear();

                // membaca baris-baris berikutnya (data)
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // skip baris kosong

                    string[] values = line.Split(',');
                    if (values.Length < 7)
                    {

                        continue;
                    }

                    // membuat object Jadwal dari data CSV
                    string namaPemesan = values[0];
                    string nomorTelepon = values[1];
                    string tanggalPesan = values[2];
                    string waktu = values[3];
                    int nomorLapangan = int.Parse(values[4]);
                    int durasi = int.Parse(values[5]);
                    double harga = double.Parse(values[6]);

                    Jadwal jadwal = new Jadwal(namaPemesan, nomorTelepon, tanggalPesan,
                                               waktu, nomorLapangan, durasi, harga);

                    // Masukkan ke list
                    listJadwal.Add(jadwal); //menyimpan objek jadwal ke dalam list listJadwal
                }
            }

            Console.WriteLine("[INFO] Data jadwal berhasil di-load dari CSV!");
        }
        catch (Exception e)
        {
            Console.WriteLine("Gagal memuat file CSV: " + e.Message);
        }
    }

// user gak bisa edit dan hapus
    static void menuPenyewa()
    {
        Console.WriteLine("\n========================= Tabel Penyewa =======================");
        tampilkanJadwal(); // panggil method ini untuk menampilkan jadwal

        Console.WriteLine("\nPilihan:");
        Console.WriteLine("1. Daftar");
        Console.WriteLine("2. Logout");
        Console.Write("Masukkan pilihan: ");
        int pilihan = int.Parse(Console.ReadLine());

        // cek kondisi inputan
        switch (pilihan)
        {
            case 1:
            tambahJadwal();
            break;
            
            case 2:
            login();
            break;

        }

    }
    // static void daftar()
    // {
        
    // }



    public static void Main(string[] args)
    {

        Console.WriteLine("Selamat Datang di Penyewaan Lapangan Futsal");
        loadFromCSV();
        login();
    }
}
