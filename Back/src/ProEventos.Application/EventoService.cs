using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {

        private readonly IGeralPersist _geralPersist;
        private readonly IEventosPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventosPersist eventoPersist)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                if (model != null)
                {
                    _geralPersist.Add<Evento>(model);
                    if (await _geralPersist.SaveChangesAsync())
                    {
                        return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Exception: {ex.Message}");
            }
        }

        public async Task<Evento> UpdateEvento(int EventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(EventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;

                _geralPersist.Update(model);

                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                }

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Exception: {ex.Message}");
            }
        }
        public async Task<bool> DeleteEvento(int EventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(EventoId, false);
                if (evento == null) throw new Exception("Evento para delete não encontrado!"); //return false;

                _geralPersist.Delete(evento);

                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Exception: {ex.Message}");
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Exception: {ex.Message}");
            }

        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Exception: {ex.Message}");
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(EventoId, includePalestrantes);
                if (evento == null) return null;

                return evento;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Exception: {ex.Message}");
            }
        }

    }
}