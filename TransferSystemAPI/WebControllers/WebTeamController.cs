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
    [Route("/api/v1/teams")]
    public class WebTeamController : ControllerBase
    {
        private readonly TeamController _teamController;
        private readonly PlayerController _playerController;

        public WebTeamController(TeamController teamController, 
            PlayerController playerController)
        {
            _teamController = teamController;
            _playerController = playerController;
        }
        
        /// <summary>Добавление новой команды</summary>
        /// <param name="teamDto">Экземпляр команды</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(TeamDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] TeamDTO teamDto)
        {
            var newTeam = new TeamBL()
            {
                Balance = teamDto.Balance,
                Country = teamDto.Country,
                Headcoach = teamDto.Headcoach,
                ManagementId = teamDto.ManagementId,
                Name = teamDto.Name,
                Stadium = teamDto.Stadium,
                StatisticsId = teamDto.StatisticsId
            };
            
            var isAdded = _teamController.Add(newTeam);
            if (isAdded == false)
            {
                return BadRequest();
            }

            var addedTeam = _teamController.FindTeamByName(teamDto.Name);
            return Ok(new TeamDTO(addedTeam));
        }

        /// <summary>
        /// Вывод информации о всех командах
        /// </summary>
        /// <returns>Список команд</returns>
        /// <param name="name">Название команды</param>
        /// <param name="roleFilter">Фильтр по роли (player, management)</param>
        /// <param name="roleId">Id экземпляра роли</param>
        /// <response code="200">Успешно выведено</response>
        /// <response code="400">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TeamDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromQuery] string name = null,
            [FromQuery] string roleFilter = null, [FromQuery] int? roleId = null)
        {
            if (name != null)
            {
                var team = _teamController.FindTeamByName(name);
                if (team == null)
                {
                    return NotFound();
                }
                return Ok(new TeamDTO(team));                
            }

            if (string.Compare(roleFilter, "player") == 0 && roleId != null)
            {
                var player = _playerController.FindPlayerById((int)roleId);
                var team = _teamController.FindTeamByPlayer(player);
                if (team == null)
                {
                    return NotFound();
                }
                return Ok(new TeamDTO(team));
            }

            if (string.Compare(roleFilter, "management") == 0 && roleId != null)
            {
                var team = _teamController.FindTeamByManagement((int)roleId);
                if (team == null)
                {
                    return NotFound();
                }
                return Ok(new TeamDTO(team));
            }

            var teams = _teamController.GetAll();
            if (teams == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDTO(teams));
        } 
        
        /// <summary>
        /// Изменение информации о команде
        /// </summary>
        /// <param name="teamDto">Экземпляр команды</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(TeamDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] TeamDTO teamDto)
        {
            var team = new TeamBL()
            {
                TeamId = teamDto.TeamId,
                Balance = teamDto.Balance,
                Country = teamDto.Country,
                Headcoach = teamDto.Headcoach,
                ManagementId = teamDto.ManagementId,
                Name = teamDto.Name,
                StatisticsId = teamDto.StatisticsId
            };
            
            var isUpdated = _teamController.Update(team);
            if (isUpdated == false)
            {
                return NotFound();
            }
            
            var updatedTeam = _teamController.FindTeamById(teamDto.TeamId);
            return Ok(new TeamDTO(updatedTeam));
        }
        
        /// <summary>
        /// Удаление информации о команде
        /// </summary>
        /// <param name="id">Идентификатор команды</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Такой записи нет</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        [HttpDelete]
        [ProducesResponseType(typeof(TeamDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var team = _teamController.FindTeamById(id);
            var isDeleted = _teamController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new TeamDTO(team));
            }
        }

        /// <summary>
        /// Получение информации о команде по Id
        /// </summary>
        /// <returns>Информация о команде</returns>
        /// <param name="id">Id команды</param>
        /// <response code="200">Успешно найдено</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(TeamDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult FindTeamById([FromRoute] int id)
        {
            var team = _teamController.FindTeamById(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(new TeamDTO(team));
        }    
             
        private List<TeamDTO> ConvertToDTO(List<TeamBL> teams)
        {
            List<TeamDTO> teamDtos = new List<TeamDTO>();
            foreach (var team in teams)
            {
                teamDtos.Add(new TeamDTO(team));
            }

            return teamDtos;
        }
    }
}