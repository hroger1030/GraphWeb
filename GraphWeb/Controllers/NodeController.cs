using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;

using log4net;

namespace GraphWeb
{
    public class NodeController : ApiController
    {
        private static readonly ILog _Log = LogManager.GetLogger(typeof(NodeController));

        public NodeController() { }

        [HttpPost]
        [Route("api/getNode")]
        [ResponseType(typeof(int))]
        public IHttpActionResult Post([FromBody] int nodeId)
        {
            try
            {
                var node = WebApiApplication.Factory.ExploreNode(nodeId);
                return Ok(node);
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return InternalServerError(ex);
            }
        }
    }
}
