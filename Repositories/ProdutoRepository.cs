using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Instituto_Frigga_Backend.Repositories
{
    public class ProdutoRepository : IProduto
    {
        public async Task<Produto> Alterar(Produto produto)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Entry(produto).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
            }
            return produto;        
        }

        public async Task<Produto> BuscarPorId(int id)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Produto.FindAsync(id);
            }
        }

        public async Task<Produto> Excluir(Produto produto)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
        }

        public async Task<List<Produto>> Listar()
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Produto.Include("CategoriaProduto").ToListAsync();
            }
        }

        public async Task<Produto> Salvar(Produto produto)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                await _context.AddAsync(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
        }
    }
}