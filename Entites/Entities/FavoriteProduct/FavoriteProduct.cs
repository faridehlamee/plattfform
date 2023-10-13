using Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entites.Entities.FavoriteProduct
{
    [Table("Favorite", Schema = "PRO")]
    public class FavoriteProduct : BaseEntity<int>, IEntity<int>
    {
        public int productId { get; set; }
        public Product.Product product { get; set; }
        public int UserId { get; set; }
        public User.User User { get; set; }

    }
}