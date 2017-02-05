using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebStock
{
    public partial class Stock
    {
        public int? Sum { get
            {
                using(StockDbEntities db = new StockDbEntities())
                {
                    var q = ProductQuantity;
                    var p = db.Products.First(x => x.Id == ProductId).Price;
                    return q * p;
                }

                    
            } }
    }
}