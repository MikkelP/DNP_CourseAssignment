using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DNP_CourseAssignment.Controller
{
    
    public class UserController : ApiController
    {

        
        [HttpGet]
        [Route("api/user/usercount")]
        public int count()
        {
            return ServerConnection.Instance.GetUserCount();
        }

            
    }
}
