using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiSegura.Controllers
{
    [Authorize]
    [RoutePrefix("api/tarjeta")]
    public class TarjetaController : ApiController
    {

    }
}
