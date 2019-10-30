using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ReceitaRepository : IReceita
    {
        public async Task<Receita> Alterar(Receita receita)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Entry(receita).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
            }
            return receita;        
        }

        public async Task<Receita> BuscarPorId(int id)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Receita.FindAsync(id);
            }
        }

        public async Task<Receita> Excluir(Receita receita)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Receita.Remove(receita);
                await _context.SaveChangesAsync();
                return receita;
            }
        }

        public async Task<List<Receita>> Listar()
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Receita.ToListAsync();
            }
        }

        public async Task<Receita> Salvar(Receita receita)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                await _context.AddAsync(receita);
                await _context.SaveChangesAsync();
                return receita;
            }
        }
    }
}