using Microsoft.AspNetCore.Mvc;
using Payment_API.Helpers;
using Payment_API.Model;
using System;
using System.Collections.Generic;

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
            try
            {
                double net = Amounts.ComputeNetAmount(transaction);
                return new NetAmount(net);
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (IndexOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occured.");
            }
        }
    }
}