using Lodging.DataAccess.Repositories;
using Lodging.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lodging.WebAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewController : ControllerBase
  {
    private readonly UnitOfWork _unitOfWork;

    public ReviewController(UnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    // GET: api/Review
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
      return Ok(await _unitOfWork.Review.SelectAsync());
    }

    // GET: api/Review/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
      try
      {
        var result = await _unitOfWork.Review.SelectAsync(id);
        if (result == null) return NotFound(id);
        return Ok(result);
      }
      catch
      {
        return StatusCode(500);
      }
    }

    // POST: api/Review
    [HttpPost]
    public async Task<IActionResult> PostAsync(ReviewModel review)
    {
      await _unitOfWork.Review.InsertAsync(review);
      await _unitOfWork.CommitAsync();

      return Created(Url.RouteUrl(review.Id), review.Id);
    }

    // PUT: api/Review/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(ReviewModel review)
    {
      _unitOfWork.Review.Update(review);
      await _unitOfWork.CommitAsync();

      return Accepted(review);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
      try
      {
        await _unitOfWork.Review.DeleteAsync(id);
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