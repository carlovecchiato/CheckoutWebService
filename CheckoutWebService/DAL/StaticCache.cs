using CheckoutWebService.BAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CheckoutWebService.DAL
{
    [System.ComponentModel.DataObject]
    public class StaticCache
    {
        public static void LoadStaticCache()
        {
            HttpContext.Current.Application["key"] = ShoppingListRepository.InitialDataTest;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static Dictionary<string, int> GetDrinks()
        {
            return HttpContext.Current.Application["key"] as Dictionary<string, int>;
        }
    }
}