using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ktra02_nguyenvantung
{
    public partial class Form1 : Form
    {

        private Cart shoppingCart = new Cart();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedProduct = GetSelectedProduct();
            if (selectedProduct != null)
            {
                shoppingCart.AddProduct(selectedProduct);
                UpdateCartDisplay();
            }
        }

        private void UpdateCartDisplay()
        {
            cartListView.Items.Clear();
            foreach (var product in shoppingCart.GetProducts())
            {
                ListViewItem item = new ListViewItem(new string[] {
                product.Name,
                product.Quantity.ToString(),
                (product.Price * product.Quantity).ToString("C")});
                    cartListView.Items.Add(item);
                item.Tag = product;
            }

            lblTotalQuantity.Text = shoppingCart.GetTotalQuantity().ToString();
            lblTotalAmount.Text = shoppingCart.CalculateTotal().ToString("C");
        }

        private Product GetSelectedCartProduct()
        {
            if (cartListView.SelectedItems.Count > 0)
            {
                // The selected item’s `Tag` is set to the `Product` object in the cart
                return (Product)cartListView.SelectedItems[0].Tag;
            }
            return null;
        }

        private Product GetSelectedProduct()
        {
            if (productListView.SelectedItems.Count > 0)
            {
                // The selected item’s `Tag` is set to the `Product` object
                return (Product)productListView.SelectedItems[0].Tag;
            }
            return null;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            productListView.View = View.Details;
            cartListView.View = View.Details;
            productListView.FullRowSelect = true;
            cartListView.FullRowSelect = true;


            productListView.Columns.Add("Image", 100);
            productListView.Columns.Add("Product Name", 150);
            productListView.Columns.Add("Price", 100);

            cartListView.Columns.Add("Product Name", 150);
            cartListView.Columns.Add("Quantity", 80);
            cartListView.Columns.Add("Total Price", 100);

            List<Product> products = new List<Product>
            {
                new Product("Sản phẩm 1", 100000, 1, null),
                new Product("Sản phẩm 2", 200000, 1, null),
                new Product("Sản phẩm 3", 300000, 1, null)
            };

            foreach (var product in products)
            {
                ListViewItem item = new ListViewItem(new string[] { "", product.Name, product.Price.ToString("C") });
                item.ImageIndex = 0;
                item.Tag = product;
                productListView.Items.Add(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectedProduct = GetSelectedCartProduct();
            if (selectedProduct != null)
            {
                shoppingCart.RemoveProduct(selectedProduct);
                UpdateCartDisplay();
            }
            else
            {
                MessageBox.Show("Please select a product to remove from the cart.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (shoppingCart.GetProducts().Count > 0)
            {
                MessageBox.Show("Thanh toán thành công!");
                shoppingCart.ClearCart();
                UpdateCartDisplay();
            }
            else
            {
                MessageBox.Show("Giỏ hàng trống!");
            }
        }
    }


    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Image Image { get; set; }

        public Product(string name, decimal price, int quantity, Image image)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            Image = image;
        }
    }


    public class Cart
    {
        private Dictionary<String, Product> products = new Dictionary<String, Product>();

        public void AddProduct(Product product)
        {
            if (products.ContainsKey(product.Name))
            {
                // Increase quantity if product is already in cart
                products[product.Name].Quantity += product.Quantity;
            }
            else
            {
                // Add product as a new entry
                products[product.Name] = new Product(product.Name, product.Price, product.Quantity, product.Image);
            }
        }
        public void RemoveProduct(Product product)
        {
            if (products.ContainsKey(product.Name))
            {
                products.Remove(product.Name);
            }
        }
        public decimal CalculateTotal()
        {
            return products.Values.Sum(p => p.Price * p.Quantity);
        }

        public List<Product> GetProducts()
        {
            return products.Values.ToList();
        }

        public int GetTotalQuantity()
        {
            return products.Values.Sum(p => p.Quantity);
        }

        public void ClearCart()
        {
            products.Clear();
        }
    }
}
