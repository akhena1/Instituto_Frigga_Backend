using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Instituto_Frigga_Backend.Repositories
{
    public class CategoriaProdutoRepository : ICategoriaProduto
    {
        public async Task<CategoriaProduto> Alterar(CategoriaProduto categoriaProduto)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Entry(categoriaProduto).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
            }
            return categoriaProduto;        
        }

        public async Task<CategoriaProduto> BuscarPorId(int id)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.CategoriaProduto.FindAsync(id);
            }
        }

        public async Task<CategoriaProduto> Excluir(CategoriaProduto categoriaProduto)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.CategoriaProduto.Remove(categoriaProduto);
                await _context.SaveChangesAsync();
                return categoriaProduto;
            }
        }

        public async Task<List<CategoriaProduto>> Listar()
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.CategoriaProduto.ToListAsync();
            }
        }

        public async Task<CategoriaProduto> Salvar(CategoriaProduto categoriaProduto)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                await _context.AddAsync(categoriaProduto);
                await _context.SaveChangesAsync();
                return categoriaProduto;
            }
        }
    }
}