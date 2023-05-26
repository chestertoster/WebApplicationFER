using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private NewDbPracContext _dbContext;
        public UsersController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.Users.FirstOrDefault(user => user.Id == id));
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        /// <param name="user">Объект пользователя, которого необходимо создать.</param>
        /// <returns>Возвращает статус HTTP 200 в случае успешного выполнения запроса.</returns>
        /// <response code="200">Запрос выполнен успешно.</response>
        /// <response code="400">В случае ошибки возвращает статус HTTP 400.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] User user)
        {
            if (_dbContext.Users.Any(innerUser => innerUser.Id == user.Id) == false)
            {
                _dbContext.Users.Add(user);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Обновляет данные пользователя.
        /// </summary>
        /// <param name="user">Объект пользователя, данные которого необходимо обновить.</param>
        /// <returns>Возвращает статус HTTP 200 в случае успешного выполнения запроса.</returns>
        /// <response code="200">Запрос выполнен успешно.</response>
        /// <response code="400">В случае ошибки возвращает статус HTTP 400.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit([FromBody] User user)
        {
            if (_dbContext.Users.Any(innerUser => innerUser.Id == user.Id))
            {
                _dbContext.Users.Update(user);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Удаляет пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Возвращает статус HTTP 200 в случае успешного выполнения запроса.</returns>
        /// <response code="200">Запрос выполнен успешно.</response>
        /// <response code="400">В случае ошибки возвращает статус HTTP 400.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var userToDelete = _dbContext.Users.FirstOrDefault(innerUser => innerUser.Id == id);

            if (userToDelete == default)
                return BadRequest();

            _dbContext.Users.Remove(userToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
