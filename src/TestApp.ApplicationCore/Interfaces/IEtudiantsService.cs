using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ApplicationCore.Entites;

namespace TestApp.ApplicationCore.Interfaces
{
    public interface IEtudiantsService
    {
        public Task<IEnumerable<Etudiant>> ObtenirToutAsync();

        public Task<Etudiant> ObtenirSelonIdAsync(int id);

        public Task AjouterAsync(Etudiant etudiant);

        public Task SupprimerAsync(Etudiant etudiant);

        public Task ModifierAsync(Etudiant etudiant);
    }
}
