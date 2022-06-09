using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ComponentAccessToDB;
using ComponentBuisinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Controllers;
using Models.ModelsBL;
using Models.ModelsDTO;

namespace TransferSystemAPI.WebControllers
{
    [ApiController]
    [Route("/api/v1/userinfo")]
    public class WebUserInfoController : ControllerBase
    {
        private readonly UserInfoController _userInfoController;

        public WebUserInfoController(UserInfoController userInfoController)
        {
            _userInfoController = userInfoController;
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="userinfo">Экземпляр пользователя</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="400">Проблемы с добавлением</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserInfoDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] UserInfoDTO userinfo)
        {
            var newUser = new UserInfoBL()
                {Login = userinfo.Login, Hash = userinfo.Hash, Permission = userinfo.Permission};
            var isAdded = _userInfoController.Add(newUser);
            if (isAdded == false)
            {
                return BadRequest();
            }

            var addedUser = _userInfoController.FindUserByLogin(userinfo.Login);
            return Ok(new UserInfoDTO(addedUser));
        }

        /// <summary>
        /// Вывод информации о всех пользователях
        /// </summary>
        /// <returns>Список пользователей</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="400">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserInfoDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            var users = _userInfoController.GetAll();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(ConvertToDTO(users));
        }
        
        /// <summary>
        /// Изменение информации пользователя
        /// </summary>
        /// <param name="userinfo">Экземпляр пользователя</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPut]
        [ProducesResponseType(typeof(UserInfoDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] UserInfoDTO userinfo)
        {
            var user = new UserInfoBL()
                {Id = userinfo.Id, Login = userinfo.Login, Hash = userinfo.Hash, Permission = userinfo.Permission};
            var isUpdated = _userInfoController.Update(user);
            if (isUpdated == false)
            {
                return NotFound();
            }
            
            var addedUser = _userInfoController.FindUserByLogin(userinfo.Login);
            return Ok(new UserInfoDTO(addedUser));
        }

        /// <summary>
        /// Удаление информации о пользователе
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <response code="200">Успешно удалено</response>
        /// <response code="400">Не удалось удалить данную запись</response>
        /// <response code="404">Такой записи нет</response>
        [HttpDelete]
        [ProducesResponseType(typeof(UserInfoDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery, Required] int id)
        {
            var userinfo = _userInfoController.FindUserById(id);
            var isDeleted = _userInfoController.Delete(id);
            switch (isDeleted)
            {
                case Models.StatusCode.BadRequest:
                    return BadRequest();
                case Models.StatusCode.NotFound:
                    return NotFound();
                default:
                    return Ok(new UserInfoDTO(userinfo));
            }
        }

        /// <summary>
        /// Получение информации о пользователе по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Экземпляр пользователя</returns>
        /// <response code="200">Успешно найден</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{login}")]
        [HttpGet]
        [ProducesResponseType(typeof(UserInfoDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult FindUserByLogin([FromRoute] string login)
        {
            var user = _userInfoController.FindUserByLogin(login);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new UserInfoDTO(user));
        }

        /*
        /// <summary>
        /// Получение информации о пользователе по его Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <response code="200">Успешно найден</response>
        /// <response code="404">Такой записи нет</response>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(UserInfoDTO), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult FindUserById([FromRoute] int id)
        {
            var user = _userInfoController.FindUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new UserInfoDTO(user));
        }
        */
        private List<UserInfoDTO> ConvertToDTO(List<UserInfoBL> users)
        {
            List<UserInfoDTO> userInfoDtos = new List<UserInfoDTO>();
            foreach (var user in users)
            {
                userInfoDtos.Add(new UserInfoDTO(user));
            }

            return userInfoDtos;
        }
    }
}