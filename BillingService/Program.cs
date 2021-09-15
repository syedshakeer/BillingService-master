using System;
using System.Collections.Generic;
using System.Linq;
namespace BillingService
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading!");
            List<MenuModel> menuModel = SetMenuData();
            var customerOrder = customerPurchaseItems();
            var Orderresult= CalculateBill(customerOrder, menuModel);
            foreach(var result in Orderresult)
            {
                Console.WriteLine("Name:{0}, Quantity:{1}, Price:{2}, serviceCharge:{3}, TotalPrice:{4}", result.name, result.quantity, result.price, result.serviceCharge, result.TotalPrice);
            }
            Console.ReadLine();
           
        }

        static List<MenuModel> SetMenuData()
        {
            List<MenuModel> billingModel = new List<MenuModel>();
            billingModel.Add(new MenuModel() { ItemID=1, coffeeType = "drink", name = "Cola", price = 0.50 });
            billingModel.Add(new MenuModel() {ItemID=2, coffeeType = "drink", name = "Coffee", price = 1.00 });
            billingModel.Add(new MenuModel() {ItemID=3, coffeeType = "food", name = "Cheese Sandwich", price = 2.00 });
            billingModel.Add(new MenuModel() {ItemID=4, coffeeType = "food", name = "Steak Sandwich", price = 4.50 });
            return billingModel;
        }

        //customer placed  order.
        static List<CustomerPurchasedModel> customerPurchaseItems()
        {
            List<CustomerPurchasedModel> order = new List<CustomerPurchasedModel>();
            order.Add(new CustomerPurchasedModel() { itemId = 1, quantity = 5 });
            order.Add(new CustomerPurchasedModel() { itemId = 2, quantity = 3 });
            order.Add(new CustomerPurchasedModel() { itemId = 3, quantity = 5 });
            order.Add(new CustomerPurchasedModel() { itemId = 4, quantity = 10 });
            return order;
        }



        static dynamic CalculateBill(List<CustomerPurchasedModel> orderModel, List<MenuModel> menuModel)
        {
            var result = from order in orderModel
                         join menu in menuModel
                         on order.itemId equals menu.ItemID
                         let totalPrice = menu.coffeeType == "drink" ? 0 : (decimal)((order.quantity * menu.price) + (0.1 * (order.quantity * menu.price)))
                         let _serviceCharge = menu.coffeeType == "drink" ? "0" :"10%"
                         select new { order.itemId, order.quantity, menu.coffeeType, menu.name, menu.price, serviceCharge = _serviceCharge , TotalPrice = totalPrice };
            return result;

        }


    }
    

   
}
