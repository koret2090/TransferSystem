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
    [Route("/api/v1/desiredPlayers")]
    public class WebDesiredPlayersController : ControllerBase
    {
        private readonly DesiredPlayersController _desiredPlayersController;
        private readonly ManagementController _managementController;
        private readonly PlayerController _playerController;
        private readonly TeamController _teamController;

        public WebDesiredPlayersController(DesiredPlayersController desiredPlayersController,
            ManagementController managementController, PlayerController playerController,
            TeamController teamController)
        {
            _desiredPlayersController = desiredPlayersController;
            _managementController = managementController;
            _playerController = playerController;
            _teamController = teamController;
        }

        /// <summary>
        /// Добавление нового игрока в желаемые
        /// </summary>
        /// <param name="playerId">Идентификатор игрока</param>
        /// <param name="managementId">Идентификатор менеджмента, который запросил игрока</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        /// <response code="404">Игрок не найден</response>
        [HttpPost]
        [ProducesResponseType(typeof(DesiredplayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Add([FromQuery, Required] int playerId, [FromQuery, Required] int managementId)
        {
            var player = _playerController.FindPlayerById(playerId);
            if (player == null)
            {
                return NotFound();
            }
            
            var isAdded = _desiredPlayersController.Add(new DesiredplayerBL() {Id = 0, PlayerId = player.PlayerId, Managementid = managementId});
            if (isAdded == false)
            {
                return BadRequest();
            }
            
            var desiredPlayers = _desiredPlayersController.GetAll();
            var addedDesiredPlayer = desiredPlayers[desiredPlayers.Count - 1];
            return Ok(AddPlayerTeamNames(new DesiredplayerDTO(addedDesiredPlayer)));
        }

        /// <summary>
        /// Вывод информации о всех желаемых игроках
        /// </summary>
        /// <param name="managementId">ID экзмепляра менджмента, соответствующего игрокам</param>
        /// <returns>Список желаемых игроков</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<DesiredplayerDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromQuery] int? managementId = null)
        {
            if (managementId == null)
            {
                var desiredplayers = _desiredPlayersController.GetAll();
                if (desiredplayers == null)
                {
                    return NotFound();
                }
                return Ok(AddPlayerTeamNames(ConvertToDTO(desiredplayers)));
            }
            else
            {
                var management = _managementController.FindManagementById((int)managementId);
                var desiredplayers = _desiredPlayersController.GetPlayersByManagement(management);
                if (desiredplayers == null)
                {
                    return NotFound();
                }
                return Ok(AddPlayerTeamNames(ConvertToDTO(desiredplayers)));
            }
        }

        /// <summary>
        /// Изменение данных желаемого игрока
        /// </summary>
        /// <param name="desiredplayerDTO">Экземпляр изменяемого игрока</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(DesiredplayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] DesiredplayerDTO desiredplayerDTO)
        {
            var desiredplayer = new DesiredplayerBL()
            {
                Id = desiredplayerDTO.Id,
                PlayerId = desiredplayerDTO.PlayerId,
                Managementid = desiredplayerDTO.Managementid
            };

            var isUpdated = _desiredPlayersController.Update(desiredplayer);
            if (isUpdated == false)
            {
                return NotFound();
            }
            var updatedDesiredPlayer = _desiredPlayersController.GetPlayerById(desiredplayerDTO.Id);
            return Ok(AddPlayerTeamNames(new DesiredplayerDTO(updatedDesiredPlayer)));
        }

        /// <summary>
        /// Удаление игрока из желаемых
        /// </summary>
        /// <param name="id">Идентификатор желаемого игрока</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="400">Не удалось удалить желаемого игрока</response>
        /// <response code="404">Не удалось найти желаемого игрока</response>
        [HttpDelete]
        [ProducesResponseType(typeof(DesiredplayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var desiredplayer = _desiredPlayersController.GetPlayerById(id);
            var isDeleted = _desiredPlayersController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(AddPlayerTeamNames(new DesiredplayerDTO(desiredplayer)));
            }
        }

        /// <summary>
        /// Вывод информации о желаемом игроке
        /// </summary>
        /// <returns>Информация о желаемом игроке</returns>
        /// <param name="id">Идентификатор требуемого к выводу игрока</param>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(DesiredplayerDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetPlayerById([FromRoute] int id)
        {
            var player = _desiredPlayersController.GetPlayerById(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(AddPlayerTeamNames(new DesiredplayerDTO(player)));
        } 
        
        private List<DesiredplayerDTO> ConvertToDTO(List<DesiredplayerBL> desiredplayers)
        {
            List<DesiredplayerDTO> desiredplayerDTOs = new List<DesiredplayerDTO>();
            foreach (var desiredplayer in desiredplayers)
            {
                desiredplayerDTOs.Add(new DesiredplayerDTO(desiredplayer));
            }

            return desiredplayerDTOs;
        }

        private List<DesiredplayerDTO> AddPlayerTeamNames(List<DesiredplayerDTO> desiredplayerDtos)
        {
            for (var i = 0; i < desiredplayerDtos.Count; i++)
            {
                desiredplayerDtos[i] = AddPlayerTeamNames(desiredplayerDtos[i]);
            }

            return desiredplayerDtos;
        }

        private DesiredplayerDTO AddPlayerTeamNames(DesiredplayerDTO desiredplayerDto)
        {
            var player = _playerController.FindPlayerById((int)desiredplayerDto.PlayerId);
            var team = _teamController.FindTeamByPlayer(player);
            desiredplayerDto.PlayerName = player.Name;
            desiredplayerDto.TeamName = team.Name;
            return desiredplayerDto;
        }
    }
}
