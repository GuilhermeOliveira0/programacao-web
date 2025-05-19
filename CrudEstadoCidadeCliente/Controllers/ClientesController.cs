using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudEstadoCidadeCliente.Data;
using CrudEstadoCidadeCliente.Models;

namespace CrudEstadoCidadeCliente.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Cidade)
                .ThenInclude(c => c.Estado)
                .OrderBy(c => c.Nome)
                .ToListAsync();

            return View(clientes);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            var cidades = _context.Cidades
                .Include(c => c.Estado)
                .OrderBy(c => c.Estado.Nome)
                .ThenBy(c => c.Nome)
                .Select(c => new {
                    c.Cod,
                    NomeCompleto = $"{c.Nome} - {c.Estado.Sigla}"
                })
                .ToList();

            if (!cidades.Any())
            {
                TempData["AlertMessage"] = "Cadastre pelo menos uma cidade antes de criar clientes.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["CidadeId"] = new SelectList(cidades, "Cod", "NomeCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cod,Nome,Sexo,Idade,DataNascimento,CidadeId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            // Recarrega as cidades se houver erro de validação
            var cidades = _context.Cidades
                .Include(c => c.Estado)
                .OrderBy(c => c.Estado.Nome)
                .ThenBy(c => c.Nome)
                .Select(c => new {
                    c.Cod,
                    NomeCompleto = $"{c.Nome} - {c.Estado.Sigla}"
                })
                .ToList();

            ViewData["CidadeId"] = new SelectList(cidades, "Cod", "NomeCompleto", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            var cidades = _context.Cidades
                .Include(c => c.Estado)
                .OrderBy(c => c.Estado.Nome)
                .ThenBy(c => c.Nome)
                .Select(c => new {
                    c.Cod,
                    NomeCompleto = $"{c.Nome} - {c.Estado.Sigla}"
                })
                .ToList();

            ViewData["CidadeId"] = new SelectList(cidades, "Cod", "NomeCompleto", cliente.CidadeId);
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cod,Nome,Sexo,Idade,DataNascimento,CidadeId")] Cliente cliente)
        {
            if (id != cliente.Cod)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Cod))
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

            var cidades = _context.Cidades
                .Include(c => c.Estado)
                .OrderBy(c => c.Estado.Nome)
                .ThenBy(c => c.Nome)
                .Select(c => new {
                    c.Cod,
                    NomeCompleto = $"{c.Nome} - {c.Estado.Sigla}"
                })
                .ToList();

            ViewData["CidadeId"] = new SelectList(cidades, "Cod", "NomeCompleto", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Cidade)
                .ThenInclude(c => c.Estado)
                .FirstOrDefaultAsync(m => m.Cod == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cliente excluído com sucesso!";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Cod == id);
        }
    }
}