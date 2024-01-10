using System;
using System.Diagnostics;

namespace KomplexFeladat
{
    public class MinTarolo
    {
        public readonly int[] minErt;
        public readonly int[] minInd;

        public MinTarolo(int[] minErt, int[] minInd)
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

        static int MinIndexTomb(ref int[,]? ADAT,int nap,int N)
        {
            int minErtek = ADAT[0,nap];
            for(int i = 0; i<N; i++)
            {
                if (minErtek > ADAT[i,nap])
                {
                    minErtek = ADAT[i, nap];
                }
            }

            return minErtek;
        }
      
        static MinTarolo MinKeres(int nap,int N, ref int[,]? ADAT)
        {
            int db = 0;
            int[] indexek = NemNullaTomb(N);
            int[] ertekek = new int[N];

            int minErtek = MinIndexTomb(ref ADAT, nap, N);
            for(int i = 0; i < N; i++)
            {
                if (ADAT[i,nap] <= minErtek)
                {
                    if(!Tartalmaz(i, indexek))
                    {
                        indexek[db] = i;
                        ertekek[db] = ADAT[i, nap];
                        db++;
                    }
                }
            }
            int[] kimenetIndex = new int[db];
            int[] kimenetErteke = new int[db];
            for(int i = 0; i < db; i++)
            {
                kimenetIndex[i] = indexek[i];
                kimenetErteke[i] = ertekek[i];
            }
            BubbleSort(ref kimenetIndex);
            BubbleSort(ref kimenetErteke);
            MinTarolo minTarolo = new MinTarolo(kimenetErteke, kimenetIndex);
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
                for(int j = 0; j < minTarolo.minInd.Length; j++)
                {
                    if (!Tartalmaz(minTarolo.minInd[j], telepulesek))
                    {
                        telepulesek[db] = minTarolo.minInd[j];
                        db++;
                    }
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
        static void Main()
        {
            string? inputLine = Console.ReadLine();
            string[] numbers = inputLine.Split(' ');
            int N = int.Parse(numbers[0]);
            int M = int.Parse(numbers[1]);

            int[,]? ADAT = Beolvas(N,M);
            EredmenyTarolo outputContainer = NaponkentiValogatas(N, M, ref ADAT);
            EredmenyKiiras(outputContainer);

        }

    }
}