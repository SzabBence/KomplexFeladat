using System;
using System.Diagnostics;

namespace KomplexFeladat
{
    public class MinTarolo
    {
        public readonly int minErt;
        public readonly int minInd;

        public MinTarolo(int minErt, int minInd)
        {
            this.minErt = minErt;
            this.minInd = minInd;
        }
    }

    public class EredmenyTarolo
    {
        public readonly int db;
        public readonly int[] telepulesIndexek;

        public EredmenyTarolo(int db, int[] telepulesIndexek)
        {
            this.db = db;
            this.telepulesIndexek = telepulesIndexek;
        }
    }
    internal class Program
    {
        static int[,]? Beolvas(int N, int M)
        {
            string? inputLine;
            int[,]? ADAT = new int[N, M];
            for (int i = 0; i < N; i++)
            {
                inputLine = Console.ReadLine();
                string[] numbers = inputLine.Split(' ');
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (numbers[j] != "")
                    {
                        ADAT[i, j] = int.Parse(numbers[j]);
                    }
                }
            }
            return ADAT;
        }


        static bool Tartalmaz(int index, int[] telepulesek)
        {
            bool folytat = false;
            int hossz = telepulesek.Length;
            int i = 0;
            while (!folytat && i<hossz)
            {
                if(index == telepulesek[i])
                {
                    folytat = true;
                }
                i++;
            }
            return folytat;
        }

        static MinTarolo MinKeres(int nap,int N, ref int[,]? ADAT)
        {
            int minErtek = ADAT[0, nap];
            int minIndex = 0;

            for(int i = 1; i < N; i++)
            {
                if (ADAT[i,nap] < minErtek)
                {
                    minErtek = ADAT[i, nap];
                    minIndex = i;
                }
            }

            MinTarolo minTarolo = new MinTarolo(minErtek, minIndex);
            return minTarolo;
        }

        static int[] Masol(int[] telepulesek, int db)
        {
            int[] masolat = new int[db];
            int j = 0;
            for(int i = 0; i<db; i++)
            {
                if (telepulesek[i] != -1)
                {
                    masolat[j] = telepulesek[i];
                    j++;
                }
            }
           
            return masolat;
        }
        static void BubbleSort(ref int[] telepulesek)
        {
            int hossz = telepulesek.Length;

            for (int i = 0; i < hossz - 1; i++)
            {
                for (int j = 0; j < hossz - i - 1; j++)
                {
                    if (telepulesek[j] > telepulesek[j + 1])
                    {
                        int tempVar = telepulesek[j];
                        telepulesek[j] = telepulesek[j + 1];
                        telepulesek[j + 1] = tempVar;
                    }
                }
            }
        }
        static int[] NemNullaTomb(int N)
        {
            int[] tomb = new int[N]; 

            for (int i = 0; i < tomb.Length; i++)
            {
                tomb[i] = -1; 
            }

            return tomb;
        }

        static void IndexHelyreIgazit(ref int[] tomb)
        {
            int n = tomb.Length;
            for (int i = 0; i < n; i++)
            {
                tomb[i]++;
            }
        }
        static EredmenyTarolo NaponkentiValogatas(int N, int M, ref int[,]? ADAT)
        {
           
            int[] telepulesek = NemNullaTomb(N);
            int db = 0;
            for(int i = 0; i < M; i++)
            {
                MinTarolo minTarolo = MinKeres(i, N,ref ADAT);
                if(!Tartalmaz(minTarolo.minInd, telepulesek))
                {
                    telepulesek[db] = minTarolo.minInd;
                    db++;
                }
            }
            
            int[] indexKorrigalt = Masol(telepulesek, db);
            BubbleSort(ref indexKorrigalt);
            IndexHelyreIgazit(ref indexKorrigalt);
            EredmenyTarolo outputContainer = new EredmenyTarolo(db, indexKorrigalt);
            return outputContainer;

        }

        static void EredmenyKiiras(EredmenyTarolo outputContainer)
        {
            Console.Write(outputContainer.db + " ");
            for(int i=0; i<outputContainer.telepulesIndexek.Length; i++)
            {
                Console.Write(outputContainer.telepulesIndexek[i] + " ");
            }
        }
        static void TestKiir(ref int[,]? ADAT,int N, int M)
        {
            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < M; j++)
                {
                    Console.Write(ADAT[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void DebugArray(ref int[] arr)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                Console.Write(arr[j] + " ");
            }
        }

        static void Logic(int N, int M, int[,] data)
        {
            for(int i = 0; i<N; i++)
            {
                MinTarolo res = MinKeres(i,N,ref data);
                Console.WriteLine($"{i},{res.minInd},{res.minErt}");
            }
        }

        static voidNapiData(int N, int M, int[,] data)
        {

        }
        static void Main()
        {
            string? inputLine = Console.ReadLine();
            string[] numbers = inputLine.Split(' ');
            int N = int.Parse(numbers[0]);
            int M = int.Parse(numbers[1]);

            int[,]? ADAT = Beolvas(N,M);
            //Logic(N,M,ADAT);
            EredmenyTarolo outputContainer = NaponkentiValogatas(N, M, ref ADAT);
            EredmenyKiiras(outputContainer);

        }

    }
}