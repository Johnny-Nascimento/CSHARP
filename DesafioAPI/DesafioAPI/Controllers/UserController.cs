using DesafioAPI.Context;
using DesafioAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesafioAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UserController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _context.usuarios
                .Include(u => u.Logs)
                .Include(u => u.Equipe)
                .ThenInclude(t => t.Projetos)
                .ToListAsync();

            return Ok(usuarios);
        }

        [HttpPost]
        public ActionResult<Usuario> Post([FromBody]List<Usuario> usuarios)
        {
            _context.usuarios.AddRange(usuarios);
            _context.SaveChanges();

            return Ok(new { message = "Usuarios adicionados com sucesso!" });
        }

        [HttpPost("upload-json")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadJson(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            using var stream = new StreamReader(file.OpenReadStream());
            var jsonContent = await stream.ReadToEndAsync();

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var listaUsuario = JsonSerializer.Deserialize<List<Usuario>>(jsonContent, options);

                _context.usuarios.AddRange(listaUsuario);
                _context.SaveChanges();

                return Ok(new { message = "Usuarios adicionados com sucesso!" });
            }
            catch (JsonException j)
            {
                return BadRequest("Arquivo JSON inválido." + j.Message);
            }
        }
    }
}
