using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComponentAccessToDB.RepositoryImplementation;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDTO;

namespace TransferSystemAPI.WebControllers
{
    [ApiController]
    [Route("/api/v1/availableDeals")] 
    public class WebAvailableDealsController : ControllerBase
    {
        private readonly AvailableDealsController _availableDealsController;
        private readonly ManagementController _managementController;

        public WebAvailableDealsController(AvailableDealsController availableDealsController,
            ManagementController managementController)
        {
            _availableDealsController = availableDealsController;
            _managementController = managementController;
        }

        /// <summary>
        /// Добавление новой сделки в доступные
        /// </summary>
        /// <param name="availabledealDTO">Экземпляр доступной сделки</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(AvailabledealDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody]AvailabledealDTO availabledealDTO)
        {
            var newAvailableDeal = new AvailabledealBL()
            {
                Id = availabledealDTO.Id,
                PlayerId = availabledealDTO.PlayerId,
                TomanagementId = availabledealDTO.TomanagementId,
                FrommanagementId = availabledealDTO.FrommanagementId,
                Cost = availabledealDTO.Cost,
                Status = availabledealDTO.Status
            };

            var isAdded = _availableDealsController.Add(newAvailableDeal);
            if (isAdded == false)
            {
                return BadRequest();
            }
            var availableDeals = _availableDealsController.GetAll();
            var addedAvailableDeal = availableDeals[availableDeals.Count - 1];
            return Ok(new AvailabledealDTO(addedAvailableDeal));
        }

        /// <summary>
        /// Вывод информации о всех возможных сделок
        /// </summary>
        /// <param name="direction">Направление получения сделок (0 - входящие, 1 - исходящие)</param>
        /// <param name="id">ID экзмепляра менджмента, соответствующего сделке</param>
        /// <returns>Список возможных сделок</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AvailabledealDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromQuery] int? direction = null, [FromQuery] int? id = null)
        {
            switch (direction)
            {                 
                case 0:
                    var management = _managementController.FindManagementById((int)id);
                    var deals = _availableDealsController.GetIncomingDeals(management);
                    if (deals == null)
                    {
                        return NotFound();
                    }
                    return Ok(ConvertToDTO(deals));

                case 1:
                    var manage = _managementController.FindManagementById((int)id);
                    var deal = _availableDealsController.GetOutgoingDeals(manage);
                    if (deal == null)
                    {
                        return NotFound();
                    }
                    return Ok(ConvertToDTO(deal));

                default:
                    var availableDeals = _availableDealsController.GetAll();
                    if (availableDeals == null)
                    {
                        return NotFound();
                    }
                    return Ok(ConvertToDTO(availableDeals));
            }            
        }

        /// <summary>
        /// Изменение доступной сделки
        /// </summary>
        /// <param name="availabledealDTO">Экземпляр изменяемой сделки</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(AvailabledealDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] AvailabledealDTO availabledealDTO)
        {
            var availableDeal = new AvailabledealBL()
            {
                Id = availabledealDTO.Id,
                PlayerId = availabledealDTO.PlayerId,
                TomanagementId = availabledealDTO.TomanagementId,
                FrommanagementId = availabledealDTO.FrommanagementId,
                Cost = availabledealDTO.Cost,
                Status = availabledealDTO.Status
            };
            var isUpdated = _availableDealsController.Update(availableDeal);
            if (isUpdated == false)
            {
                return NotFound();
            }
            var updatedavailableDeal = _availableDealsController.GetDealById(availabledealDTO.Id);
            return Ok(new AvailabledealDTO(updatedavailableDeal));
        }

        /// <summary>
        /// Удаление доступной сделки
        /// </summary>
        /// <param name="id">Идентификатор сделки</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        /// <response code="404">Такой записи нет</response>
        [HttpDelete]
        [ProducesResponseType(typeof(AvailabledealDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var availabledeal = _availableDealsController.GetDealById(id);
            var isDeleted = _availableDealsController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new AvailabledealDTO(availabledeal));
            }
        }

        /// <summary>
        /// Вывод информации о сделке
        /// </summary>
        /// <returns>Информация об искомой сделке</returns>
        /// <param name="id">Идентификатор требуемой к выводу сделки</param>
        /// <response code="200">Успешно выведено</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(AvailabledealDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetDealById([FromRoute] int id)
        {
            var deal = _availableDealsController.GetDealById(id);
            if (deal == null)
            {
                return NotFound();
            }
            return Ok(new AvailabledealDTO(deal));
        }

        //ConfirmDeal

        //RejectDeal
         
        /// <summary>
        /// Подтверждение или отклонение сделки
        /// </summary>
        /// <param name="id">Id нужной сделки</param>
        /// <param name="status">Новый статус сделки</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="400">Что-то пошло не так</response>
        [Route("{id}/status")]
        [HttpPatch]
        [ProducesResponseType(typeof(AvailabledealDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult GetOutgoingDeals([FromRoute] int id, [FromQuery, Required] Status status)
        {
            var deal = _availableDealsController.GetDealById(id);
            bool isUpdated;
            switch (status)
            {
                case Status.Confirmed:
                    isUpdated = _availableDealsController.ConfirmDeal(deal);
                    break;
                case Status.Rejected:
                    isUpdated = _availableDealsController.RejectDeal(deal);
                    break;
                default:
                    isUpdated = false;
                    break;
            }

            if (!isUpdated)
            {
                return BadRequest();
            }

            return Ok(new AvailabledealDTO(_availableDealsController.GetDealById(id)));
        }

        private List<AvailabledealDTO> ConvertToDTO(List<AvailabledealBL> availabledeals)
        {
            List<AvailabledealDTO> availabledealDTOs = new List<AvailabledealDTO>();
            foreach (var availabledeal in availabledeals)
            {
                availabledealDTOs.Add(new AvailabledealDTO(availabledeal));
            }

            return availabledealDTOs;
        }
    }
}
