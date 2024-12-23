using System.Security.Claims;
using AutoMapper;
using Market.Data;
using Market.Dtos.Store;
using Market.Model;
using Market.Services.Repository.IRepository;
using Market.Services.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Authorize(Roles = "merchant")]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IStoreService _storeService;
        public StoreController(IUnitOfWork unitOfWork,
         IMapper mapper,
         IStoreService storeService,
        AppDbContext context
         )
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _storeService = storeService ??
                throw new ArgumentNullException(nameof(storeService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStores()
        {
            var user_Id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (user_Id == null)
            {
                return Unauthorized("User not authenticated.");
            }

            var stores_fetch = await _storeService.GetAllStoresForMerchant(user_Id);

           return Ok(stores_fetch);
        }
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetStoreById(int id)
        {

            var user_Id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (user_Id == null)
            {
                return Unauthorized("User not authenticated.");
            }

            var store = await _storeService.GetStoreByIdWithProductsAsync(id, user_Id);

            return Ok(store);
        }
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetStoreByName(string name)
        {

            var user_Id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (user_Id == null)
            {
                return Unauthorized("User not authenticated.");
            }

            var store = await _storeService.FindByNameAsync(name, user_Id);

            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> createStore(StoreCreateDto store)
        {
            if (store is null)
            {
                return BadRequest("Store information is required.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }

            var newStore = await _storeService.CreateStoreAsync(store, userId);

            if (newStore == null)
            {
                return Problem("Failed to retrieve created store.");
            }

            return CreatedAtAction(nameof(GetStoreById),
             new { id = newStore.Id },newStore);
        }

        [HttpPatch("id/{id}")]
        public async Task<IActionResult> updateStore(int id, JsonPatchDocument<StoreEditDto> patchDoc)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }
            var store = await _storeService.UpdateStoreAsync(id,patchDoc, userId);

            if (store is null)
            {
                Console.WriteLine("No Stores Found");
                return NotFound();
            }

            Console.WriteLine("---> Store Edited Successfully");
            return NoContent();
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return Unauthorized("User not authenticated.");
            }
            var storeDeletion = _storeService.DeleteStoreAsync(id, userId);
            if (!storeDeletion.Result)
            {
                Console.WriteLine("No Stores Found");
                return NotFound();
            }
    
            Console.WriteLine("--->Store Deleted Successfully [Controller Check]");
            return NoContent();
        }


    }
}
