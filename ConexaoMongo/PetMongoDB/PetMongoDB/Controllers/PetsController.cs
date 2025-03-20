using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using PetMongoDB.Data;
using PetMongoDB.Models;

namespace PetMongoDB.Controllers
{
    public class PetsController : Controller
    {
        //criando uma vpáriavel de contexto 
        ContextMongoDB _context = new ContextMongoDB();

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            //mudando depois do find
            return View(await _context.Pet.Find(u => true).ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //mudanças para que ele procure na coleçõa um ID
            var pet = await _context.Pet
                .Find(m => m.Id == id).FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,Raca,Cuidador,Celular")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                
                pet.Id = Guid.NewGuid();
                //mudança feita: retirou as duas linhas do meio e inseriu uma nova linha
                await _context.Pet.InsertOneAsync(pet);
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //substituindo a sentença antiga para essa nova(copiada do Details)
            var pet = await _context.Pet
               .Find(m => m.Id == id).FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Idade,Raca,Cuidador,Celular")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //mudança feita, mandando ir na coleção pet e achar o documentos em que o id do pet bata com este ID
                    await _context.Pet.ReplaceOneAsync(m => m.Id == pet.Id, pet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
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
            return View(pet);
        }

        // GET: Pets/Delete/5
        //buscando o que vocês querem deletar
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //mudança feita colando o mesmo feito no details
            var pet = await _context.Pet
                 .Find(m => m.Id == id).FirstOrDefaultAsync();
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //mudança feita
            await _context.Pet.DeleteOneAsync(u => u.Id == id);
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(Guid id)
        {
            //mudança feita
            return _context.Pet.Find(e => e.Id == id).Any();
        }
    }
}
