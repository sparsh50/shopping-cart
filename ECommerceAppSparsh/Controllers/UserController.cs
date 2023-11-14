using ECommerceAppSparsh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerceAppSparsh.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7289/api");
        private readonly HttpClient _client;

        public UserController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public IActionResult Index(string searchString, int pageNo = 1)
        {

            if (searchString != null)
            {
                searchString = searchString.Trim();
                pageNo = 1;
            }
            try
            {
                ViewBag.SearchString = searchString;
                ViewBag.PageNo = pageNo;
                int pageSize = 4;
                string accessToken = HttpContext.Session.GetString("AccessToken");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/Index?pageNo=" + pageNo + "&searchString=" + searchString).Result;
                Tuple<List<ProductViewModel>, int> tupleData;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    tupleData = JsonConvert.DeserializeObject<Tuple<List<ProductViewModel>, int>>(data);
                    GetCartItems();
                    int totalProducts = tupleData.Item2;
                    if (totalProducts % pageSize == 0)
                    {
                        ViewBag.TotalPages = (totalProducts / pageSize);
                    }
                    else
                    {
                        ViewBag.TotalPages = (totalProducts / pageSize) + 1;
                    }
                    return View(tupleData.Item1);
                }
            }
            catch
            {
                return View();
            }
            return View();
        }
       

        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            ProductViewModel product = new ProductViewModel();
            string accessToken = HttpContext.Session.GetString("AccessToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/GetById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(ProductViewModel product)
        {

            string data = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            string accessToken = HttpContext.Session.GetString("AccessToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/User/AddProductToCart", content).Result;
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index");
            }
            return View(product);

        }
        [HttpGet]
        public IActionResult Cart()
        {

            List<CartItem> cartItems = GetCartItems();
            if (cartItems.Count == 0)
            {
                return RedirectToAction("EmptyCart");
            }
            return View(cartItems);

        }
        [HttpGet]
        public IActionResult EmptyCart()
        {
            return View();
        }
        private List<CartItem> GetCartItems()
        {
            string accessToken = HttpContext.Session.GetString("AccessToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/GetCartItems").Result;
            List<int> cartItemsId = new List<int>();
            List<CartItem> cartItems = new List<CartItem>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(data);
                if (cartItems.Count == 0) return cartItems;
                foreach (var item in cartItems)
                {
                    cartItemsId.Add(item.ProductId);
                }

                ViewBag.CartProductIds = cartItemsId;
                return cartItems;
            }
            return cartItems;
        }
            
        [HttpGet]
        public IActionResult RemoveCartItem(int id)
        {
            string accessToken = HttpContext.Session.GetString("AccessToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/User/RemoveCartItem/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public IActionResult CheckOut()
        {
            string accessToken = HttpContext.Session.GetString("AccessToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User/CheckOut").Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Order Successfully Placed";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Not Ordered";
            return RedirectToAction("Cart");
        }
    }
}
