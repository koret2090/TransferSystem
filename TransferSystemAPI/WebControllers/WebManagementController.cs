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
    [Route("/api/v1/managements")]
    public class WebManagementController : ControllerBase
    {
        private readonly ManagementController _managementController;

        public WebManagementController(ManagementController managementController)
        {
            _managementController = managementController;
        }

        /// <summary>
        /// Добавление нового менеджмента
        /// </summary>
        /// <param name="managementDTO">Экземпляр менеджмента</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(ManagementDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] ManagementDTO managementDTO)
        {
            var newManagement = new ManagementBL()
            {
                ManagementId = managementDTO.ManagementId,
                AnalysistId = managementDTO.AnalysistId,
                ManagerId = managementDTO.ManagerId
            };

            var isAdded = _managementController.Add(newManagement);
            if (isAdded == false)
            {
                return BadRequest();
            }
            var managements = _managementController.GetAll();
            var addedManagement = managements[managements.Count - 1];
            return Ok(new ManagementDTO(addedManagement));
        }

        /// <summary>
        /// Вывод информации о всех менеджментах
        /// </summary>
        /// <param name="personal">Фильтр по роли (analytic, manager)</param>
        /// <param name="id">Id экземпляра выбранной роли</param>
        /// <returns>Список менеджментов</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ManagementDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromQuery] string personal = null, [FromQuery] int? id = null)
        {
            if (String.Compare(personal, "analytic") == 0)
            {
                var management = _managementController.FindByAnalytic((int)id);
                if (management == null)
                {
                    return NotFound();
                }
                return Ok(new ManagementDTO(management));
            }
            else if (String.Compare(personal, "manager") == 0)
            {
                var management = _managementController.FindByManager((int)id);
                if (management == null)
                {
                    return NotFound();
                }
                return Ok(new ManagementDTO(management));
            }
            else
            {
                var managements = _managementController.GetAll();
                if (managements == null)
                {
                    return NotFound();
                }
                return Ok(ConvertToDTO(managements));
            }
        }

        /// <summary>
        /// Изменение данных менеджмента
        /// </summary>
        /// <param name="managementDTO">Экземпляр изменяемого менеджмента</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(ManagementDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] ManagementDTO managementDTO)
        {
            var management = new ManagementBL()
            {
                ManagementId = managementDTO.ManagementId,
                AnalysistId = managementDTO.AnalysistId,
                ManagerId = managementDTO.ManagerId
            };

            var isUpdated = _managementController.Update(management);
            if (isUpdated == false)
            {
                return NotFound();
            }
            var updatedManagement = _managementController.FindManagementById(managementDTO.ManagementId);
            return Ok(new ManagementDTO(updatedManagement));
        }

        /// <summary>
        /// Удаление менеджмента
        /// </summary>
        /// <param name="id">Идентификатор менеджмента</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="404">Такой записи нет</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        [HttpDelete]
        [ProducesResponseType(typeof(ManagementDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var management = _managementController.FindManagementById(id);
            var isDeleted = _managementController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new ManagementDTO(management));
            }
        }

        /// <summary>
        /// Вывод информации о менеджменте по id менеджмента
        /// </summary>
        /// <returns>Информация о менеджменте</returns>
        /// <param name="id">id менеджмента</param>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(ManagementDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult FindManagementById([FromRoute] int id)
        {
            var management = _managementController.FindManagementById(id);
            if (management == null)
            {
                return NotFound();
            }
            return Ok(new ManagementDTO(management));
        }

        private List<ManagementDTO> ConvertToDTO(List<ManagementBL> managements)
        {
            List<ManagementDTO> managementDTOs = new List<ManagementDTO>();
            foreach (var management in managements)
            {
                managementDTOs.Add(new ManagementDTO(management));
            }

            return managementDTOs;
        }
    }
}

