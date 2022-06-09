using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ComponentAccessToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDTO;

namespace TransferSystemAPI.WebControllers
{
    [ApiController]
    [Route("/api/v1/players")] 
    public class WebPlayerController : ControllerBase
    {
        private readonly PlayerController _playerController;
        private readonly TeamController _teamController;

        public WebPlayerController(PlayerController playerController,
            TeamController teamController)
        {
            _playerController = playerController;
            _teamController = teamController;
        }

        /// <summary>
        /// Добавление нового игрока
        /// </summary>
        /// <param name="playerDTO">Экземпляр игрока</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] PlayerDTO playerDTO)
        {
            var newPlayer = new PlayerBL()
            {
                PlayerId = playerDTO.PlayerId,
                TeamId = playerDTO.TeamId,
                Playerstatistics = playerDTO.Playerstatistics,
                Playerspecifications = playerDTO.Playerspecifications,
                Name = playerDTO.Name,
                Position = playerDTO.Position,
                Weight = playerDTO.Weight,
                Height = playerDTO.Height,
                Number = playerDTO.Number,
                Age = playerDTO.Age,
                Country = playerDTO.Country,
                Cost = playerDTO.Cost
            };

            var isAdded = _playerController.Add(newPlayer);
            if (isAdded == false)
            {
                return BadRequest();
            }
            var addedPlayer = _playerController.FindPlayerByName(playerDTO.Name);
            return Ok(new PlayerDTO(addedPlayer));
        }

        /// <summary>
        /// Вывод информации о всех игроках
        /// </summary>
        /// <param name="roleFilter">Фильтр (player, team)</param>
        /// <param name="name">Имя экземпляра фильтра (имя игрока/название команды)</param>
        /// <returns>Список игроков</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PlayerDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromQuery] string roleFilter = null, [FromQuery] string name = null)
        {
            if (String.Compare(roleFilter, "player") == 0 && name != null)
            {
                var player = _playerController.FindPlayerByName(name);
                if (player == null)
                {
                    return NotFound();
                }
                return Ok(new PlayerDTO(player));
            }
            else if (String.Compare(roleFilter, "team") == 0 && name != null)
            {
                var team = _teamController.FindTeamByName(name);
                var players = _playerController.GetPlayersByTeam(team);
                if (players == null)
                {
                    return NotFound();
                }
                return Ok(ConvertToDTO(players));
            }
            else
            {
                var players = _playerController.GetAll();
                if (players == null)
                {
                    return NotFound();
                }
                return Ok(ConvertToDTO(players));
            }
        }

        /// <summary>
        /// Изменение данных игрока
        /// </summary>
        /// <param name="playerDTO">Экземпляр изменяемого игрока</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(PlayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] PlayerDTO playerDTO)
        {
            var player = new PlayerBL()
            {
                PlayerId = playerDTO.PlayerId,
                TeamId = playerDTO.TeamId,
                Playerstatistics = playerDTO.Playerstatistics,
                Playerspecifications = playerDTO.Playerspecifications,
                Name = playerDTO.Name,
                Position = playerDTO.Position,
                Weight = playerDTO.Weight,
                Height = playerDTO.Height,
                Number = playerDTO.Number,
                Age = playerDTO.Age,
                Country = playerDTO.Country,
                Cost = playerDTO.Cost
            };

            var isUpdated = _playerController.Update(player);
            if (isUpdated == false)
            {
                return NotFound();
            }
            var updatedPlayer = _playerController.FindPlayerById(playerDTO.PlayerId);
            return Ok(new PlayerDTO(updatedPlayer));
        }

        /// <summary>
        /// Удаление игрока
        /// </summary>
        /// <param name="id">Идентификатор игрока</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Такой записи нет</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        [HttpDelete]
        [ProducesResponseType(typeof(PlayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var player = _playerController.FindPlayerById(id); 
            var isDeleted = _playerController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new PlayerDTO(player));
            }
        }

        /// <summary>
        /// Вывод информации об игроке по его id
        /// </summary>
        /// <returns>Информация об игроке</returns>
        /// <param name="id">Идентификатор игрока</param>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PlayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult FindPlayerById([FromRoute] int id)
        {
            var player = _playerController.FindPlayerById(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(new PlayerDTO(player));
        }

        private List<PlayerDTO> ConvertToDTO(List<PlayerBL> players)
        {
            List<PlayerDTO> playerDTOs = new List<PlayerDTO>();
            foreach (var player in players)
            {
                playerDTOs.Add(new PlayerDTO(player));
            }

            return playerDTOs;
        }
    }
}
