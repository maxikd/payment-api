using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment_API.Helpers;
using Payment_API.Model;

namespace Payment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpGet]
        [Route("mdr")]
        public ActionResult<ICollection<MDR>> MDR()
        {
            return Ok(MDRData.GetMDR());
        }

        [HttpPost]
        [Route("transaction")]
        public ActionResult<NetAmount> Transaction([FromBody] Transaction transaction)
        {
            double net = Amounts.ComputeNetAmount(transaction);
            return new NetAmount(net);
        }
    }
}