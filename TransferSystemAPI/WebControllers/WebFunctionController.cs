using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDTO;

namespace TransferSystemAPI.WebControllers
{
    [ApiController]
    [Route("/api/v1/teamstats")]
    public class WebFunctionController : ControllerBase
    {
        private readonly FunctionController _functionController;

        public WebFunctionController(FunctionController functionController)
        {
            _functionController = functionController;
        }

        /// <summary>
        /// Получение информации о статистиках
        /// </summary>
        /// /// <returns>Список статистик</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PlayersTeamStatDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetPlayersTeamStat()
        {
            var players = _functionController.GetPlayersTeamStat();
            if (players == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDTO(players));
        }

        private List<PlayersTeamStatDTO> ConvertToDTO(List<PlayersTeamStatBL> playersTeamStats)
        {
            List<PlayersTeamStatDTO> playersTeamStatDTOs = new List<PlayersTeamStatDTO>();
            foreach (var playersTeamStat in playersTeamStats)
            {
                playersTeamStatDTOs.Add(new PlayersTeamStatDTO(playersTeamStat));
            }

            return playersTeamStatDTOs;
        }
    }
}
