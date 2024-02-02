namespace kektura_struct
{
    public struct Section
    {
        public string kiindulopont;
        public string vegpont;
        public double hossz;
        public int emelkedes;
        public int lejtes;
        public char pecsetelohely;

        public bool IsIncompleteName()
        {
            return !this.vegpont.Contains("pecsetelohely") && this.pecsetelohely == 'i';
        }

        public int CalculateStatus()
        {
            return this.emelkedes - this.lejtes;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /*   bool IsIncompleteName(Section row)
            {
                return !row.vegpont.Contains("pecsetelohely") && row.pecsetelohely == 'i';
            }*/
            string[] textF = File.ReadAllLines("kektura.csv");
            List<Section> sections = new List<Section>();
            for (int i = 1; i < textF.Length; i++)
            {
                Section line = new Section();
                line.kiindulopont = textF[i].Trim().Split(';')[0];
                line.vegpont = textF[i].Trim().Split(';')[1];
                line.hossz = Convert.ToDouble(textF[i].Trim().Split(';')[2]);
                line.emelkedes = Convert.ToInt32(textF[i].Trim().Split(';')[3]);
                line.lejtes = Convert.ToInt32(textF[i].Trim().Split(';')[4]);
                line.pecsetelohely = Convert.ToChar(textF[i].Trim().Split(';')[5]);

                sections.Add(line);
            }
            double count = 0;
            foreach (var item in sections)
            {
                count += item.hossz;
            }
            Console.WriteLine($"Az összes túra hossza: {count:f2} km.");
            double shortest = double.MaxValue;
            string startPoint = "";
            foreach (var item in sections)
            {
                if (shortest > item.hossz)
                {
                    shortest = item.hossz;
                    startPoint = item.kiindulopont;
                }
            }
            Console.WriteLine($"A legrövidebb szakasz kezdőpontja: {startPoint} és hossza: {shortest:f2} km.");

            foreach (var item in sections)
            {
                if (item.IsIncompleteName())
                {
                    Console.WriteLine($"\t{item.vegpont}");
                }
            }
            //A túra legmagasabb végpontja meg tengerszintfeletti magassága
            int currentHeight = 192;
            int max = int.MinValue;
            string endPoint = "";
            foreach (var item in sections)
            {
                int status = item.CalculateStatus();
                currentHeight += status;
                if (currentHeight > max)
                {
                    max = currentHeight;
                    endPoint = item.vegpont;
                }
            }
            Console.WriteLine($"A legmagsabb végpont: {endPoint} és magassága: {max} m.");
        }
    }
}
