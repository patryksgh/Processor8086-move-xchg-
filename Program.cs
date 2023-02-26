using Microsoft.Win32;
using System;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace procesor8086
{
    class Program
    {
        static void Main(string[] args)
        {
            //Każda wartość odpowiada odpowiedniemu rejestrowi AX, BX, CX, DX
            string[] wartosci = { "", "", "", "" };
            string[] rejestry = { "AX", "BX", "CX", "DX" };

            PierwszeWlaczenie();

            WprowadzanieWartosci(ref wartosci, rejestry);

            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("SYMULATOR POLECEN DLA PROCESORA\n");
                System.Threading.Thread.Sleep(500);
                WyswietlWartosci(wartosci, rejestry);

                Console.WriteLine("\nWybierz polecenie: ");
                Console.WriteLine("1 - MOV");
                Console.WriteLine("2 - XCHG");
                Console.WriteLine("3 - ZMIEN WARTOSCI REJESTRU");
                Console.WriteLine("4 - AUTOR");
                Console.WriteLine("5 - ZAKONCZ PRACE SYMULATORA");


                char polecenie = Console.ReadKey().KeyChar;

                switch (polecenie)
                {
                    case '1':
                        PolecenieMov(ref wartosci, rejestry);
                        break;
                    case '2':
                        PolecenieXCHG(ref wartosci, rejestry);
                        break;
                    case '3':
                        ZmianaRejestru(ref wartosci, rejestry);
                        break;
                    case '4':
                        Autor();
                        break;
                    case '5':

                        return;
                    default:
                        Console.WriteLine("Niepoprawne polecenie!");
                        break;
                }
            }
        }

        public static void WprowadzanieWartosci(ref string[] wartosci, string[] rejestry)
        {
            for (int i = 0; i < rejestry.Length; i++)
            {
                for (; ; )
                {
                    Console.WriteLine("Podaj wartość rejestru " + rejestry[i] + ":");
                    Console.WriteLine("(Tylko w systemie szesnastkowym i nie więcej niż 4 znaki)");
                    wartosci[i] = Console.ReadLine();

                    if (CzyHex(wartosci[i]))
                    {
                        break;
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("To nie jest liczba w systemie szesnastkowym! \n Spróbuj ponownie");
                    }
                }

            }
        }

        public static void ZmianaRejestru(ref string[] wart, string[] reje)
        {
            Console.Clear();
            WyswietlWartosci(wart, reje);

            Console.WriteLine("\nKtora wartosc rejestru chcesz zmienić: ");
            for (int i = 0; i < reje.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + reje[i]);
            }

            Console.WriteLine("Prosze wybrac rejestr");

            char wybor = Console.ReadKey().KeyChar;
            int adres1 = int.Parse(wybor.ToString());
            adres1--;

            Console.Clear();

            for (; ; )
            {
                Console.WriteLine("Podaj wartość rejestru " + reje[adres1] + ":");
                Console.WriteLine("(Tylko w systemie szesnastkowym i nie więcej niż 4 znaki)");
                wart[adres1] = Console.ReadLine();

                if (CzyHex(wart[adres1]))
                {
                    break;
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("To nie jest liczba w systemie szesnastkowym! \n Spróbuj ponownie");
                }
            }
            Console.Clear();
        }

        public static void PolecenieXCHG(ref string[] wart, string[] reje)
        {
            Console.Clear();
            WyswietlWartosci(wart, reje);

            Console.WriteLine("\nKtora wartosc chcesz zamienić: ");
            for (int i = 0; i < reje.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + reje[i]);
            }

            Console.WriteLine("Prosze wybrac rejestr");

            char wybor = Console.ReadKey().KeyChar;
            int adres1 = int.Parse(wybor.ToString());
            adres1--;


            Console.Clear();
            WyswietlWartosci(wart, reje);

            Console.WriteLine("\nZ którym rejestrem chcesz zamienić rejestr " + reje[adres1] + " :");
            for (int i = 0; i < reje.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + reje[i]);
            }

            Console.WriteLine("Prosze wybrac rejestr");

            wybor = Console.ReadKey().KeyChar;
            int adres2 = int.Parse(wybor.ToString());
            adres2--;
            string rejestrTymczasowy;
            rejestrTymczasowy = wart[adres2];
            wart[adres2] = wart[adres1];
            wart[adres1] = rejestrTymczasowy;
            Console.Clear();
            Console.WriteLine("Pomyslnie zamieniono wartosc rejestru " + reje[adres1] + " z wartoscia rejestru " + reje[adres2]);
            System.Threading.Thread.Sleep(500);
            Console.Clear();

        }


        public static void PolecenieMov(ref string[] wart, string[] reje)
        {
            Console.Clear();
            WyswietlWartosci(wart, reje);

            Console.WriteLine("\nKtora wartosc chcesz przeniesc: ");
            for (int i = 0; i < reje.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + reje[i]);
            }

            Console.WriteLine("Prosze wybrac rejestr");

            char wybor = Console.ReadKey().KeyChar;
            int adres1 = int.Parse(wybor.ToString());
            adres1--;


            Console.Clear();
            WyswietlWartosci(wart, reje);

            Console.WriteLine("\nDo ktorego rejestru chcesz przeniesc rejestr " + reje[adres1] + " :");
            for (int i = 0; i < reje.Length; i++)
            {
                Console.WriteLine(i + 1 + " " + reje[i]);
            }

            Console.WriteLine("Prosze wybrac rejestr");

            wybor = Console.ReadKey().KeyChar;
            int adres2 = int.Parse(wybor.ToString());
            adres2--;
            wart[adres2] = wart[adres1];
            Console.Clear();
            Console.WriteLine("Pomyslnie przeniesiono wartosc rejestru " + reje[adres1] + " do rejestru " + reje[adres2]);
            System.Threading.Thread.Sleep(500);
            Console.Clear();




        }

        public static void WyswietlWartosci(string[] wart, string[] reje)
        {
            Console.WriteLine("Wartosci rejestrów: \n");
            for (int i = 0; i < reje.Length; i++)
            {
                Console.WriteLine(reje[i] + " = " + wart[i]);
            }
        }
        public static bool CzyHex(string data)
        {
            return Regex.IsMatch(data, @"\A\b[0-9a-fA-F]{1,4}\b\Z");
        }

        public static void PierwszeWlaczenie()
        {
            Console.WriteLine("Witaj w symulatorze poleceń procesora");
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Na początku przypisz wartości do rejestrów AX, BX, CX, DX. \nWartości mogą być wpisane tylko w systemie szesnastkowym!");
            System.Threading.Thread.Sleep(500);
            Console.Clear();
        }

        public static void Autor()
        {
            Console.Clear();
            Console.WriteLine("Patryk Lichoń");
            Console.WriteLine("Indeks: 14735");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
