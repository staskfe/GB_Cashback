using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Boticario.Cashback.WebAPI
{
    public class BaseControllerCustom : ControllerBase
    {

        public ObjectResult InternalErrorCustom (string message)
        {
            return new ObjectResult(new { code = 500, message });
        }
    }
}
