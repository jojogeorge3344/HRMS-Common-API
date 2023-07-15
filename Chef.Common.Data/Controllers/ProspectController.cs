using Chef.Common.Authentication;
using Chef.Common.Exceptions;

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
                return NotFound();

            return Ok(prospect);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(Prospect prospect)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await prospectService.IsExistingProspectAsync(prospect))
                throw new ResourceAlreadyExistsException($"Prospect with same properties already exists.");

            if (await prospectService.IsTaxNoExist(prospect.TaxNo, prospect.Id))
                throw new ResourceAlreadyExistsException($"Tax number already exists.");

            return CreatedAtAction(nameof(Insert), await prospectService.InsertAsync(prospect));
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(Prospect prospect)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await prospectService.IsExistingProspectAsync(prospect))
                throw new ResourceAlreadyExistsException($"Prospect with same properties already exists.");

            if (await prospectService.IsTaxNoExist(prospect.TaxNo, prospect.Id))
                throw new ResourceAlreadyExistsException($"Tax number already exists.");

            return Ok(await prospectService.UpdateAsync(prospect));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var rowsAffected = await prospectService.DeleteAsync(id);
            if (rowsAffected < 1)
                return NotFound("The Prospect does not exist.");

            return Ok(rowsAffected);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProspectDto>>> GetAll()
        {
            return Ok(await prospectService.GetAllAsync());
        }

        [HttpGet("IsCodeExist/{code}")]
        public async Task<ActionResult<bool>> IsCodeExist(string code)
        {
            return Ok(await prospectService.IsCodeExist(code));
        }

        [HttpGet("IsTaxNoExist/{taxNo}/{prospectId}")]
        public async Task<ActionResult<bool>> IsTaxNoExist(long taxNo, int prospectId)
        {
            return Ok(await prospectService.IsTaxNoExist(taxNo, prospectId));
        }
    }
}

