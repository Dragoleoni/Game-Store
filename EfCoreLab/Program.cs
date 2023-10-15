using EfCoreLab.Repositories;

namespace EfCoreLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var devRepo = new DevelopersRepository();
            var gameRepo = new GamesRepository();
            var devs = devRepo.GetAll().Take(50);
            foreach (var dev in devs)
            {
                Console.WriteLine(dev.ToString());
            }
            Console.WriteLine("and many more devs...");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Every 10th dev (mod on id)");
            devs = devRepo.GetAllByFilter().Take(50);

            foreach (var dev in devs)
            {
                Console.WriteLine(dev.ToString());
            }

            Console.WriteLine("-------------------------------------");

            Console.WriteLine($"Total count of devs: {devRepo.GetCount()}");

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Top 50 games");
            var games = gameRepo.GetAll(50);
            foreach (var game in games)
            {
                Console.WriteLine(game.ToString());
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("All games with price > 70");
            games = gameRepo.GetAllByFilter(50, 70);
            foreach (var game in games)
            {
                Console.WriteLine(game.ToString());
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Last 5 devs after add new dev and after delete");

            var newDev = new Models.Developer()
            {
                DeveloperName = $"[{Guid.NewGuid()}] Suhogo Game Dev"
            };

            devRepo.Create(newDev);

            var lastFiveDevs = devRepo.GetAll().TakeLast(5);

            foreach (var dev in lastFiveDevs)
            {
                Console.WriteLine(dev.ToString());
            }
            Console.WriteLine("after delete");
            devRepo.Delete(newDev);
            lastFiveDevs = devRepo.GetAll().TakeLast(5);

            foreach (var dev in lastFiveDevs)
            {
                Console.WriteLine(dev.ToString());
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Last 5 games after add new game and after delete");

            var newGame = new Models.Game()
            {
                Title = "Test game for devs",
                Description = null,
                DeveloperId = 1,
                ReleaseYear = 2023,
                Price = 25
            };

            gameRepo.Create(newGame);
            var lastFiveGames = gameRepo.GetLast(5);

            foreach (var game in lastFiveGames)
            {
                Console.WriteLine(game.ToString());
            }
            gameRepo.Delete(newGame);
            Console.WriteLine("after delete");

            lastFiveGames = gameRepo.GetLast(5);

            foreach (var game in lastFiveGames)
            {
                Console.WriteLine(game.ToString());
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Updates games descriptions with more then 70$");
            gameRepo.UpdateByFilter(70);

            games = gameRepo.GetAllByFilter(50, 70);

            foreach (var game in games)
            {
                Console.WriteLine(game.ToString());
            }

            gameRepo.ResetDescriptions();
        }
    }
}