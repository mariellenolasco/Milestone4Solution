using Lodging.DataAccess.Repositories;
using Lodging.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lodging.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RentalController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Rental
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _unitOfWork.Rental.SelectAsync()); ;
        }

        // GET: api/Rental/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var result = await _unitOfWork.Rental.SelectAsync(id);
                if (result == null) return NotFound(id);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // POST: api/Rental
        [HttpPost]
        public async Task<IActionResult> PostAsync(RentalModel rental)
        {
            await _unitOfWork.Rental.InsertAsync(rental);
            await _unitOfWork.CommitAsync();

            return Created(Url.RouteUrl(rental.Id), rental.Id);
        }

        // PUT: api/Rental/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(RentalModel rental)
        {
            _unitOfWork.Rental.Update(rental);
            await _unitOfWork.CommitAsync();

            return Accepted(rental);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _unitOfWork.Rental.DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                return NoContent();
            }
            catch
            {
                return NotFound(id);
            }
        }
    }
}