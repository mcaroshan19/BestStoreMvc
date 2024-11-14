using BestStoreMvc.Database;
using BestStoreMvc.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BestStoreMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _Context;
        private readonly IWebHostEnvironment _environment;
        public ProductController(AppDbContext Context, IWebHostEnvironment environment)
        {

            _Context = Context;
            _environment=environment;
        }



        public IActionResult Index()
        {

            //var products = _Context.Products.ToList();
            //return View(products);

            var products = _Context.Products.Include(p => p.City).ToList();
            return View(products);


        }


        public IActionResult Create()
        {
            ViewBag.Cities = _Context.Cities.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDto productDto)

        {
            if (productDto.ImageFile==null)
            {
                ModelState.AddModelError("ImageFile", "The Image File is required");

            }
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }
            //new changes 

            if (!_Context.Cities.Any(c => c.CityId == productDto.CityId))
            {
                ModelState.AddModelError("CityId", "Invalid City selected.");
                return View(productDto); // Return if CityId is invalid
            }

            //Save the Image file

            string newFileName = DateTime.Now.ToString("yyyyyMMddHHmmssfff");
            newFileName = Path.GetExtension(productDto.ImageFile!.FileName);

            string imageFullpath = _environment.WebRootPath+ "/Products" + newFileName;
            using (var stream = System.IO.File.Create(imageFullpath))
            {
                productDto.ImageFile.CopyTo(stream);

            }


            //Save the Product in database 

            Product product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Price= productDto.Price,
                Description = productDto.Description,
                
                ImageFileName= newFileName,
                CreatedAt = DateTime.Now,
                CityId = productDto.CityId
            };
            _Context.Products.Add(product);
            _Context.SaveChanges();





            return RedirectToAction("Index", "Product");
        }


        public IActionResult Edit(int id)
        {
            var product = _Context.Products.Include(p => p.City).FirstOrDefault(p => p.Id == id);
            //var product = _Context.Products.Find(id);
            if (product==null)
            {
                return RedirectToAction("Index", "Product");
            }

            //Create Product Dto  From Product

            var productDto = new ProductDto
            {
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
                CityId = product.CityId

            };
            ViewData["ProductId"]=product.Id;
            ViewData["ImageFileName"]=product.ImageFileName;
            ViewData["CreatedAt"]=product.CreatedAt.ToString("MM/dd/yyyy");

            // Fetch a list of cities to show in the dropdown list
            ViewData["Cities"] = _Context.Cities.ToList();

            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductDto productDto)
        {
            var product = _Context.Products.Find(id);
            if (product==null)
            {
                return RedirectToAction("Index", " Product");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ProductId"]=product.Id;
                ViewData["ImageFileName"]=product.ImageFileName;
                ViewData["CreatedAt"]=product.CreatedAt.ToString("MM/dd/yyyy");


                return View(productDto);
            }


            //update image if you have new img  file 
            string newFileName = product.ImageFileName;
            if (productDto.ImageFile !=null)
            {
                newFileName = DateTime.Now.ToString("yyyyyMMddHHmmssfff");
                newFileName = Path.GetExtension(productDto.ImageFile!.FileName);
                string imageFullpath = _environment.WebRootPath+ "/Products" + newFileName;


                using (var stream = System.IO.File.Create(imageFullpath))
                {
                    productDto.ImageFile.CopyTo(stream);

                }
                //delete Old PAth
                string oldImagePath = _environment.WebRootPath+ "/Products" +product.ImageFileName;
                System.IO.File.Delete(oldImagePath);
            }


            product.Name = productDto.Name;
            product.Brand = productDto.Brand;
            product.Category = productDto.Category;
            product.Price= productDto.Price;
            product.Description = productDto.Description;
            product.ImageFileName= newFileName;
            product.CityId = productDto.CityId;
            _Context.SaveChanges();
            return RedirectToAction("Index", "Product");
        }


        public IActionResult Delete(int id)
        {
            var product = _Context.Products.Include(p => p.City).FirstOrDefault(p => p.Id == id);

            if (product== null)
            {
                return RedirectToAction("Index", "Product");
            }

            string imageFullPath = _environment.WebRootPath+ "/Products" +product.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            _Context.Products.Remove(product);
            _Context.SaveChanges(true);
            return RedirectToAction("Index", "Product");
        }



        //[HttpGet]
        //public JsonResult SearchCities(string term)
        //{
        //    var cities = _Context.Cities
        //        .Where(c => c.CityName.Contains(term))
        //        .Select(c => new { id = c.CityId, text = c.CityName })
        //        .ToList();

        //    return Json(cities);
        //}

    }
}