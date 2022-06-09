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
    [Route("/api/v1/playerSpecifications")]
    public class WebPlayerSpecificationsController : ControllerBase
    {
        private readonly PlayerSpecificationsController _playerSpecificationsController;
        private readonly PlayerController _playerController;

        public WebPlayerSpecificationsController(PlayerSpecificationsController playerSpecificationsController,
            PlayerController playerController)
        {
            _playerSpecificationsController = playerSpecificationsController;
            _playerController = playerController;
        }

        /// <summary>
        /// Добавление нового набора характеристик игрока
        /// </summary>
        /// <param name="playerSpecificationDTO">Экземпляр набора характеристик</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerspecificationDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] PlayerspecificationDTO playerSpecificationDTO)
        {
            var newPlayerspecification = new PlayerspecificationBL()
            {
                SpecificationsId = playerSpecificationDTO.SpecificationsId,
                Shooting = playerSpecificationDTO.Shooting,
                Defense = playerSpecificationDTO.Defense,
                Skating = playerSpecificationDTO.Skating,
                Physical = playerSpecificationDTO.Physical
            };

            var isAdded = _playerSpecificationsController.Add(newPlayerspecification);
            if (isAdded == false)
            {
                return BadRequest();
            }
            var playerSpecifications = _playerSpecificationsController.GetAll();
            var addedPlayerSpecifications = playerSpecifications[playerSpecifications.Count - 1];
            return Ok(new PlayerspecificationDTO(addedPlayerSpecifications));
        }

        /// <summary>
        /// Вывод информации о всех наборах характеристик игроков
        /// </summary>
        /// <returns>Список наборов характеристик игроков</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<PlayerspecificationDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var playerSpecifications = _playerSpecificationsController.GetAll();
            if (playerSpecifications == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDTO(playerSpecifications));
        }

        /// <summary>
        /// Изменение данных набора характеристик
        /// </summary>
        /// <param name="playerSpecificationDTO">Экземпляр изменяемого набора характеристик</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(PlayerspecificationDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] PlayerspecificationDTO playerSpecificationDTO)
        {
            var playerSpecification = new PlayerspecificationBL()
            {
                SpecificationsId = playerSpecificationDTO.SpecificationsId,
                Shooting = playerSpecificationDTO.Shooting,
                Defense = playerSpecificationDTO.Defense,
                Skating = playerSpecificationDTO.Skating,
                Physical = playerSpecificationDTO.Physical
            };

            var isUpdated = _playerSpecificationsController.Update(playerSpecification);
            if (isUpdated == false)
            {
                return NotFound();
            }
            var updatedPlayerSpecification = _playerSpecificationsController.GetPlayerSpecificationById(playerSpecificationDTO.SpecificationsId);
            return Ok(new PlayerspecificationDTO(updatedPlayerSpecification));
        }

        /// <summary>
        /// Удаление набора характеристик
        /// </summary>
        /// <param name="id">Идентификатор характеристики</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        /// <response code="404">Такой записи нет</response>
        [HttpDelete]
        [ProducesResponseType(typeof(PlayerspecificationDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var playerSpecification = _playerSpecificationsController.GetPlayerSpecificationById(id);
            var isDeleted = _playerSpecificationsController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new PlayerspecificationDTO(playerSpecification));
            }
        }      

        /// <summary>
        /// Вывод информации о наборе характеристик по его id
        /// </summary>
        /// <returns>Информация о наборе характеристик</returns>
        /// <param name="id">Идентификатор набора характеристик</param>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PlayerspecificationDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetPlayerSpecificationById([FromRoute] int id)
        {
            var playerSpecification = _playerSpecificationsController.GetPlayerSpecificationById(id);
            if (playerSpecification == null)
            {
                return NotFound();
            }
            return Ok(new PlayerspecificationDTO(playerSpecification));
        }

        private List<PlayerspecificationDTO> ConvertToDTO(List<PlayerspecificationBL> playerSpecifications)
        {
            List<PlayerspecificationDTO> playerspecificationDTOs = new List<PlayerspecificationDTO>();
            foreach (var playerSpecification in playerSpecifications)
            {
                playerspecificationDTOs.Add(new PlayerspecificationDTO(playerSpecification));
            }

            return playerspecificationDTOs;
        }
    }
}
