using KiemTraDeThiTHu1.models;
using KiemTraDeThiTHu1.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KiemTraDeThiTHu1.Controllers
{
    public class HoaController : Controller
    {
        ThucVatDB2Context _thucvatdbcontext;
        HoaService _hoaService;
        public HoaController(ThucVatDB2Context thucVatDB2Context,HoaService hoaService)
        {
            _thucvatdbcontext = thucVatDB2Context;
                _hoaService = hoaService;
        }
        public IActionResult Index(string name)
        {
            var results = string.IsNullOrEmpty(name)
      ? _thucvatdbcontext.Hoas.ToList()
      : _thucvatdbcontext.Hoas.Where(h => h.TenHoa.Contains(name)).ToList();
            return View(results);
        }

        public IActionResult Create()
        {
            var thucVatList = _thucvatdbcontext.ThucVats.ToList();
            ViewBag.ThucVatId = new SelectList(thucVatList, "ThucVatID", "TenThucVat");

            return View();

        }

        [HttpPost]
        public IActionResult Create(Hoa hoa)
        {
            try
            {
                _thucvatdbcontext.Hoas.Add(hoa);
                _thucvatdbcontext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
           
        }

        public IActionResult Update(int id)
        {

            var hoa = _thucvatdbcontext.Hoas.Find(id);
            var jsonData = JsonConvert.SerializeObject(hoa);
            HttpContext.Session.SetString($"{id}", jsonData);
            return View(hoa);
        }
        [HttpPost]
        public IActionResult Update(Hoa hoa)
        {
            //try
            //{
            var hoaX = _thucvatdbcontext.Hoas.Find(hoa.HoaId);


            hoaX.TenHoa = hoa.TenHoa;
            hoaX.ThucVatId = hoa.ThucVatId;
            

                _thucvatdbcontext.Hoas.Update(hoaX);
                _thucvatdbcontext.SaveChanges();

                return RedirectToAction("Index");
            //}
            //catch (Exception)
            //{

            //    return View();
            //}
            

        }
        public IActionResult Details(int id) {
            var hoa = _thucvatdbcontext.Hoas.Find(id);

            return View(hoa);
        }
       
        public IActionResult Delete(int id)
        {
           
            var hoa = _thucvatdbcontext.Hoas.Find(id);     
            _hoaService.hoas.Add(hoa);
            var Listhoa = _hoaService.hoas;
                HttpContext.Session.Remove("deleted"); // Xóa Session sau khi rollback
                string jsonData = JsonConvert.SerializeObject(Listhoa); // Mã hóa về sạng chuỗi Json
                HttpContext.Session.SetString("deleted", jsonData);

            

            _thucvatdbcontext.Hoas.Remove(hoa);
            _thucvatdbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RollBack() // Add lại vào db đối tượng vừa bị xóa
        {
            var sessionData = HttpContext.Session.GetString("deleted");
            if (string.IsNullOrEmpty(sessionData))
            {
                return Content("Không tìm thấy data vừa bị xóa");
            }
            else
            {
                
                var deletedItem = JsonConvert.DeserializeObject<List<Hoa>>(sessionData);
                foreach (var item in deletedItem)
                {
                    Hoa rollBackItem = new Hoa()
                    {
                        TenHoa = item.TenHoa,
                        ThucVatId = item.ThucVatId,

                    };
                    _thucvatdbcontext.Hoas.Add(rollBackItem);
                    _thucvatdbcontext.SaveChanges();
                }
               
                HttpContext.Session.Remove("deleted"); // Xóa Session sau khi rollback
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult RoleBackEdit(int id)
        {
            var sessionData = HttpContext.Session.GetString($"{id}");
            if (string.IsNullOrEmpty(sessionData))
            {
                return Content("null");
            }
            var deletedItem = JsonConvert.DeserializeObject<Hoa>(sessionData);
            var hoaX = _thucvatdbcontext.Hoas.Find(deletedItem.HoaId);
            hoaX.TenHoa=deletedItem.TenHoa;
            hoaX.ThucVatId=deletedItem.ThucVatId;
            _thucvatdbcontext.Hoas.Update(hoaX);
            _thucvatdbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
