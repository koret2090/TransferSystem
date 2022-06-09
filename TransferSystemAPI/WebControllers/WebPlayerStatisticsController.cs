using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ModelsBL;
using Models.ModelsDTO;

namespace TransferSystemAPI.WebControllers
{
    [ApiController]
    [Route("/api/v1/playerstatistics")]
    public class WebPlayerStatisticsController : ControllerBase
    {
        private readonly PlayerStatisticsController _playerStatisticsController;
        private readonly PlayerController _playerController;

        public WebPlayerStatisticsController(PlayerStatisticsController playerStatisticsController,
            PlayerController playerController)
        {
            _playerStatisticsController = playerStatisticsController;
            _playerController = playerController;
        }
        
        /// <summary>
        /// Добавление новой статистики игрока
        /// </summary>
        /// <param name="playerstatisticDto">Экземпляр статистики игрока</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerstatisticDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] PlayerstatisticDTO playerstatisticDto)
        {
            var newPlayerStatistic = new PlayerstatisticBL()
            {
                Numberofwashers = playerstatisticDto.Numberofwashers,
                Averagegametime = playerstatisticDto.Averagegametime
            };
            
            var isAdded = _playerStatisticsController.Add(newPlayerStatistic);
            if (isAdded == false)
            {
                return BadRequest();
            }

            var addedPlayerStatistic = _playerStatisticsController.GetPlayerStatisticById(playerstatisticDto.StatisticsId);
            return Ok(new PlayerstatisticDTO(addedPlayerStatistic));
        }
        
        /// <summary>
        /// Вывод информации о всех статистиках игроков
        /// </summary>
        /// <response code="200">Успешно выведено</response>
        /// <response code="400">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PlayerstatisticDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var playerstatistics = _playerStatisticsController.GetAll();
            if (playerstatistics == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDTO(playerstatistics));
        }
        
        /// <summary>
        /// Изменение информации о статистике игрока
        /// </summary>
        /// <param name="playerstatisticDto">Экземпляр информации о статистике игрока</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(PlayersTeamStatDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] PlayerstatisticDTO playerstatisticDto)
        {
            var playerStatistic = new PlayerstatisticBL()
            {
                StatisticsId = playerstatisticDto.StatisticsId,
                Numberofwashers = playerstatisticDto.Numberofwashers,
                Averagegametime = playerstatisticDto.Averagegametime
            };
            
            var isUpdated = _playerStatisticsController.Update(playerStatistic);
            if (isUpdated == false)
            {
                return NotFound();
            }
            
            var updatedPlayerStatistic = _playerStatisticsController.GetPlayerStatisticById(playerstatisticDto.StatisticsId);
            return Ok(new PlayerstatisticDTO(updatedPlayerStatistic));
        }
        
        /// <summary>
        /// Удаление информации о статистике игрока
        /// </summary>
        /// <param name="id">Идентификатор статистики игрока</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Такой записи нет</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        [HttpDelete]
        [ProducesResponseType(typeof(PlayerstatisticDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var playerstatistic = _playerStatisticsController.GetPlayerStatisticById(id);
            var isDeleted = _playerStatisticsController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new PlayerstatisticDTO(playerstatistic));
            }
        }
        
        /// <summary>
        /// Получение информации о статистике игрока по Id
        /// </summary>
        /// <param name="id">Id статистики игрока</param>
        /// <response code="200">Успешно найден</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PlayerstatisticDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetPlayerStatisticById([FromRoute] int id)
        {
            var playerstatistic = _playerStatisticsController.GetPlayerStatisticById(id);
            if (playerstatistic == null)
            {
                return NotFound();
            }
            return Ok(new PlayerstatisticDTO(playerstatistic));
        }
               
        private List<PlayerstatisticDTO> ConvertToDTO(List<PlayerstatisticBL> playerstatistics)
        {
            List<PlayerstatisticDTO> playersTeamStatDtos = new List<PlayerstatisticDTO>();
            foreach (var playerstatistic in playerstatistics)
            {
                playersTeamStatDtos.Add(new PlayerstatisticDTO(playerstatistic));
            }

            return playersTeamStatDtos;
        }
    }
}