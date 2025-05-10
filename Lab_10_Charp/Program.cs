using System;
using System.Threading;

namespace LifeOfCity
{
    // Інтерфейс події
    public interface ICityEvent
    {
        string Name { get; }
        string Description { get; }
        DateTime Time { get; }
    }

    // Подія: Пожежа
    public class FireEvent : ICityEvent
    {
        public string Name => "Пожежа";
        public string Description { get; }
        public DateTime Time { get; }

        public FireEvent(string location)
        {
            Description = $"Пожежа у районі: {location}";
            Time = DateTime.Now;
        }
    }

    // Подія: Концерт
    public class ConcertEvent : ICityEvent
    {
        public string Name => "Концерт";
        public string Description { get; }
        public DateTime Time { get; }

        public ConcertEvent(string artist)
        {
            Description = $"Концерт виконавця: {artist}";
            Time = DateTime.Now;
        }
    }

    // Клас міста
    public class City
    {
        public delegate void CityEventHandler(ICityEvent cityEvent);
        public event CityEventHandler OnEventOccurred;

        public void TriggerEvent(ICityEvent cityEvent)
        {
            Console.WriteLine($"\nПодія: {cityEvent.Name} — {cityEvent.Description}");
            OnEventOccurred?.Invoke(cityEvent);
        }
    }

    // Пожежна служба
    public class FireDepartment
    {
        public void OnEvent(ICityEvent cityEvent)
        {
            if (cityEvent is FireEvent)
            {
                Console.WriteLine("Пожежна служба виїхала на місце події.");
            }
        }
    }

    // Поліція
    public class PoliceDepartment
    {
        public void OnEvent(ICityEvent cityEvent)
        {
            if (cityEvent is ConcertEvent)
            {
                Console.WriteLine("Поліція забезпечує порядок на концерті.");
            }
        }
    }

    // Програма
    class Program
    {
        static void Main(string[] args)
        {
            City city = new City();

            FireDepartment fire = new FireDepartment();
            PoliceDepartment police = new PoliceDepartment();

            // Підписка на події
            city.OnEventOccurred += fire.OnEvent;
            city.OnEventOccurred += police.OnEvent;

            // Події
            city.TriggerEvent(new FireEvent("Центральний ринок"));
            Thread.Sleep(1000);

            city.TriggerEvent(new ConcertEvent("Океан Ельзи"));
            Thread.Sleep(1000);

            city.TriggerEvent(new FireEvent("Вул. Холодногірська"));
            Thread.Sleep(1000);

            city.TriggerEvent(new ConcertEvent("Kalush Orchestra"));

            Console.WriteLine("\nМоделювання завершено.");

            Console.WriteLine();
            Console.WriteLine("Enter...");
            Console.ReadLine();
        }
    }
}
