using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
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
        private readonly ITheRepository<Student> _repo;

        public StudentsController(ILogger<StudentsController> logger, ITheRepository<Student> repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get() => new ObjectResult(await _repo.GetAll());

        // GET api/students/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(long id)
        {
            var student = await _repo.Get(obj => obj.Id, id);
            if (student == null)
                return new NotFoundResult();
            return new ObjectResult(student);
        }

        // POST api/students
        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] Student student)
        {
            student.Id = await _repo.GetNextId();
            await _repo.Create(student);
            return new OkObjectResult(student);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Put(long id, [FromBody] Student student)
        {
            var studentFromDb = await _repo.Get(m => m.Id, id);

            if (studentFromDb == null)
                return new NotFoundResult();

            student.Id = studentFromDb.Id;
            student.InternalId = studentFromDb.InternalId;
            await _repo.Update(student, m => m.Id == id);

            return new OkObjectResult(student);
        }

        // DELETE api/students/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.Get(m => m.Id, id);

            if (post == null)
                return new NotFoundResult();

            await _repo.Delete(m => m.Id, id);
            return new OkResult();
        }
    }

}
