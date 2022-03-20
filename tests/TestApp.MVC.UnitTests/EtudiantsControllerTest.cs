using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ApplicationCore.Entites;
using TestApp.ApplicationCore.Interfaces;
using TestApp.MVC.Controllers;
using Xunit;


namespace TestApp.MVC.UnitTests
{
    public class EtudiantsControllerTest
    {
        [Fact]
        public async Task Details_IdInexistant_Retourne_NotFound()
        {

            //Etant donné
            //Initialiser une nouvelle instance de Mock en spécifiant l'interface du service à substituer
            var mockEtudiantsService = new Mock<IEtudiantsService>();
            //Définir le résultat qui sere retourné lorque la fonction sera appelée
            mockEtudiantsService.Setup(e => e.ObtenirSelonIdAsync(It.IsAny<int>())).Returns(() => Task.FromResult<Etudiant>(null));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);

            //Quand
            var actionResult = await etudiantsController.Details(2);

            //Alors
            actionResult.Should().BeOfType(typeof(NotFoundResult));
            //Assert.True(false);
        }


        [Fact]
        public async Task Details_IdExistant_Retourne_ViewResult()
        {
            //Etant donné
            //Initialiser un Etudiant
            var fixture = new Fixture();
            var etudiant = fixture.Create<Etudiant>();

            //Initialiser une nouvelle instance de Mock en spécifiant l'interface du service à substituer
            var mockEtudiantsService = new Mock<IEtudiantsService>();
            //Définir le résultat qui sere retourné lorque la fonction sera appelée
            mockEtudiantsService.Setup(e => e.ObtenirSelonIdAsync(It.IsAny<int>())).Returns(() => Task.FromResult(etudiant));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);

            //Quand
            var viewResult = await etudiantsController.Details(1) as ViewResult;

            //Alors
            viewResult.Should().NotBeNull();
            var etudiantResult = viewResult.Model as Etudiant;
            etudiantResult.Should().Be(etudiant);
        }


        [Fact]
        public async Task Create_EtudiantInvalide_Retourne_ViewResult()
        {
            //Etant donné
            var fixture = new Fixture();
            var etudiant = fixture.Create<Etudiant>();

          
            var mockEtudiantsService = new Mock<IEtudiantsService>();
            mockEtudiantsService.Setup(e => e.AjouterAsync(It.IsAny<Etudiant>()));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);
            etudiantsController.ModelState.AddModelError("Nom","Le champ est obligatoire");

            //Quand
            var viewResult = await etudiantsController.Create(etudiant) as ViewResult;

            //Alors
            viewResult.Should().NotBeNull();
            mockEtudiantsService.Verify(e => e.AjouterAsync(It.IsAny<Etudiant>()), Times.Never);
            var etudiantResult = viewResult.Model as Etudiant;
            etudiantResult.Should().Be(etudiant);
        }



        [Fact]
        public async Task Create_EtudiantValide_Retourne_RedirectToAction()
        {
            //Etant donné
            var fixture = new Fixture();
            var etudiant = fixture.Create<Etudiant>();

            var mockEtudiantsService = new Mock<IEtudiantsService>();
            mockEtudiantsService.Setup(e => e.AjouterAsync(It.IsAny<Etudiant>()));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);
            
            //Quand
            var redirectToActionResult = await etudiantsController.Create(etudiant) as RedirectToActionResult;

            //Alors
            redirectToActionResult.Should().NotBeNull();
            redirectToActionResult.ActionName.Should().Be("Index");
            mockEtudiantsService.Verify(e => e.AjouterAsync(It.IsAny<Etudiant>()));
        }


        [Fact]
        public async Task Index_NbelementsNulls_Retourne_ViewResult()
        {
            //Etant donné
            //Initialiser un Etudiant
            var fixture = new Fixture
            {
                RepeatCount = 15
            };

            var etudiants = fixture.CreateMany<Etudiant>();

            //Initialiser une nouvelle instance de Mock en spécifiant l'interface du service à substituer
            var mockEtudiantsService = new Mock<IEtudiantsService>();
            //Définir le résultat qui sere retourné lorque la fonction sera appelée
            mockEtudiantsService.Setup(e => e.ObtenirToutAsync()).Returns(() => Task.FromResult(etudiants));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);

            //Quand
            var viewResult = await etudiantsController.Index(null) as ViewResult;

            //Alors
            viewResult.Should().NotBeNull();
            var etudiantsResult = viewResult.Model as IEnumerable<Etudiant>;
            etudiantsResult.Should().BeEquivalentTo(etudiants);
        }

        [Fact]
        public async Task Index_NbelementsNonNull_Retourne_ViewResult()
        {
            //Etant donné
            //Initialiser un Etudiant
            var fixture = new Fixture
            {
                RepeatCount = 15
            };

            var etudiants = fixture.CreateMany<Etudiant>();

            //Initialiser une nouvelle instance de Mock en spécifiant l'interface du service à substituer
            var mockEtudiantsService = new Mock<IEtudiantsService>();
            //Définir le résultat qui sere retourné lorque la fonction sera appelée
            mockEtudiantsService.Setup(e => e.ObtenirToutAsync()).Returns(() => Task.FromResult(etudiants));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);

            //Quand
            var viewResult = await etudiantsController.Index(10) as ViewResult;

            //Alors
            viewResult.Should().NotBeNull();
            var etudiantsResult = viewResult.Model as IEnumerable<Etudiant>;
            etudiantsResult.ToList().Count.Should().Be(10);
            etudiantsResult.Should().BeEquivalentTo(etudiants.Take(10));
        }



        [Fact]
        public async Task Edit_HttpGet_IdInexistant_Retourne_NotFound()
        {

            //Etant donné
            //Initialiser une nouvelle instance de Mock en spécifiant l'interface du service à substituer
            var mockEtudiantsService = new Mock<IEtudiantsService>();
            //Définir le résultat qui sere retourné lorque la fonction sera appelée
            mockEtudiantsService.Setup(e => e.ObtenirSelonIdAsync(It.IsAny<int>())).Returns(() => Task.FromResult<Etudiant>(null));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);

            //Quand
            var actionResult = await etudiantsController.Edit(2);

            //Alors
            actionResult.Should().BeOfType(typeof(NotFoundResult));

        }


        [Fact]
        public async Task Edit_HttpGet_IdExistant_Retourne_ViewResult()
        {
            //Etant donné
            //Initialiser un Etudiant
            var fixture = new Fixture();
            var etudiant = fixture.Create<Etudiant>();

            //Initialiser une nouvelle instance de Mock en spécifiant l'interface du service à substituer
            var mockEtudiantsService = new Mock<IEtudiantsService>();
            //Définir le résultat qui sere retourné lorque la fonction sera appelée
            mockEtudiantsService.Setup(e => e.ObtenirSelonIdAsync(It.IsAny<int>())).Returns(() => Task.FromResult(etudiant));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);

            //Quand
            var viewResult = await etudiantsController.Edit(1) as ViewResult;

            //Alors
            viewResult.Should().NotBeNull();
            var etudiantResult = viewResult.Model as Etudiant;
            etudiantResult.Should().Be(etudiant);
        }


        [Fact]
        public async Task Edit_HttpPost_EtudiantInvalide_Retourne_ViewResult()
        {
            //Etant donné
            var fixture = new Fixture();
            var etudiant = fixture.Create<Etudiant>();


            var mockEtudiantsService = new Mock<IEtudiantsService>();
            mockEtudiantsService.Setup(e => e.ModifierAsync(It.IsAny<Etudiant>()));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);
            etudiantsController.ModelState.AddModelError("Nom", "Le champ est obligatoire");

            //Quand
            var viewResult = await etudiantsController.Edit(1, etudiant) as ViewResult;

            //Alors
            viewResult.Should().NotBeNull();
            mockEtudiantsService.Verify(e => e.ModifierAsync(It.IsAny<Etudiant>()), Times.Never);
            var etudiantResult = viewResult.Model as Etudiant;
            etudiantResult.Should().Be(etudiant);
        }



        [Fact]
        public async Task Edit_HttpPost_EtudiantValide_Retourne_RedirectToActionIndex()
        {
            //Etant donné
            var fixture = new Fixture();
            var etudiant = fixture.Create<Etudiant>();

            var mockEtudiantsService = new Mock<IEtudiantsService>();
            mockEtudiantsService.Setup(e => e.ModifierAsync(It.IsAny<Etudiant>()));

            var etudiantsController = new EtudiantsController(mockEtudiantsService.Object);

            //Quand
            var redirectToActionResult = await etudiantsController.Edit(1, etudiant) as RedirectToActionResult;

            //Alors
            redirectToActionResult.Should().NotBeNull();
            redirectToActionResult.ActionName.Should().Be("Index");
            mockEtudiantsService.Verify(e => e.ModifierAsync(It.IsAny<Etudiant>()));
        }
    }
}
