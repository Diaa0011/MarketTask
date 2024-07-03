using AutoMapper;
using Market.Dtos.Store;
using Market.Models;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StoreController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var Stores = await _unitOfWork.Stores.GetAllAsync();

            return Ok(_mapper.Map<List<StoreReadDto>>(Stores));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            var Store = await _unitOfWork.Stores.GetByIdAsync(id);

            return Ok(_mapper.Map<StoreReadDto>(Store));
        }

        [HttpPost]
        public async Task<IActionResult> createStore(StoreCreateDto store)
        {
            if(store is null)
            {
                return NoContent();
            }

            var newStore = _mapper.Map<Store>(store);
            
            await _unitOfWork.Stores.AddAsync(newStore);
            
            _unitOfWork.Save();

            var idOfStore = await _unitOfWork.Stores.FindAsync(c=> c.Name == newStore.Name);

            return CreatedAtAction(nameof(GetStoreById), new { id = newStore.Id }, newStore);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStore(int id,StoreEditDto storeFix)
        {
            var store = await _unitOfWork.Stores.GetByIdAsync(id);

            if (store is null)
            {
                Console.WriteLine("No Stores Found");
                return NotFound();
            }


            _mapper.Map(storeFix, store);

            _unitOfWork.Save();
            Console.WriteLine("---> Deleted Successfully");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var StoreDeleteDto = await _unitOfWork.Stores.GetByIdAsync(id);

            if (StoreDeleteDto is null)
            {
                Console.WriteLine("No Stores Found");
                return NotFound();
            }
            var storeDeletion = _mapper.Map<Store>(StoreDeleteDto);

            _unitOfWork.Stores.Delete(storeDeletion);

            _unitOfWork.Save();

            Console.WriteLine("---> Deleted Successfully");
            return NoContent();
        }


    }
}
