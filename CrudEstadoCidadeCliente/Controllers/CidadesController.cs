using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudEstadoCidadeCliente.Data;
using CrudEstadoCidadeCliente.Models;

namespace CrudEstadoCidadeCliente.Controllers
{
    public class CidadesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CidadesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cidades
        public async Task<IActionResult> Index()
        {
            var cidades = await _context.Cidades
                .Include(c => c.Estado)
                .OrderBy(c => c.Estado.Nome)
                .ThenBy(c => c.Nome)
                .ToListAsync();

            return View(cidades);
        }

        // GET: Cidades/Create
        public IActionResult Create()
        {
            var estados = _context.Estados.OrderBy(e => e.Nome).ToList();

            if (!estados.Any())
            {
                TempData["AlertMessage"] = "Cadastre pelo menos um estado antes de criar cidades.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EstadoId"] = new SelectList(estados, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cod,Nome,EstadoId")] Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cidade);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cidade cadastrada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados.OrderBy(e => e.Nome), "Id", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidades.FindAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }

            ViewData["EstadoId"] = new SelectList(_context.Estados.OrderBy(e => e.Nome), "Id", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cod,Nome,EstadoId")] Cidade cidade)
        {
            if (id != cidade.Cod)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cidade);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cidade atualizada com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CidadeExists(cidade.Cod))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoId"] = new SelectList(_context.Estados.OrderBy(e => e.Nome), "Id", "Nome", cidade.EstadoId);
            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidade = await _context.Cidades
                .Include(c => c.Estado)
                .FirstOrDefaultAsync(m => m.Cod == id);

            if (cidade == null)
            {
                return NotFound();
            }

            return View(cidade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cidade = await _context.Cidades
                .Include(c => c.Clientes)
                .FirstOrDefaultAsync(c => c.Cod == id);

            if (cidade == null)
            {
                return NotFound();
            }

            if (cidade.Clientes?.Any() == true)
            {
                TempData["ErrorMessage"] = "Não é possível excluir esta cidade porque existem clientes vinculados a ela.";
                return View("Delete", cidade);
            }

            _context.Cidades.Remove(cidade);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cidade excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool CidadeExists(int id)
        {
            return _context.Cidades.Any(e => e.Cod == id);
        }
    }
}