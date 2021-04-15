using back.Data;
using back.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace back.Controllers
{
    
    public class UsersController : ODataController
    {
        private readonly ApplicationContext _db;

        public UsersController(ApplicationContext context)
        {
            _db = context;
        }

        [EnableQuery]
        public IActionResult Post([FromODataBody]User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return Created(user);
        }
    }
}