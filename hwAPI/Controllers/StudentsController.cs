using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using hwAPI.Model;
using hwAPI.Common;
using hwAPI.Model.Core;
using MongoDB.Bson;

namespace hwAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IRepositoryContext<Student> _repo;

        public StudentsController(ILogger<StudentsController> logger, IRepositoryContext<Student> repositoryContext)
        {
            _logger = logger;
            _repo = repositoryContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get() => await _repo.Collection.Find(_ => true).ToListAsync();

        // GET api/students/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(long id)
        {
            var student = _repo.Collection.Find(obj => obj.Id == id).FirstOrDefault();
            if (student == null)
                return new NotFoundResult();
            return new ObjectResult(student);
        }

        // POST api/students
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            student.Id = await _repo.Collection.CountDocumentsAsync(new BsonDocument()) + 1;
            await _repo.Collection.InsertOneAsync(student);
            return new OkObjectResult(student);
        }

        // DELETE api/students/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var student = _repo.Collection.Find(obj => obj.Id == id).FirstOrDefault();
            if (student == null)
                return new NotFoundResult();

            //Delete
            var filter = Builders<Student>.Filter.Eq(m => m.Id, id);
            var deleteResult = await _repo.Collection.DeleteOneAsync(filter);

            return new OkResult();
        }
    }

}
