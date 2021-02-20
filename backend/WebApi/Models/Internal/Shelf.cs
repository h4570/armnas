using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Internal
{
    public class Shelf
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Barcode { get; set; }
        public IEnumerable<ShelfCategory> Categories { get; set; }

        public void Update(Shelf shelf)
        {
            Barcode = shelf.Barcode;
        }

    }
}
