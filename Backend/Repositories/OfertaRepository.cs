using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class OfertaRepository : IOferta
    {
        public async Task<Oferta> Alterar(Oferta oferta)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Entry(oferta).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
            }
            return oferta;        
        }

        public async Task<Oferta> BuscarPorId(int id)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Oferta.FindAsync(id);
            }
        }

        public async Task<Oferta> Excluir(Oferta oferta)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Oferta.Remove(oferta);
                await _context.SaveChangesAsync();
                return oferta;
            }
        }

        public async Task<List<Oferta>> Listar()
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Oferta.ToListAsync();
            }
        }

        public async Task<Oferta> Salvar(Oferta oferta)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                await _context.AddAsync(oferta);
                await _context.SaveChangesAsync();
                return oferta;
            }
        }
    }
}