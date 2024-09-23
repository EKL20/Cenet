using Microsoft.AspNetCore.Mvc;
using ValhallaManagement.Application.DTOs;
using ValhallaManagement.Application.UseCase;
using ValhallaManagement.Application.UseCases;
using ValhallaManagement.Core.Entities;

namespace ValhallaManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/vikingos")]
    public class VikingosController : ControllerBase
    {
        private readonly CrearVikingoUseCase _crearVikingoUseCase;
        private readonly ObtenerVikingoPorIdUseCase _obtenerVikingoPorIdUseCase;
        private readonly ActualizarVikingoUseCase _actualizarVikingoUseCase;
        private readonly EliminarVikingoUseCase _eliminarVikingoUseCase;

        public VikingosController(CrearVikingoUseCase crearVikingoUseCase,
            ObtenerVikingoPorIdUseCase obtenerVikingoPorIdUseCase,
            ActualizarVikingoUseCase actualizarVikingoUseCase,
            EliminarVikingoUseCase eliminarVikingoUseCase)
        {
            _crearVikingoUseCase = crearVikingoUseCase;
            _obtenerVikingoPorIdUseCase = obtenerVikingoPorIdUseCase;
            _actualizarVikingoUseCase = actualizarVikingoUseCase;
            _eliminarVikingoUseCase = eliminarVikingoUseCase;
        }


        // POST: Crear nuevo vikingo
        [HttpPost]
        public async Task<IActionResult> CrearVikingo([FromBody] VikingoDto vikingoDto)
        {
            var resultado = await _crearVikingoUseCase.Ejecutar(vikingoDto);
            return CreatedAtAction(nameof(CrearVikingo), new { id = resultado.Id }, resultado);
        }

        // GET: Obtener un vikingo por ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObtenerVikingoPorId(int id)
        {
            var vikingo = await _obtenerVikingoPorIdUseCase.Execute(id);

            if (vikingo == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra el vikingo
            }

            return Ok(vikingo); // Retorna 200 con el vikingo si se encuentra
        }


        // PUT: Actualizar un vikingo existente
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarVikingo(int id, [FromBody] Vikingo vikingo)
        {
            if (id != vikingo.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo del request.");
            }

            // Llamamos al caso de uso sin intentar asignar el resultado a una variable
            await _actualizarVikingoUseCase.Execute(vikingo);

            return Ok(vikingo); // Retorna 200 con el vikingo actualizado
        }


        // DELETE: Eliminar un vikingo por ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarVikingo(int id)
        {
            var vikingo = await _obtenerVikingoPorIdUseCase.Execute(id);

            if (vikingo == null)
            {
                return NotFound(); // Retorna 404 si no existe el vikingo a eliminar
            }

            await _eliminarVikingoUseCase.Execute(id);
            return NoContent(); // Retorna 204 indicando que se eliminó el recurso
        }
    }
}
