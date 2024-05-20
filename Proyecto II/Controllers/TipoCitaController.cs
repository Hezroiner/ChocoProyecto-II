using Microsoft.AspNetCore.Mvc;
using Proyecto_II.Entities;
using Proyecto_II.Services;
using Services;

namespace Proyecto_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoCitaController : Controller
    {
            private ITipoCita _svTpoCita;

            public TipoCitaController(ITipoCita svTipoCita)
            {
                _svTpoCita = svTipoCita;
            }

            //Get All
            [HttpGet]
            public IEnumerable<TipoCita> Get()
            {
                return _svTpoCita.GetAll();
            }

            //GetById
            [HttpGet("{id}")]
            public TipoCita Get(int id)
            {
                return _svTpoCita.GetById(id);
            }
    }
}

