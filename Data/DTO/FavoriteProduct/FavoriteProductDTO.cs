using Data.DTO.BaseDTO;


namespace Data.DTO.FavoriteProductDTO
{
    public class FavoriteProductDTO : BaseDto<FavoriteProductDTO, Entites.Entities.FavoriteProduct.FavoriteProduct, int>
    {
      

        public int productId { get; set; }
        public int UserId { get; set; }



    }
}