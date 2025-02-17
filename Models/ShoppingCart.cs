using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment9.Models
{
    public class ShoppingCart
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ProfileId { get; set; }  // Links to Profile
        public int ItemId { get; set; }     // Links to ShoppingItem
        public int Quantity { get; set; }   // Number of items added to the cart
    }
}
