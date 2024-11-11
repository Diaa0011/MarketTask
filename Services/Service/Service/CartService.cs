using AutoMapper;
using Market.Dtos.Cart;
using Market.Services.Repository.IRepository;
using Market.Services.Service.IService;

namespace Market.Services.Service.Service
{
    public class CartService:ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CartReadDto> GetCart(string clientId)
        {
            var client = await _unitOfWork.Clients.FindAsync(u => u.Id == clientId);
            if(client == null)
            {
                return null;
            }
            var gottenCart = await _unitOfWork.Carts.GetCartByClientIdAsync(clientId);
            var cartId = gottenCart.CartId;
            var cart = await _unitOfWork.Carts.GetByIdAsync(cartId);
            var cartReadDto = _mapper.Map<CartReadDto>(cart);
            return cartReadDto;
        }

        public Task<bool> RemoveCart()
        {
            throw new NotImplementedException();

    }
    }
}
