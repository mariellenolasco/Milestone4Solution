using Lodging.Models;
using System.Threading.Tasks;

namespace Lodging.DataAccess.Repositories
{
    /// <summary>
    /// Upon construction creates repositories for the LodgingModel, RentalModel, and ReviewModel from the general repo class
    /// </summary>
    public class UnitOfWork
    {
        private readonly LodgingContext _context;

        public virtual Repository<LodgingModel> Lodging { get; }
        public virtual Repository<RentalModel> Rental { get; set; }
        public virtual Repository<ReviewModel> Review { get; set; }

        public UnitOfWork(LodgingContext context)
        {
            _context = context;

            Lodging = new Repository<LodgingModel>(context);
            Rental = new Repository<RentalModel>(context);
            Review = new Repository<ReviewModel>(context);
        }

        /// <summary>
        /// Represents the _UnitOfWork_ `Commit` method
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync() => await _context.SaveChangesAsync();
    }
}