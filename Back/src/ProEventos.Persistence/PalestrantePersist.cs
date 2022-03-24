using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {

        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext proEventosContext)
        {
            _context = proEventosContext;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(e => e.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(e => e.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(e => e.RedesSociais)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            if (includeEventos)
            {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(e => e.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(e => e.RedesSociais)
                .Where(p => p.Id == PalestranteId);

            if (includeEventos)
            {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(e => e.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.FirstOrDefaultAsync();
        }

    }
}