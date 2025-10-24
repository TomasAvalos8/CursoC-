﻿using dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using negocio;

namespace pokedex_mvc.Controllers
{
    public class PokemonController : Controller
    {
        // GET: PokemonController
        public ActionResult Index(string filtro)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            var pokemons = negocio.listar();

            if (!string.IsNullOrEmpty(filtro))
            {
                pokemons = pokemons.FindAll(p=> p.Nombre.Contains(filtro));
            }
            ViewBag.filtro = filtro; 
            return View(pokemons);
        }

        // GET: PokemonController/Details/5
        public ActionResult Details(int id)
        {
            PokemonNegocio negocio = new PokemonNegocio();

            var pokemon = negocio.listar().Find(p => p.Id == id);
            return View(pokemon);
        }

        // GET: PokemonController/Create
        public ActionResult Create()
        {
            ElementoNegocio negocioElemento = new ElementoNegocio();
            ViewBag.Elementos = new SelectList(negocioElemento.listar(), "Id", "Descripcion");
            return View();
        }

        // POST: PokemonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pokemon pokemon)
        {
            try
            {
                if (pokemon.Nombre == "maxi")
                {
                    ModelState.AddModelError("", "No puede llamarse Maxi");
                }

                if (!ModelState.IsValid)
                {
                    return View(pokemon);
                }

                PokemonNegocio negocio = new PokemonNegocio();
                //pokemon.Tipo = new Elemento { Id = 1 };
                //pokemon.Debilidad = new Elemento { Id = 2 };
                negocio.agregar(pokemon);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PokemonController/Edit/5
        public ActionResult Edit(int id)
        {
            ElementoNegocio negocioElemento = new ElementoNegocio();
            PokemonNegocio negocio = new PokemonNegocio();

            var pokemon = negocio.listar().Find(p => p.Id == id);

            var lista = negocioElemento.listar();
            ViewBag.Tipos = new SelectList(lista, "Id", "Descripcion", pokemon.Tipo.Id);
            ViewBag.Debilidades = new SelectList(lista, "Id", "Descripcion", pokemon.Debilidad.Id);

            return View(pokemon);
        }

        // POST: PokemonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pokemon pokemon)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                negocio.modificar(pokemon);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PokemonController/Delete/5
        public ActionResult Delete(int id)
        {

            PokemonNegocio negocio = new PokemonNegocio();

            var pokemon = negocio.listar().Find(p => p.Id == id);

            return View(pokemon);
        }

        // POST: PokemonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                negocio.eliminar(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
