using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    [Route("api/[controller]")]
    public class EquipmentController : Controller
    {
        private NewDbPracContext _dbContext;
        public EquipmentController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Получает информацию об оборудовании по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор оборудования.</param>
        /// <returns>Возвращает объект оборудования в формате JSON.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="404">Оборудование не найдено.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.Equipment.FirstOrDefault(equipment => equipment.Id == id));
        }

        /// <summary>
        /// Создает новую запись об оборудовании.
        /// </summary>
        /// <param name="equipment">Данные для создания новой записи об оборудовании.</param>
        /// <returns>Возвращает код состояния HTTP.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="400">В запросе содержатся некорректные данные.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(Equipment equipment)
        {
            if (_dbContext.Equipment.Any(innerEquipment => innerEquipment.Id == equipment.Id) == false)
            {
                _dbContext.Equipment.Add(equipment);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Обновляет данные записи об оборудовании.
        /// </summary>
        /// <param name="equipment">Данные для обновления записи об оборудовании.</param>
        /// <returns>Возвращает код состояния HTTP.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="400">В запросе содержатся некорректные данные или запись об оборудовании не найдена в базе данных.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Edit(Equipment equipment)
        {
            if (_dbContext.Equipment.Any(innerEquipment => innerEquipment.Id == equipment.Id))
            {
                _dbContext.Equipment.Update(equipment);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Удаляет запись об оборудовании из базы данных по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор записи об оборудовании.</param>
        /// <returns>Возвращает код состояния HTTP.</returns>
        /// <response code="200">Успешное выполнение запроса.</response>
        /// <response code="400">Запись об оборудовании не найдена в базе данных.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var equipmentToDelete = _dbContext.Equipment.FirstOrDefault(innerEquipment => innerEquipment.Id == id);

            if (equipmentToDelete == default)
                return BadRequest();

            _dbContext.Equipment.Remove(equipmentToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
