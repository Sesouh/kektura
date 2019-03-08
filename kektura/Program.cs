using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace kektura
{
    class Program
    {
        static void Main(string[] args)
        {
            double tura_hossza = 0;
            double legrovidebb_szakasz = 0;
            int legrovidebb_szakasz_index = -1;
            string[] lines;
            var list = new List<string>();
            var fileStream = new FileStream("kektura.csv", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                int i = -1;
                while ((line = streamReader.ReadLine()) != null)
                {
                    
                    list.Add(line);
                    if (line.Split(';').Length > 2)
                    {
                        if (legrovidebb_szakasz == 0) legrovidebb_szakasz = Convert.ToDouble(line.Split(';')[2]);
                        i++;
                        double szakasz = Convert.ToDouble(line.Split(';')[2]);
                        tura_hossza += szakasz;
                        if (szakasz <= legrovidebb_szakasz)
                        {
                            legrovidebb_szakasz = szakasz;
                            legrovidebb_szakasz_index = i;
                        }

                        //Console.WriteLine(HianyosNev(line));

                    }

                }
            }
            lines = list.ToArray();
            Console.WriteLine("3. feladat: Szakaszok száma: "+(list.Count - 1)+" db");
            Console.WriteLine("4. feladat: A túra teljes hossza: " + tura_hossza + " km");
            Console.WriteLine(@"5. feladat: A legrövidebb szakasz adatai: 
        Kezdete: {0}
        Vége: {1}
        Távolság: {2} km", list[legrovidebb_szakasz_index+1].Split(';')[0],
                        list[legrovidebb_szakasz_index+1].Split(';')[1],
                        list[legrovidebb_szakasz_index+1].Split(';')[2]);
            Console.WriteLine("7. feladat: Hiányos állomás nevek: ");
            bool van_e_hianyos_nevu = false;
            for (int i = 0; i < list.Count; i++)
            {
                
                
                if (lines[i].Split(';').Length > 2)
                {
                    if (HianyosNev(lines[i]))
                    { 
                        van_e_hianyos_nevu = true;
                        Console.WriteLine("\t" + list[i].Split(';')[1]);
                    }
                }
            }
            if (!van_e_hianyos_nevu) Console.WriteLine("Nincs hiányos állomásnév!");
            Vegpont_magassaga(list);
            Console.ReadKey();

        }

        static public bool HianyosNev(string sor)
        {
            if (!(Convert.ToString(sor.Split(';')[1]).Contains("pecsetelohely")) && Convert.ToString(sor.Split(';')[5]).Equals("i")) return true;
            return false;
        }

        static public void Vegpont_magassaga(List<string> list)
        {
            double vegpont_magassag = 0;
            int vegpont_index = -1;
            double aktualis_magassag = Convert.ToDouble(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                aktualis_magassag += Convert.ToDouble(list[i].Split(';')[3]);
                aktualis_magassag -= Convert.ToDouble(list[i].Split(';')[4]);

                if (vegpont_magassag == 0)
                {
                    vegpont_magassag = aktualis_magassag;
                    vegpont_index = i;
                }
                if (vegpont_magassag < aktualis_magassag)
                {
                    vegpont_magassag = aktualis_magassag;
                    vegpont_index = i;
                }
                
            }
            //Console.WriteLine(vegpont_magassag);
            //Console.WriteLine(vegpont_index);
            Console.WriteLine(@"8. feladat: A legrövidebb szakasz adatai: 
        A végpont neve: {0}
        A végpont tengerszint feletti magassága: {1} m", list[vegpont_index].Split(';')[1],
                                                        vegpont_magassag);
        }
    }
}
