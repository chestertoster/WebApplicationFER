using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    [Route("api/[controller]")]
    public class ActivityDirectionController : Controller
    {
        private readonly NewDbPracContext _dbContext;
        public ActivityDirectionController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получает конкретное направление деятельности из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор направления деятельности для получения.</param>
        /// <returns>Возвращает объект JSON, представляющий направление деятельности, соответствующее указанному идентификатору. Если соответствующее направление деятельности не найдено, возвращает HTTP-статус 404 Not Found.</returns>
        /// <response code="200">Указывает, что запись направления деятельности была успешно получена.</response>
        /// <response code="404">Указывает, что в базе данных не найдена соответствующая запись направления деятельности.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ActivityDirection), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.ActivityDirections.FirstOrDefault(energyConsumption => energyConsumption.Id == id));
        }

        /// <summary>
        /// Создает новую запись направления деятельности в базе данных.
        /// </summary>
        /// <param name="activityDirection">Объект, представляющий новую запись направления деятельности.</param>
        /// <returns>Возвращает HTTP-статус 200 OK в случае успешного создания записи в базе данных, либо HTTP-статус 400 Bad Request в случае, если запись с указанным идентификатором уже существует.</returns>
        /// <response code="200">Указывает, что запись направления деятельности была успешно создана.</response>
        /// <response code="400">Указывает, что в базе данных уже существует запись с указанным идентификатором.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(ActivityDirection activityDirection)
        {
            if (_dbContext.ActivityDirections.Any(innerDirection => innerDirection.Id == activityDirection.Id) == false)
            {
                _dbContext.ActivityDirections.Add(activityDirection);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Обновляет данные по определенному направлению деятельности.
        /// </summary>
        /// <param name="activityDirection">Объект направления деятельности для обновления.</param>
        /// <returns>Возвращает HTTP-статус 200 OK в случае успешного обновления данных. Если объект направления деятельности для обновления не найден, возвращает HTTP-статус 400 Bad Request.</returns>
        /// <response code="200">Указывает, что данные по направлению деятельности были успешно обновлены.</response>
        /// <response code="400">Указывает, что объект направления деятельности для обновления не найден в базе данных.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(ActivityDirection activityDirection)
        {
            if (_dbContext.ActivityDirections.Any(innerDirection => innerDirection.Id == activityDirection.Id))
            {
                _dbContext.ActivityDirections.Update(activityDirection);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Удаляет запись о направлении деятельности из базы данных по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор направления деятельности для удаления.</param>
        /// <returns>Возвращает объект JSON, представляющий удаленное направление деятельности. Если запись не найдена в базе данных, возвращает HTTP-статус 404 Not Found.</returns>
        /// <response code="200">Указывает, что запись о направлении деятельности была успешно удалена.</response>
        /// <response code="404">Указывает, что в базе данных не найдена соответствующая запись о направлении деятельности.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ActivityDirection), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var directionToDelete = _dbContext.ActivityDirections.FirstOrDefault(innerDirection => innerDirection.Id == id);

            if (directionToDelete == default)
                return BadRequest();

            _dbContext.ActivityDirections.Remove(directionToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
