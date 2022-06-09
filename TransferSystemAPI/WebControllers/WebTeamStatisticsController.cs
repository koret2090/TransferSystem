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
    [Route("/api/v1/teamstatistics")]
    public class WebTeamStatisticsController : ControllerBase
    {
        private readonly TeamStatisticsController _teamStatisticsController;
        private readonly TeamController _teamController;
        
        public WebTeamStatisticsController(TeamStatisticsController teamStatisticsController,
            TeamController teamController)
        {
            _teamStatisticsController = teamStatisticsController;
            _teamController = teamController;
        }
        
        /// <summary>
        /// Добавление новой статистики команды
        /// </summary>
        /// <param name="teamstatistic">Экземпляр статистики команды</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(TeamstatisticDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] TeamstatisticDTO teamstatistic)
        {
            var newTeamStatistic = new TeamstatisticBL()
            {
                NumberOfMatchesPlayed = teamstatistic.NumberOfMatchesPlayed,
                League = teamstatistic.League,
                PlaceInTheLeague = teamstatistic.PlaceInTheLeague,
                NumberOfTrophies = teamstatistic.NumberOfTrophies
            };
            
            var isAdded = _teamStatisticsController.Add(newTeamStatistic);
            if (isAdded == false)
            {
                return BadRequest();
            }

            var addedTeamStatistic = _teamStatisticsController.GetTeamStatisticById(teamstatistic.StatisticsId);
            return Ok(new TeamstatisticDTO(addedTeamStatistic));
        }

        /// <summary>
        /// Вывод информации о всех статистиках команд
        /// </summary>
        /// <returns>Список статистик команд</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="400">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TeamstatisticDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var teamstatistics = _teamStatisticsController.GetAll();
            if (teamstatistics == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDTO(teamstatistics));
        }
        
        /// <summary>
        /// Изменение статистики команды
        /// </summary>
        /// <param name="teamstatisticDto">Экземпляр статистики команды</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(TeamstatisticDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] TeamstatisticDTO teamstatisticDto)
        {
            var teamStatistic = new TeamstatisticBL()
            {
                StatisticsId = teamstatisticDto.StatisticsId,
                NumberOfMatchesPlayed = teamstatisticDto.NumberOfMatchesPlayed,
                League = teamstatisticDto.League,
                PlaceInTheLeague= teamstatisticDto.PlaceInTheLeague,
                NumberOfTrophies = teamstatisticDto.NumberOfTrophies
            };
            
            var isUpdated = _teamStatisticsController.Update(teamStatistic);
            if (isUpdated == false)
            {
                return NotFound();
            }
            
            var updatedTeamStatistic = _teamStatisticsController.GetTeamStatisticById(teamstatisticDto.StatisticsId);
            return Ok(new TeamstatisticDTO(updatedTeamStatistic));
        }
        
        /// <summary>
        /// Удаление статистики команды
        /// </summary>
        /// <param name="id">Идентифкатор статистики команды</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Такой записи нет</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        [HttpDelete]
        [ProducesResponseType(typeof(TeamstatisticDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var teamstatistic = _teamStatisticsController.GetTeamStatisticById(id);
            var isDeleted = _teamStatisticsController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new TeamstatisticDTO(teamstatistic));
            }
        }

        /// <summary>
        /// Получение информации о статистике команды по Id
        /// </summary>
        /// <param name="id">Id статистики команды</param>
        /// <returns>Экземпляр статистики команды</returns>
        /// <response code="200">Успешно найден</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(TeamstatisticDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetTeamStatisticById([FromRoute] int id)
        {
            var teamstatistic = _teamStatisticsController.GetTeamStatisticById(id);
            if (teamstatistic == null)
            {
                return NotFound();
            }
            return Ok(new TeamstatisticDTO(teamstatistic));
        }
        
        private List<TeamstatisticDTO> ConvertToDTO(List<TeamstatisticBL> teamstatistics)
        {
            List<TeamstatisticDTO> teamstatisticDtos = new List<TeamstatisticDTO>();
            foreach (var teamstatistic in teamstatistics)
            {
                teamstatisticDtos.Add(new TeamstatisticDTO(teamstatistic));
            }

            return teamstatisticDtos;
        }
    }
}