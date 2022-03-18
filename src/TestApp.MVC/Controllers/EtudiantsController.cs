using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ApplicationCore.Entites;
using TestApp.ApplicationCore.Interfaces;


namespace TestApp.MVC.Controllers
{
    public class EtudiantsController : Controller
    {
        private readonly IEtudiantsService _etudiantsService;

        public EtudiantsController(IEtudiantsService etudiantsService)
        {
            _etudiantsService = etudiantsService;
        }
        public async Task<ActionResult> Index(int? nbElement)
        {
            var etudiants = await _etudiantsService.ObtenirToutAsync();

            if(nbElement.HasValue)
                etudiants = etudiants.Take(nbElement.Value);
            //On retourne la liste des étudiants dans l'index
            return View(etudiants);
        }

        // GET: EtudiantsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //On obtient l'étudiant sélectionné  
            var etudiant = await _etudiantsService.ObtenirSelonIdAsync(id);

            //Si l'étudiant n'a pas été trouvé, on retourne un notfound
            if (etudiant == null)
                return NotFound();

            //Si l'etudiant a été trouvé, on passe le modèle étudiant à la vue pour affichage
            return View(etudiant);
        }

        // GET: EtudiantsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EtudiantsController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
               await _etudiantsService.AjouterAsync(etudiant);
                return RedirectToAction(nameof(Index));
            }

            return View(etudiant);
        }

        // GET: EtudiantsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var etudiant = await _etudiantsService.ObtenirSelonIdAsync(id);

            if (etudiant == null)
                return NotFound();

            return View(etudiant);
        }

        // POST: EtudiantsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Etudiant etudiant)
        {
            if (ModelState.IsValid)
            {
               await _etudiantsService.ModifierAsync(etudiant);
                return RedirectToAction(nameof(Index));
            }

            return View(etudiant);
        }

        // GET: EtudiantsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var etudiant = await _etudiantsService.ObtenirSelonIdAsync(id);

            if (etudiant == null)
                return NotFound();

            return View(etudiant);
        }

        // POST: EtudiantsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
