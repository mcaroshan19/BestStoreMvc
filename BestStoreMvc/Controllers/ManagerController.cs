
using BestStoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using BestStoreMvc.Database;
using System.Xml.Linq;



namespace BestStoreMvc.Controllers
{
    public class ManagerController : Controller
    {
        //private readonly ApplicationDbContext _context;
        //private readonly IWebHostEnvironment Environment;
        //public ManagerController(ApplicationDbContext contex, IWebHostEnvironment environment)
        //{

        //    _context = contex;
        //    Environment = environment;

        //}

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment Environment;
        public ManagerController(AppDbContext context, IWebHostEnvironment environment)
        {

            _context = context;
            Environment = environment;
        }







        public IActionResult Index()
        {
            var manager = _context.Managers?.ToList() ?? new List<Manager>();

            //var manager= _context.Managers.ToList();
            return View(manager);
        }

        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Create(ManagerDto managerDto)
        {
            if (_context.Managers.Count() >= 10)
            {
                
                ModelState.AddModelError(string.Empty, "Cannot add more than 10 managers.");
                return View(managerDto);
            }



            if (managerDto.FileName==null)
            {
                ModelState.AddModelError("Add file", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                return View(managerDto);
            }



            string Newfilename = DateTime.Now.ToString("yyyyMMddHHssfff");

            Newfilename = Path.GetExtension(managerDto.FileName.FileName!);

            string imageFullpath = Environment.WebRootPath + "/" + Newfilename;

            using (var stream = System.IO.File.Create(imageFullpath))
            {
                managerDto.FileName.CopyTo(stream);
            }

            var manager = new Manager()
            {
                Name=managerDto.Name,
                Category=managerDto.Category,
                Freshness=managerDto.Freshness,
                ImagefileName=Newfilename,
                Description=managerDto.Description,
                Comments=managerDto.Comments,
                Price=managerDto.Price,
                Email=managerDto.Email,
                PhoneNumber=managerDto.PhoneNumber,





            };



            _context.Managers.Add(manager);
            _context.SaveChanges();




            return RedirectToAction("Index", "Manager");

        }
    }
}

