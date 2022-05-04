using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using hwAPI.Model;
using hwAPI.Common;

namespace hwAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            //建立 mongo client
            var client = new MongoClient(AppConfigure.getConnectionString());
            //取得 database
            var db = client.GetDatabase(AppConfigure.getdbString());
            //取得 user collection           
            var collection = db.GetCollection<Student>(AppConfigure.getCollectionNameString());

            var tmp = await collection.Find(_ => true).ToListAsync();

            return tmp;
        }
    }

}
