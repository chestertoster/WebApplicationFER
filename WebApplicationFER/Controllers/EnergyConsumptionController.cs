using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    [Route("api/[controller]")]
    public class EnergyConsumptionController : Controller
    {
        private readonly NewDbPracContext _dbContext;
        public EnergyConsumptionController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получает конкретный тип энергии из базы данных.
        /// </summary>
        /// <param name="code">Идентификатор типа энергии для получения.</param>
        /// <returns>Возвращает объект JSON, представляющий тип энергии, соответствующий указанному идентификатору. Если соответствующий тип энергии не найден, возвращает HTTP-статус 404 Not Found.</returns>
        /// <response code="200">Указывает, что запись типа энергии была успешно получена.</response>
        /// <response code="404">Указывает, что в базе данных не найдена соответствующая запись типа энергии.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnergyType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(string code)
        {
            return Json(_dbContext.EnergyConsumptions.FirstOrDefault(energyConsumption => energyConsumption.EquipmentCode == code));
        }

        /// <summary>
        /// Создает новую запись потребления энергии в базе данных.
        /// </summary>
        /// <param name="energyConsumption">Новая запись потребления энергии.</param>
        /// <returns>Возвращает HTTP-статус 200 OK в случае успешного создания записи, иначе возвращает HTTP-статус 400 Bad Request.</returns>
        /// <response code="200">Указывает, что запись потребления энергии была успешно создана.</response>
        /// <response code="400">Указывает, что запись потребления энергии с таким же кодом оборудования уже существует в базе данных.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(EnergyConsumption energyConsumption)
        {
            if (_dbContext.EnergyConsumptions.Any(innerEnergyConsumption => innerEnergyConsumption.EquipmentCode == energyConsumption.EquipmentCode) == false)
            {
                _dbContext.EnergyConsumptions.Add(energyConsumption);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Обновляет информацию о потреблении энергии в базе данных.
        /// </summary>
        /// <param name="energyConsumption">Объект, содержащий информацию о потреблении энергии.</param>
        /// <returns>Возвращает HTTP-статус 200 OK, если обновление прошло успешно, иначе возвращает статус 400 Bad Request.</returns>
        /// <response code="200">Указывает, что данные о потреблении энергии были успешно обновлены.</response>
        /// <response code="400">Указывает, что не удалось обновить данные о потреблении энергии из-за отсутствия соответствующего объекта в базе данных.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(EnergyConsumption energyConsumption)
        {
            if (_dbContext.EnergyConsumptions.Any(innerConsumption => innerConsumption.EquipmentCode == energyConsumption.EquipmentCode))
            {
                _dbContext.EnergyConsumptions.Update(energyConsumption);

                _dbContext.SaveChangesAsync();

                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Удаляет запись потребления энергии из базы данных по коду оборудования.
        /// </summary>
        /// <param name="code">Код оборудования записи потребления энергии, которую требуется удалить.</param>
        /// <returns>Возвращает HTTP-статус 200 OK в случае успешного удаления записи, иначе возвращает HTTP-статус 400 Bad Request.</returns>
        /// <response code="200">Указывает, что запись потребления энергии была успешно удалена.</response>
        /// <response code="400">Указывает, что запись потребления энергии с указанным кодом оборудования не найдена в базе данных.</response>
        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(string code)
        {
            var consumptionToDelete = _dbContext.EnergyConsumptions.FirstOrDefault(innerConsumption => innerConsumption.EquipmentCode == code);

            if (consumptionToDelete == default)
                return BadRequest();

            _dbContext.EnergyConsumptions.Remove(consumptionToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
