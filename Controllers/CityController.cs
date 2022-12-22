using lb4.Models;
using Microsoft.AspNetCore.Mvc;

namespace lb4.Controllers
{
    public class CityController : Controller
    {
        ConversationContext _context;
        public CityController(ConversationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<City> cities = _context.Cities;
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {

            if (ModelState.IsValid)
            {
                await _context.Cities.AddAsync(city);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(city);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(City city)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Update(city);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(city);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCity(int? id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
