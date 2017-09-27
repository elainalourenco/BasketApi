using ShoppingCart.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Api.Repositories
{
    public class BasketRepository : IRepository<Basket>
    {
        private Dictionary<int, Basket> dataSource;

        public BasketRepository()
        {
            this.dataSource = new Dictionary<int, Basket>();
        }

        public Basket Get(int id)
        {
            Basket result = null;
            this.dataSource.TryGetValue(id, out result);
            return result;
        }

        public Basket InsertOrUpdate(Basket item)
        {
            Basket result = this.Get(item.Id);
            if (result == null)
            {
                item.Id = this.dataSource.Count + 1;
                this.dataSource.Add(item.Id, item);
            }
            else
            {
                this.dataSource[item.Id] = item;
            }

            result = item;

            return result;
        }


        public void Delete(int id)
        {
            this.dataSource.Remove(id);
        }
    }
}