﻿using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Services;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : Controller
    {
        private ICita _svCita;

           

        public CitaController(ICita svCita)
        {
            _svCita = svCita;
        }

        //Get All
        [HttpGet]
        public IEnumerable<Cita> Get()
        {
            return _svCita.GetAll();
        }

        //GetById
        [HttpGet("{id}")]
        public Cita Get(int id)
        {
            return _svCita.GetById(id);
        }

        //Post
        [HttpPost]
        public void Post([FromBody] Cita cita)
        {
            _svCita.AddCita(cita);
        }

        //Put
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cita cita)
        {
            _svCita.Update(id, new Cita
            {
                FechaHora = cita.FechaHora,
                Lugar = cita.Lugar,
                Status = cita.Status
            });
        }

        //Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _svCita.Delete(id);
        }

    }
}