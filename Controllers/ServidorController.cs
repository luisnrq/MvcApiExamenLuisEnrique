using Microsoft.AspNetCore.Mvc;
using MvcApiExamenLuisEnrique.Models;
using MvcApiExamenLuisEnrique.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiExamenLuisEnrique.Controllers
{
    public class ServidorController : Controller
    {
        private ServiceApiSeries service;

        public ServidorController(ServiceApiSeries service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        public async Task<IActionResult> Details(int id)
        {
            Serie serie = await this.service.FindSerieAsync(id);
            return View(serie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Serie serie)
        {
            await this.service.InsertSerieAsync(0,serie.NombreSerie,serie.Imagen,serie.Puntuacion,serie.Anio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Serie serie = await this.service.FindSerieAsync(id);
            return View(serie);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Serie serie)
        {
            await this.service.UpdateSerieAsync(serie.IdSerie, serie.NombreSerie, serie.Imagen, serie.Puntuacion, serie.Anio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteSerieAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> PersonajesSerie(int id)
        {
            List<Personaje> personajes = await this.service.GetPersonajesSeriesAsync(id);
            return View(personajes);
        }

        public async Task<IActionResult> DetailsPersonaje(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);
            return View(personaje);
        }

        public IActionResult CreatePersonajeSerie()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonajeSerie(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje.NombrePersonaje,personaje.Imagen,personaje.IdSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePersonaje(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CambiarSerieAsync()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            ViewData["SERIES"] = series;

            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            ViewData["PERSONAJES"] = personajes;

            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> CambiarSerie(int serie, int personaje)
        {
            await this.service.UpdatePersonajeSerieAsync(serie, personaje);
            return RedirectToAction("Index");
        }
        
    }
}
