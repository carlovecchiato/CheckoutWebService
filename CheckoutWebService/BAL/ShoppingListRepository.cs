using CheckoutWebService.DAL;
using CheckoutWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckoutWebService.BAL
{
    public class ShoppingListRepository
    {
        static public Dictionary<string, int> InitialDataTest =
            new Dictionary<string, int>
        {
            { "pepsi" , 10},
            { "coke" , 11},
            { "gin" , 12},
            { "vodka" , 13}
        };

        public Dictionary<string, int> List
        {
            get
            {
                return StaticCache.GetDrinks();
            }
        }

        /// <summary>
        /// Add a new item in the list. If already existing update the quantity ADDING the new value
        /// </summary>
        /// <param name="key">It is not case sensitive</param>
        /// <param name="quantity">This value will be added to the previous one (It will not replace the old one)</param>
        /// <returns>The new quantity</returns>
        public int Add(string key, int quantity)
        {
            //to avoid the madness of upper/lower case
            string lowerKey = key.ToLower();

            if (List.Keys.Contains(lowerKey))
            {
                List[lowerKey] += quantity;
            }
            else
            {
                List.Add(lowerKey, quantity);
            }

            return List[lowerKey];
        }

        public int Add(Drink item)
        {
            return Add(item.Name, item.Quantity);
        }

        /// <summary>
        /// Update the item quantity.
        /// </summary>
        /// <param name="key">It is not case sensitive</param>
        /// <param name="quantity">This value will replace the old one</param>
        /// <returns>True if item updated, false otherwise (item does not exist)</returns>
        public bool Update(string key, int quantity)
        {
            //to avoid the madness of upper/lower case
            string lowerKey = key.ToLower();

            if (List.Keys.Contains(lowerKey))
            {
                List[lowerKey] = quantity;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Drink item)
        {
            return Update(item.Name, item.Quantity);
        }

        /// <summary>
        /// Delete the item
        /// </summary>
        /// <param name="key">It is not case sensitive</param>
        /// <returns>True if item deleted, false otherwise (item does not exist)</returns>
        public bool Delete(string key)
        {
            //to avoid the madness of upper/lower case
            string lowerKey = key.ToLower();

            if (List.Keys.Contains(lowerKey))
            {
                List.Remove(lowerKey);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <param name="name">It is not case sensitive</param>
        /// <returns>Return the drink with the provided name</returns>
        public Drink GetDrink(string name)
        {
            //to avoid the madness of upper/lower case
            string lowerName = name.ToLower();

            if (List.Keys.Contains(lowerName))
            {
                return new Drink
                {
                    Name = lowerName,
                    Quantity = List[lowerName]
                };
            }
            else
            {
                return null;
            }
        }
    }
}