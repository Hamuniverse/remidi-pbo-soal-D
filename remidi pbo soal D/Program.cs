using System;
using System.Collections.Generic;
using System.Linq;

class Nasabah
{
    public string NomorRekening { get; private set; }
    public string Nama { get; private set; }
    public decimal Saldo { get; private set; }

    public Nasabah(string nomorRekening, string nama, decimal saldoAwal)
    {
        NomorRekening = nomorRekening;
        Nama = nama;
        Saldo = saldoAwal;
    }

    public void TampilInfo()
    {
        Console.WriteLine($"Nomor Rekening: {NomorRekening} | Nama: {Nama} | Saldo: Rp{Saldo:N0}");
    }

    public bool TarikUang(decimal jumlah)
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("Masukkan jumlah penarikan yang benar.");
            return false;
        }

        if (Saldo >= jumlah)
        {
            Saldo -= jumlah;
            Console.WriteLine($"Penarikan berhasil. Sisa saldo anda adalah: Rp{Saldo:N0}");
            return true;
        }
        else
        {
            Console.WriteLine("Saldo anda tidak mencukupi.");
            return false;
        }
    }

    public void SetorUang(decimal jumlah)
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("Jumlah setor harus lebih dari 0.");
            return;
        }

        Saldo += jumlah;
        Console.WriteLine($"Setoran berhasil. Saldo anda sekarang: Rp{Saldo:N0}");
    }

    public bool Transfer(Nasabah tujuan, decimal jumlah)
    {
        if (jumlah <= 0)
        {
            Console.WriteLine("Masukkan jumlah transfer yang benar.");
            return false;
        }

        if (Saldo >= jumlah)
        {
            Saldo -= jumlah;
            tujuan.Saldo += jumlah;
            Console.WriteLine($"Transfer berhasil ke {tujuan.Nama} (Rek: {tujuan.NomorRekening}).");
            return true;
        }
        else
        {
            Console.WriteLine("Maaf saldo anda tidak mencukupi.");
            return false;
        }
    }
}

class Program
{
    static List<Nasabah> daftarNasabah = new List<Nasabah>();

    static void Main()
    {
        // Data awal
        daftarNasabah.Add(new Nasabah("2046", "Ilham", 1000000));
        daftarNasabah.Add(new Nasabah("1010", "Fajri", 5000000));

        while (true)
        {
            Console.WriteLine("\n----- BANK PELITA DIGITAL -----");
            Console.Write("Masukkan Nomor Rekening: ");
            string noRek = Console.ReadLine();

            var nasabah = daftarNasabah.FirstOrDefault(n => n.NomorRekening == noRek);

            if (nasabah == null)
            {
                Console.WriteLine("Nomor rekening tidak ditemukan.");
                continue;
            }

            bool login = true;
            while (login)
            {
                Console.WriteLine($"\nSelamat datang, {nasabah.Nama} di Bank Pelita Digital!");
                Console.WriteLine("1. Lihat Data Rekening");
                Console.WriteLine("2. Tarik Tunai");
                Console.WriteLine("3. Setor Tunai");
                Console.WriteLine("4. Transfer Antar Rekening");
                Console.WriteLine("5. Keluar");
                Console.Write("Pilih menu: ");
                string pilih = Console.ReadLine();

                switch (pilih)
                {
                    case "1":
                        nasabah.TampilInfo();
                        break;

                    case "2":
                        Console.Write("Masukkan jumlah uang yang ingin di tarik: Rp");
                        decimal tarik = decimal.Parse(Console.ReadLine());
                        nasabah.TarikUang(tarik);
                        break;

                    case "3":
                        Console.Write("Masukkan jumlah uang yang ingin di setor: Rp");
                        decimal setor = decimal.Parse(Console.ReadLine());
                        nasabah.SetorUang(setor);
                        break;

                    case "4":
                        Console.Write("Masukkan nomor rekening tujuan: ");
                        string noTujuan = Console.ReadLine();
                        var penerima = daftarNasabah.FirstOrDefault(n => n.NomorRekening == noTujuan);

                        if (penerima == null)
                        {
                            Console.WriteLine("Rekening tujuan tidak ditemukan.");
                            break;
                        }

                        Console.Write("Masukkan jumlah yang ingin di transfer: Rp");
                        decimal jumlahTransfer = decimal.Parse(Console.ReadLine());
                        nasabah.Transfer(penerima, jumlahTransfer);
                        break;

                    case "5":
                        login = false;
                        Console.WriteLine("--- TERIMA KASIH --- \n");
                        break;

                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }
    }
}