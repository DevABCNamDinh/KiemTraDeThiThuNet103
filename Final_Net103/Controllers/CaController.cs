using Final_Net103.Context;
using Final_Net103.DomainClass;
using Final_Net103.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Final_Net103.Controllers
{
    public class CaController : Controller
    {
        MyContext2 _context;
        CaService _caService;
        public CaController(MyContext2 context2,CaService caService)
        {
            _context = context2;
            _caService = caService;
        }
        public IActionResult Index()
        {
            var cas= _context.Cas.ToList();

            return View(cas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Ca ca)
        {
            _context.Cas.Add(ca);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var caEdit = _context.Cas.Find(id);
            var JsonEdit = JsonConvert.SerializeObject(caEdit);
            HttpContext.Session.SetString($"{id}", JsonEdit);
            return View(caEdit);
        }
        [HttpPost]
        public IActionResult Edit(Ca ca)
        {
            var caEdit = _context.Cas.Find(ca.Id);
            caEdit.Ten=ca.Ten;
            caEdit.ThucAn=ca.ThucAn;
            caEdit.TapTinh=ca.TapTinh;
            caEdit.Idca=ca.Idca;
            
            _context.Cas.Update(caEdit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var caList = _caService.CaList;
            var caEdit = _context.Cas.Find(id);
            caList.Add(caEdit);
            var caJson = JsonConvert.SerializeObject(caList);
            HttpContext.Session.SetString("List", caJson);
            _context.Cas.Remove(caEdit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var caEdit = _context.Cas.Find(id);

            return View(caEdit);
        }

        public IActionResult RoleBack()
        {
            var caJson = HttpContext.Session.GetString("List");
            if (string.IsNullOrEmpty(caJson))
            {
                return Content("Không tìm thấy data vừa bị xóa");
            }
            else
            {

                var deletedItem = JsonConvert.DeserializeObject<List<Ca>>(caJson);
                foreach (var item in deletedItem)
                {
                    Ca rollBackItem = new Ca()
                    {
                       Ten=item.Ten,
                       ThucAn=item.ThucAn,
                       TapTinh=item.TapTinh,
                       Idca=item.Idca,


                    };
                    _context.Cas.Add(rollBackItem);
                    _context.SaveChanges();
                }

                HttpContext.Session.Remove("List"); // Xóa Session sau khi rollback
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult RoleBackEdit(int id)
        {
            var caJson = HttpContext.Session.GetString($"{id}");
            if (string.IsNullOrEmpty(caJson))
            {
                return Content("Không tìm thấy data vừa bị xóa");
            }
            else
            {
                var caBack = JsonConvert.DeserializeObject<Ca>(caJson);
                var ca = _context.Cas.Find(caBack.Id);
                ca.Ten= caBack.Ten;
                ca.ThucAn= caBack.ThucAn;
                ca.TapTinh= caBack.TapTinh;
                ca.Idca= caBack.Idca;
                _context.Cas.Update(ca);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
