using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    [Route("api/[controller]")]
    public class EnergyTypeController : Controller
    {
        private NewDbPracContext _dbContext;
        public EnergyTypeController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получает конкретный тип энергии из базы данных.
        /// </summary>
        /// <param name="id">Идентификатор типа энергии для получения.</param>
        /// <returns>Возвращает объект JSON, представляющий тип энергии, соответствующий указанному идентификатору. Если соответствующий тип энергии не найден, возвращает HTTP-статус 404 Not Found.</returns>
        /// <response code="200">Указывает, что запись типа энергии была успешно получена.</response>
        /// <response code="404">Указывает, что в базе данных не найдена соответствующая запись типа энергии.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnergyType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.EnergyTypes.FirstOrDefault(energyType => energyType.Id == id));
        }

        /// <summary>
        /// Создает новый тип энергии и сохраняет его в базе данных.
        /// </summary>
        /// <param name="energyType">Объект типа энергии, который нужно добавить в базу данных.</param>
        /// <returns>Возвращает HTTP-статус 200 OK, если тип энергии был успешно создан и сохранен в базе данных. Если указанный тип энергии уже существует, возвращает HTTP-статус 400 Bad Request.</returns>
        /// <response code="200">Указывает, что тип энергии был успешно создан и сохранен в базе данных.</response>
        /// <response code="400">Указывает, что указанный тип энергии уже существует в базе данных.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(EnergyType energyType)
        {
            if (_dbContext.EnergyTypes.Any(innerEnergyType => innerEnergyType.Id == energyType.Id) == false)
            {
                _dbContext.EnergyTypes.Add(energyType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Обновляет тип энергии в базе данных.
        /// </summary>
        /// <param name="energyType">Объект типа энергии, который нужно обновить.</param>
        /// <returns code="200">Возвращает код 200, если объект был успешно обновлен.</returns>
        /// <returns code="400">Указывает, что указанный тип энергии уже существует в базе данных.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(EnergyType energyType)
        {
            if (_dbContext.EnergyTypes.Any(innerType => innerType.Id == energyType.Id))
            {
                _dbContext.EnergyTypes.Update(energyType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Удаляет тип энергии из базы данных по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор типа энергии, который нужно удалить.</param>
        /// <returns code="200">Возвращает код 200, если объект был успешно удален.</returns>
        /// <returns code="400">Запись не найдена в базе данных.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var equipmentToDelete = _dbContext.EnergyTypes.FirstOrDefault(innerEnergyType => innerEnergyType.Id == id);

            if (equipmentToDelete == default)
                return BadRequest();

            _dbContext.EnergyTypes.Remove(equipmentToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
