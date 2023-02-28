using Chef.Common.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chef.Trading.Web.Controllers
{
    [Route("api/common/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProspectController : ControllerBase
    {
        private readonly IProspectService prospectService;

        public ProspectController(IProspectService prospectService)
        {
            this.prospectService = prospectService;
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<ProspectDto>> Get(int id)
        {
            var prospect = await prospectService.GetAsync(id);

            if (prospect == null)
            {
                return NotFound();
            }

            return Ok(prospect);
        }

        //[HttpGet("GetAll")]
        //public async Task<ActionResult<IEnumerable<ProspectDto>>> GetAll()
        //{
        //    var prospects = await prospectService.GetAllAsync();

        //    return Ok(prospects);
        //}

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(Prospect obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultCount = await this.prospectService.GetExistingProspectAsync(obj);
            if (resultCount != 0)
            {
                throw new Exception($"Prospect with same properties already exists.");
            }
            else
            {
                var prospect = await prospectService.InsertAsync(obj);
                return CreatedAtAction(nameof(Insert), prospect);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(Prospect prospect)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultCount = await this.prospectService.GetEditExistingProspectAsync(prospect);
            if (resultCount != 0)
            {
                throw new Exception($"Prospect with same properties already exists.");
            }
            else
            {
                var result = await prospectService.UpdateAsync(prospect);
                return Ok(result);
            }
        }

        //[HttpDelete("Delete/{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var prospect = await prospectService.GetAsync(id);

        //    if (prospect == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await prospectService.DeleteAsync(id);

        //    return Ok(result);
        //}

        //[HttpGet("GetAllCustomer")]
        //public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomer()
        //{
        //    var prospects = await prospectService.GetAllCustomer();

        //    return Ok(prospects);
        //}

        ////[HttpGet("GetAllTaxJurisdiction")]
        ////public async Task<ActionResult<IEnumerable<TaxJurisdictionDto>>> GetAllTaxJurisdiction()
        ////{
        ////    var prospects = await prospectService.GetAllTaxJurisdiction();

        ////    return Ok(prospects);
        ////}


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProspectDto>>> GetAll()
        {
            var prospects = await prospectService.GetAll();

            return Ok(prospects);
        }
    }
}

