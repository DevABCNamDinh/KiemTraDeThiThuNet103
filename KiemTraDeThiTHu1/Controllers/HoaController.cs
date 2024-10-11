using KiemTraDeThiTHu1.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace KiemTraDeThiTHu1.Controllers
{
    public class HoaController : Controller
    {
        ThucVatDB2Context _thucvatdbcontext;
        public HoaController()
        {
            _thucvatdbcontext = new ThucVatDB2Context();
        }
        public IActionResult Index()
        {
            var hoa = _thucvatdbcontext.Hoas.ToList();
            return View(hoa);
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
            var thucVatList = _thucvatdbcontext.ThucVats.ToList();
            ViewBag.ThucVatId = new SelectList(thucVatList, "ThucVatID", "TenThucVat");
            return View(hoa);
        }
        [HttpPost]
        public IActionResult Update(Hoa hoa)
        {
            try
            {
                _thucvatdbcontext.Hoas.Update(hoa);
                _thucvatdbcontext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
            

        }
        public IActionResult Details(int id) {
            var hoa = _thucvatdbcontext.Hoas.Find(id);

            return View(hoa);
        }
        public IActionResult Delete(int id)
        {
            var hoa = _thucvatdbcontext.Hoas.Find(id);
            string jsonData = JsonConvert.SerializeObject(hoa); // Mã hóa về sạng chuỗi Json
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
                var deletedItem = JsonConvert.DeserializeObject<Hoa>(sessionData);
                Hoa rollBackItem = new Hoa()
                {
                    TenHoa = deletedItem.TenHoa,
                    ThucVatId = deletedItem.ThucVatId,
                  
                };
                _thucvatdbcontext.Hoas.Add(rollBackItem);
                _thucvatdbcontext.SaveChanges();
                HttpContext.Session.Remove("deleted"); // Xóa Session sau khi rollback
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
