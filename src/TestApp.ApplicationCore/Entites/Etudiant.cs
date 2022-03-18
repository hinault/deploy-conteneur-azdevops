using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.ApplicationCore.Entites
{
    public class Etudiant : BaseEntity
    {
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [MaxLength(25, ErrorMessage = "La taille maximale du champ est de 20")]
        public string Nom { get; set; }


        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [EmailAddress(ErrorMessage = "Le champ ne correspond pas à un email valide")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [Display(Name = "Adresse de courriel")]
        public string Email { get; set; }

        [Display(Name = "Date inscription")]
        public DateTime DateInscription { get; set; }
    }
}
