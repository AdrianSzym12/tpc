using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace SkanerTCP
{
    class Program
    {
        private static string IP = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Write("Podaj zakres od: ");
            int poczatek = int.Parse(Console.ReadLine());
            Console.Write("Do: ");
            int koniec = int.Parse(Console.ReadLine());
            int bufor= ((koniec - poczatek) / 4 );
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < 4; i++)
            {
                Thread thr1 = new Thread(() => skan(poczatek+i*bufor, bufor + poczatek+i*bufor));
                thr1.Start();
                Thread.Sleep(111);
               
            }

            stopwatch.Stop();

            Console.WriteLine("Czas zakończnia procesu: {0} ms", stopwatch.ElapsedMilliseconds);


           


        }
        static void skan(int poczatek,int koniec)
        {
            Stopwatch stopwatch2 = new Stopwatch();
            Console.WriteLine("Początek= "+poczatek + " " + koniec);
            for (int i = poczatek; i <= koniec; i++)
            {
                stopwatch2.Start();
                using (TcpClient Scan = new TcpClient())
                {

                    try
                    {
                        Scan.Connect(IP, i);
                        Console.WriteLine($"[{i}] | Aktywny");
                        stopwatch2.Stop();
                        Console.WriteLine("Czas: {0} ms", stopwatch2.ElapsedMilliseconds);
                    }
                    catch
                    {
                        Console.WriteLine($"[{i}] | Nie aktywny");
                        stopwatch2.Stop();
                        Console.WriteLine("Czas: {0} ms", stopwatch2.ElapsedMilliseconds);
                    }
                    
                }
               
            }
           
        }
    }
}
