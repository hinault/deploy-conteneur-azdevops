using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ApplicationCore.Entites;
using TestApp.ApplicationCore.Interfaces;

namespace TestApp.ApplicationCore.Services
{
    public class EtudiantsService : IEtudiantsService
    {

        private readonly IAsyncRepository<Etudiant> _etudiantRepository;

        public EtudiantsService(IAsyncRepository<Etudiant> etudiantRepository)
        {
            _etudiantRepository = etudiantRepository;
        }

        public async Task AjouterAsync(Etudiant etudiant)
        {
            await _etudiantRepository.AddAsync(etudiant);
        }

        public async Task ModifierAsync(Etudiant etudiant)
        {
            await _etudiantRepository.EditAsync(etudiant);
        }

        public async Task<Etudiant> ObtenirSelonIdAsync(int id)
        {
            return await _etudiantRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Etudiant>> ObtenirToutAsync()
        {
            return await _etudiantRepository.ListAsync();
        }

        public async Task SupprimerAsync(Etudiant etudiant)
        {
            await _etudiantRepository.DeleteAsync(etudiant);
        }
    }
}
