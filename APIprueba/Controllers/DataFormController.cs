using APIprueba.Interfaz;
using APIprueba.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIprueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataFormController : ControllerBase
    {
        public DataFormController(IformData formDatos)
        {
            this.formDatos = formDatos;
        }

        private readonly IformData formDatos;
        [HttpPost("EnviarNotificacion")]

        public async Task <IActionResult> EnviarNotificacion([FromBody] formsData data) 
        {
            try
            {
                var formData = await this.formDatos.datos(data);
                return Ok(formData);
                
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("listaCiudades")]

        public async Task<IActionResult> listaCiudades()
        {
            try
            {
                var listaCiudades = await this.formDatos.Estados();
                return Ok(listaCiudades);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
