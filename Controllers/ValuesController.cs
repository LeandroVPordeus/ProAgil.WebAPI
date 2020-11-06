using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly ProAgilContext _context;
        public ValuesController(ProAgilContext context )
        {
           _context = context;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetEventos(int id)
        {
            try
            {
                var result = await _context.Eventos.ToListAsync();
                 return Ok(result);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha No Banco De Dados");
            }
         
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           try
            {
                var result = await _context.Eventos.FirstOrDefaultAsync(p => p.Id ==id);
                 
                 return Ok(result);
            }
            catch (System.Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Falha No Banco De Dados");
            }
       
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]Evento evento)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Eventos.Add(evento);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterEvento",
                new { id = evento.Id }, evento);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Evento evento)
        {
            if (id != evento.Id)
            {
                return BadRequest();
            }
            _context.Entry(evento).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Evento> Delete(int id)
        {
            var evento = _context.Eventos.FirstOrDefault(x => x.Id == id);

            if (evento != null)
            {
                return NotFound();
            }
            _context.Eventos.Remove(evento);
            _context.SaveChanges();
            return evento;
            
            
        }
    }
}
