using EfCoreLab.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EfCoreLab.Repositories;

internal class GamesRepository : BaseRepository<Game>
{
    public override List<Game> GetAll()
    {
        using var context = new GameStoreContext();

        var games = context.Games.Include(x => x.Developer).OrderBy(x => x.GameId).ToList();

        return games;
    }

    public List<Game> GetLast(int limit)
    {
        using var context = new GameStoreContext();

        var games = context.Games.Include(x => x.Developer).OrderBy(x => x.GameId).Take(limit).ToList();

        return games;
    }

    public List<Game> GetAll(int limit)
    {
        using var context = new GameStoreContext();

        var games = context.Games.Take(limit).Include(x => x.Developer).OrderBy(x => x.GameId).ToList();

        return games;
    }

    public List<Game> GetAllByFilter(int limit, int lowerPriceBound)
    {
        using var context = new GameStoreContext();

        var games = context.Games.Where(x => x.Price > lowerPriceBound)
            .Take(limit)
            .Include(x => x.Developer)
            .OrderBy(x => x.GameId)
            .ToList();

        return games;
    }

    public void UpdateByFilter(int price)
    {
        using var context = new GameStoreContext();

        foreach (var game in context.Games)
        {
            if (game.Price > price && !game.Description!.Contains("[HC]"))
            {
                // High cost
                game.Description = "[HC] " + game.Description;
            }
        }

        context.SaveChanges();
    }

    public void ResetDescriptions()
    {
        using var context = new GameStoreContext();

        foreach (var game in context.Games)
        {
            // HC] Описание 1
            // var a = ["HC]", "Описание 1"]

            if (game.Description!.Contains("[HC]"))
            {
                // High cost
                game.Description = game.Description.Split("]")[1].Trim();
            }
        }

        context.SaveChanges();
    }
}
