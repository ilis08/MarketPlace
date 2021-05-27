using ApplicationService.DTOs;
using Data.Context;
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

                })
            }
        }
    }
}
