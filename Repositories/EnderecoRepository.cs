using System.Collections.Generic;
using System.Threading.Tasks;
using Instituto_Frigga_Backend.Domains;
using Instituto_Frigga_Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Instituto_Frigga_Backend.Repositories
{
    public class EnderecoRepository : IEndereco
    {
        public async Task<Endereco> Alterar(Endereco endereco)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Entry(endereco).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
            }
            return endereco;        
        }

        public async Task<Endereco> BuscarPorId(int id)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Endereco.FindAsync(id);
            }
        }

        public async Task<Endereco> Excluir(Endereco endereco)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Endereco.Remove(endereco);
                await _context.SaveChangesAsync();
                return endereco;
            }
        }

        public async Task<List<Endereco>> Listar()
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.Endereco.ToListAsync();
            }
        }

        public async Task<Endereco> Salvar(Endereco endereco)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                await _context.AddAsync(endereco);
                await _context.SaveChangesAsync();
                return endereco;
            }
        }
    }
}