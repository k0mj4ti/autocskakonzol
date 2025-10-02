namespace autocskakonzol
{
    internal class Program
    {
        public static ServerConnection connection;

        static void Main(string[] args)
        {
            connection = new ServerConnection("http://localhost:3000");

            while (true)
            {
                ManageMenu();
            }
        }

        static void ManageMenu()
        {
            WriteMenu();

            int num = GetNumber(1, 10);
            SwitchMenu(num).Wait();
            Console.WriteLine("\nNyomd meg egy gombot a folytatáshoz...");
            Console.ReadKey();
        }

        static void WriteMenu()
        {
            Console.Clear();
            Console.WriteLine("Főmenü");
            Console.WriteLine("1. Car listázás");
            Console.WriteLine("2. Új Car");
            Console.WriteLine("3. Car törlés");
            Console.WriteLine("4. Manufacturer listázás");
            Console.WriteLine("5. Új Manufacturer");
            Console.WriteLine("6. Manufacturer törlése");
            Console.WriteLine("7. Owner listázás");
            Console.WriteLine("8. Új owner");
            Console.WriteLine("9. owner törlése");
            Console.WriteLine("10. Kilépés\n");
        }

        static int GetNumber(int min = int.MinValue, int max = int.MaxValue, string text = "Írj be egy számocskát: ")
        {
            Console.Write(text);
            string line = Console.ReadLine().Trim(' ', ',', '.');


            if (int.TryParse(line, out int result))
            {
                if (result >= min && result <= max)
                    return result;


                Console.WriteLine("Hopp, ez a szám nem jó. ");
            }
            else
            {
                Console.WriteLine("Ez nem szám, próbáld újra.");

            }

            return GetNumber(min, max, text);
        }

        static async Task SwitchMenu(int num)
        {
            switch (num)
            {
                case 1: await ListCars(); break;
                case 2: await AddCar(); break;
                case 3: await DeleteCar(); break;
                case 4: await ListManufacturers(); break;
                case 5: await AddManufacturer(); break;
                case 6: await DeleteManufacturer(); break;
                case 7: await ListOwners(); break;
                case 8: await AddOwner(); break;
                case 9: await DeleteOwner(); break;
                case 10: Environment.Exit(0); break;

                default: Console.WriteLine("Rossz választás, próbáld újra."); break;
            }
        }

        static async Task ListCars()
        {
            var cars = await connection.GetCars();
            foreach (var c in cars)
                Console.WriteLine($"{c.id}: {c.modell} ({c.manufacturerYear}) - {c.performance}LE, kerek: {c.wheelSize}\"");
        }

        static async Task AddCar()
        {
            Console.Write("Manufacturer id: ");
            string makeId = Console.ReadLine();
            Console.Write("Modell neve: ");
            string model = Console.ReadLine();
            int perf = GetNumber(1, int.MaxValue, "Teljesítmény: ");
            int year = GetNumber(1900, DateTime.Now.Year, "Évjárat: " );
            int wheel = GetNumber(1, int.MaxValue, "Kerék mérete: ");

            var msg = await connection.PostCars(makeId, model, perf, year, wheel);
            Console.WriteLine(msg.message);
        }

        static async Task DeleteCar()
        {
            Console.Write("id: ");
            string id = Console.ReadLine();
            var msg = await connection.DeleteCars(id);

            Console.WriteLine(msg.message);
        }

        static async Task ListManufacturers()
        {
            var list = await connection.GetManufacturers();

            foreach (var m in list)
                Console.WriteLine($"{m.id}: {m.name} ({m.country}), alapitva: {m.foundationYear}, gyártáss: {m.manufacturerYear}");
        }

        static async Task AddManufacturer()
        {
            Console.Write("Gyártó neve: ");
            string name = Console.ReadLine();
            int fYear = GetNumber(1800, DateTime.Now.Year, "Alapítás év: ");
            Console.Write("Ország: ");
            string country = Console.ReadLine();
            int mYear = GetNumber(1900, DateTime.Now.Year, "Gyártás év: ");

            var msg = await connection.PostManufacturers(name, fYear, country, mYear);
            Console.WriteLine(msg.message);
        }

        static async Task DeleteManufacturer()
        {
            Console.Write("id: ");
            string id = Console.ReadLine();
            var msg = await connection.DeleteManufacturers(id);

            Console.WriteLine(msg.message);
        }

        static async Task ListOwners()
        {
            var list = await connection.GetOwners();
            foreach (var o in list)
                Console.WriteLine($"{o.id}: {o.name}, {o.address}, szül év: {o.birthYear}, autóID: {o.carID}");
        }

        static async Task AddOwner()
        {
            Console.Write("Car id: ");
            string carId = Console.ReadLine();
            Console.Write("Tulaj neve: ");
            string name = Console.ReadLine();
            Console.Write("Cím: ");
            string address = Console.ReadLine();
            int bYear = GetNumber(1900, DateTime.Now.Year, "Szül év: ");

            var msg = await connection.PostOwners(carId, name, address, bYear);
            Console.WriteLine(msg.message);
        }

        static async Task DeleteOwner()
        {
            Console.Write("id: ");
            string id = Console.ReadLine();
            var msg = await connection.DeleteOwners(id);
            Console.WriteLine(msg.message);
        }
    }
}
