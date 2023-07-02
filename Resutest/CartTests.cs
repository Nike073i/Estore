using Estore.BL.Catalog;
using Resutest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Resutest
{
    public class CartTests : BaseTest
    {
        const string TEST_PRODUCT_UNIQUE_ID1 = "c-glazami-hakera";
        const string TEST_PRODUCT_UNIQUE_ID2 = "bibliya-c-5-izd";

        [Test]
        public async Task EmptyCartTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var cartmodel = await _cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(0));
                Assert.That(cartmodel.Items, Is.Empty);
            }
        }

        [Test]
        public async Task AddCartTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var productModel1 = await _product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);
                var productModel2 = await _product.GetProduct(TEST_PRODUCT_UNIQUE_ID2);

                Assert.That(productModel1, Is.Not.Null);
                Assert.That(productModel2, Is.Not.Null);

                var cartmodel = await _cart.GetCurrentUserCart();

                // добавить первый
                await _cart.AddCurrentUserCartProduct(productModel1.Product.ProductId!.Value);

                // проверить
                cartmodel = await _cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));
                Assert.That(cartmodel.Items[0].ProductCount, Is.EqualTo(1));

                // добавить второй
                await _cart.AddCurrentUserCartProduct(productModel1.Product.ProductId!.Value);

                // проверить
                cartmodel = await _cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price * 2));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));
                Assert.That(cartmodel.Items[0].ProductCount, Is.EqualTo(2));

                // добавить ещё товар
                await _cart.AddCurrentUserCartProduct(productModel2.Product.ProductId!.Value);

                // проверить
                cartmodel = await _cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price * 2 + productModel2.Product.Price));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(2));
                Assert.That(cartmodel.Items.First(m => m.ProductId == productModel1.Product.ProductId).ProductCount, Is.EqualTo(2));
                Assert.That(cartmodel.Items.First(m => m.ProductId == productModel2.Product.ProductId).ProductCount, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task UpdateCartTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var productModel1 = await _product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);
                Assert.That(productModel1, Is.Not.Null);
                
                var cartmodel = await _cart.GetCurrentUserCart();

                // добавить 
                await _cart.AddCurrentUserCartProduct(productModel1.Product.ProductId!.Value);

                await _cart.UpdateCurrentUserCartProduct(productModel1.Product.ProductId!.Value, 3);

                // проверить
                cartmodel = await _cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(productModel1.Product.Price * 3));
                Assert.That(cartmodel.Items.Count, Is.EqualTo(1));
                Assert.That(cartmodel.Items[0].ProductCount, Is.EqualTo(3));
            }
        }

        [Test]
        public async Task RemoveCartTest()
        {
            using (var scope = Helper.CreateTransactionScope())
            {
                var productModel1 = await _product.GetProduct(TEST_PRODUCT_UNIQUE_ID1);
                Assert.That(productModel1, Is.Not.Null);

                var cartmodel = await _cart.GetCurrentUserCart();

                // добавить 
                await _cart.AddCurrentUserCartProduct(productModel1.Product.ProductId!.Value);

                await _cart.UpdateCurrentUserCartProduct(productModel1.Product.ProductId!.Value, 0);

                // проверить
                cartmodel = await _cart.GetCurrentUserCart();
                Assert.That(cartmodel.Total, Is.EqualTo(0));
                Assert.That(cartmodel.Items, Is.Empty);
            }
        }
    }
}
