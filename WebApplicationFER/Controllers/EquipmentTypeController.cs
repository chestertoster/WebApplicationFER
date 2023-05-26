using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    [Route("api/[controller]")]
    public class EquipmentTypeController : Controller
    {
        private NewDbPracContext _dbContext;
        public EquipmentTypeController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Возвращает данные о типе оборудования по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор типа оборудования.</param>
        /// <returns>Возвращает данные о типе оборудования в формате JSON.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="404">В запросе содержатся некорректные данные.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.EquipmentTypes.FirstOrDefault(equipmentType => equipmentType.Id == id));
        }

        /// <summary>
        /// Создает новый тип оборудования.
        /// </summary>
        /// <param name="equipmentType">Данные о типе оборудования.</param>
        /// <returns>Возвращает статус HTTP 200 в случае успешного создания типа оборудования.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="400">В запросе содержатся некорректные данные.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(EquipmentType equipmentType)
        {
            if (_dbContext.EquipmentTypes.Any(innerType => innerType.Id == equipmentType.Id) == false)
            {
                _dbContext.EquipmentTypes.Add(equipmentType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Обновляет информацию о типе оборудования.
        /// </summary>
        /// <param name="equipmentType">Объект типа оборудования для обновления.</param>
        /// <returns>Возвращает код состояния HTTP.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="400">В запросе содержатся некорректные данные.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [Consumes("application/json")]
        public IActionResult Edit([FromBody] EquipmentType equipmentType)
        {
            if (_dbContext.EquipmentTypes.Any(innerType => innerType.Id == equipmentType .Id))
            {
                _dbContext.EquipmentTypes.Update(equipmentType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Удаляет тип оборудования по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор типа оборудования.</param>
        /// <returns>Возвращает код состояния HTTP.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="400">В запросе содержатся некорректные данные.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var equipmentToDelete = _dbContext.EquipmentTypes.FirstOrDefault(innerType => innerType.Id == id);

            if (equipmentToDelete == default)
                return BadRequest();

            _dbContext.EquipmentTypes.Remove(equipmentToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
