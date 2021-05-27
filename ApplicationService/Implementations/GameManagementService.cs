using ApplicationService.DTOs;
using Data.Context;
using Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService.Implementations
{
    public class GameManagementService
    {
        private GameStoreDBContext ctx = new GameStoreDBContext();

        public List<GameDTO> Get()
        {
            List<GameDTO> gamesDto = new List<GameDTO>();

            foreach (var item in ctx.Games.ToList())
            {
                gamesDto.Add(new GameDTO
                {
                        Id = item.Id,
                        Name = item.Name,
                        ShortDescription = item.ShortDescription,
                        LongDescription = item.LongDescription,
                        Release = item.Release,
                        Image = item.Image,
                        Price = item.Price,
                        Available = item.Available,
                        CategoryId = item.CategoryId,
                        Category = new CategoryDTO
                        {
                            Id = item.CategoryId,
                            Title = item.Category.Title,
                            Description = item.Category.Description
                        }
                 });
            }
            return gamesDto;
        }

        public GameDTO GetById(int id)
        {
            Game game = ctx.Games.Find(id);

            GameDTO gameDTO = new GameDTO
            {
                Id = game.Id,
                Name = game.Name,
                ShortDescription = game.ShortDescription,
                LongDescription = game.LongDescription,
                Release = game.Release,
                Image = game.Image,
                Price = game.Price,
                Available = game.Available,
                CategoryId = game.CategoryId,
                Category = new CategoryDTO
                {
                    Id = game.CategoryId,
                    Title = game.Category.Title,
                    Description = game.Category.Description
                }
            };
            return gameDTO;
        }

        public bool Save(GameDTO gameDto)
        {
            if (gameDto.Category == null || gameDto.CategoryId == 0)
            {
                return false;
            }

           /* Category category = new Category
            {
                Id = gameDto.CategoryId,
                Title = gameDto.Category.Title,
                Description = gameDto.Category.Description
            };*/

            Game game = new Game
            {
                Name = gameDto.Name,
                ShortDescription = gameDto.ShortDescription,
                LongDescription = gameDto.LongDescription,
                Release = gameDto.Release,
                Image = gameDto.Image,
                Price = gameDto.Price,
                Available = gameDto.Available,
                CategoryId = gameDto.CategoryId
            };

            try
            {
                ctx.Games.Add(game);
                ctx.SaveChanges();

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Game game = ctx.Games.Find(id);
                ctx.Games.Remove(game);
                ctx.SaveChanges();

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
