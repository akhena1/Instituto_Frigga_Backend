using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Instituto_Frigga_Backend.Repositories
{
    public class CategoriaReceitaRepository : ICategoriaReceita
    {
        public async Task<CategoriaReceita> Alterar(CategoriaReceita categoriaReceita)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Entry(categoriaReceita).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
            }
            return categoriaReceita;        
        }

        public async Task<CategoriaReceita> BuscarPorId(int id)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.CategoriaReceita.FindAsync(id);
            }
        }

        public async Task<CategoriaReceita> Excluir(CategoriaReceita categoriaReceita)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.CategoriaReceita.Remove(categoriaReceita);
                await _context.SaveChangesAsync();
                return categoriaReceita;
            }
        }

        public async Task<List<CategoriaReceita>> Listar()
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.CategoriaReceita.ToListAsync();
            }
        }

        public async Task<CategoriaReceita> Salvar(CategoriaReceita categoriaReceita)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                await _context.AddAsync(categoriaReceita);
                await _context.SaveChangesAsync();
                return categoriaReceita;
            }
        }
    }
}