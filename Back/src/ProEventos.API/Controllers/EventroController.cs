using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        /*
                private readonly ILogger<EventoController> _logger;

                public EventoController(ILogger<EventoController> logger)
                {
                    _logger = logger;
                }
        */
        public IEnumerable<Evento> _eventos = new Evento[]
        {
                new Evento()
                {
                    EventoId = 1,
                    Tema = "Angular e dotNetCore",
                    Local = "Localteesste",
                    QtdPessoas = 5
                },
                new Evento()
                {
                    EventoId = 2,
                    Tema = "abbbbbbc",
                    Local = "ccccccdee",
                    QtdPessoas = 10
                },
        };

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _eventos;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
            return _eventos.Where(x => x.EventoId == id);
        }

    }
}
