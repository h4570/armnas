using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{
    public class ShelfCategory
    {
        public int Id { get; set; }
        public int ShelfId { get; set; }
        public Shelf Shelf { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
