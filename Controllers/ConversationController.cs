using lb4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lb4.Controllers
{
    public class ConversationController : Controller
    {
        ConversationContext _context;
        public ConversationController(ConversationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Conversation> conversations = _context.Conversations.Include(prop => prop.Abonent).Include(prop => prop.City);
            return View(conversations);
        }

        public IActionResult Create()
        {
            ViewBag.Abonents = new SelectList(_context.Abonents, "Id", "FirstName");
            ViewBag.Cities = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Conversation conversation)
        {
            if (conversation.AbonentId != 0 && conversation.CityId != 0)
            {
                await _context.Conversations.AddAsync(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Abonents = new SelectList(_context.Abonents, "Id", "FirstName");
            ViewBag.Cities = new SelectList(_context.Cities, "Id", "Name");

            return View(conversation);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = _context.Conversations.Include(p => p.Abonent).Include(p => p.City).First(p => p.Id == id);

            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConversation(int? id)
        {
            var conversation = await _context.Conversations.FindAsync(id);

            if (conversation == null)
            {
                return NotFound();
            }
            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
