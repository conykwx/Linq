using System;
using System.Linq;
using System.Data.Linq; // Для роботи з LINQ to SQL
using System.Data.Linq.Mapping;

class Program
{
    static void Main(string[] args)
    {
        // Створюємо контекст даних для підключення до SQL Server
        using (var db = new DataContext(@"Server=DESKTOP-9K56BQI\SQLEXPRESS;Database=Linq;Trusted_Connection=True;TrustServerCertificate=True;"))
        {
            // Відображення всієї інформації про країни
            var allCountries = from country in db.GetTable<Countries>()
                               select country;

            Console.WriteLine("Вся інформація про країни:");
            foreach (var country in allCountries)
            {
                Console.WriteLine($"{country.Name} - Столиця: {country.Capital}, Населення: {country.Population}, Площа: {country.Area}, Частина світу: {country.Continent}");
            }

            // Відображення назв країн
            var countryNames = from country in db.GetTable<Countries>()
                               select country.Name;

            Console.WriteLine("\nНазви країн:");
            foreach (var name in countryNames)
            {
                Console.WriteLine(name);
            }

            // Відображення назв столиць
            var capitalNames = from country in db.GetTable<Countries>()
                               select country.Capital;

            Console.WriteLine("\nНазви столиць:");
            foreach (var capital in capitalNames)
            {
                Console.WriteLine(capital);
            }

            // Відображення назв європейських країн
            var europeanCountries = from country in db.GetTable<Countries>()
                                    where country.Continent == "Europe"
                                    select country.Name;

            Console.WriteLine("\nЄвропейські країни:");
            foreach (var name in europeanCountries)
            {
                Console.WriteLine(name);
            }

            // Відображення країн з площею більше певного числа
            int minArea = 1000000; // Порогове значення площі
            var largeCountries = from country in db.GetTable<Countries>()
                                 where country.Area > minArea
                                 select country.Name;

            Console.WriteLine($"\nКраїни з площею більше {minArea}:");
            foreach (var name in largeCountries)
            {
                Console.WriteLine(name);
            }

            // Країни з літерами 'a' та 'u' в назвах
            var auCountries = from country in db.GetTable<Countries>()
                              where country.Name.Contains("a") && country.Name.Contains("u")
                              select country.Name;

            Console.WriteLine("\nКраїни з літерами 'a' та 'u':");
            foreach (var name in auCountries)
            {
                Console.WriteLine(name);
            }

            // Відображення країни з найбільшою площею
            var largestCountry = (from country in db.GetTable<Countries>()
                                  orderby country.Area descending
                                  select country).FirstOrDefault();

            if (largestCountry != null)
            {
                Console.WriteLine("\nКраїна з найбільшою площею:");
                Console.WriteLine($"{largestCountry.Name} - {largestCountry.Area} км²");
            }

            // Обчислення середньої площі країн в Азії
            var averageAreaAsia = (from country in db.GetTable<Countries>()
                                   where country.Continent == "Asia"
                                   select country.Area).Average();

            Console.WriteLine("\nСередня площа країн в Азії:");
            Console.WriteLine($"{averageAreaAsia} км²");
        }
    }
}

// Модель для таблиці Countries
[Table(Name = "Countries")]
public class Countries
{
    [Column(IsPrimaryKey = true, IsDbGenerated = true)]
    public int Id { get; set; }

    [Column]
    public string Name { get; set; }

    [Column]
    public string Capital { get; set; }

    [Column]
    public int Population { get; set; }

    [Column]
    public int Area { get; set; }

    [Column]
    public string Continent { get; set; }
}
