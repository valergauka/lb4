using lb4.Models;
using Microsoft.AspNetCore.Mvc;

namespace lb4.Controllers
{
    public class AbonentController : Controller
    {
        ConversationContext _context;
        public AbonentController(ConversationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Abonent> abonents = _context.Abonents;
            return View(abonents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Abonent abonent)
        {

            if (ModelState.IsValid)
            {
                await _context.Abonents.AddAsync(abonent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(abonent);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonent = await _context.Abonents.FindAsync(id);

            if (abonent == null)
            {
                return NotFound();
            }

            return View(abonent);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Abonent abonent)
        {
            if (ModelState.IsValid)
            {
                _context.Abonents.Update(abonent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(abonent);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonent = await _context.Abonents.FindAsync(id);

            if (abonent == null)
            {
                return NotFound();
            }

            return View(abonent);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAbonent(int? id)
        {
            var abonent = await _context.Abonents.FindAsync(id);

            if (abonent == null)
            {
                return NotFound();
            }
            _context.Abonents.Remove(abonent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
