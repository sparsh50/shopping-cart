using ECommerceAppSparsh.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerceAppSparsh.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7289/api");
        private readonly HttpClient _client;

        public AdminController()
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
        public IActionResult Create()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel productModel)
        {
            try
            {
                string accessToken = HttpContext.Session.GetString("AccessToken");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                string data = JsonConvert.SerializeObject(productModel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/Create", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Product is added successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View();
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
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
        public IActionResult Edit(ProductViewModel model)
        {
            try
            {
                ProductViewModel product = new ProductViewModel();
                string accessToken = HttpContext.Session.GetString("AccessToken");
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                int id = model.ProductId;
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Admin/Edit/" + id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
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
        public IActionResult Delete(ProductViewModel productModel)
        {
            string accessToken = HttpContext.Session.GetString("AccessToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Admin/Delete/" + productModel.ProductId).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }

            return View();
        }

    }
}
