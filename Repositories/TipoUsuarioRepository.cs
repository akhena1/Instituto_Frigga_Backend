using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        public async Task<TipoUsuario> Alterar(TipoUsuario tipoUsuario)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.Entry(tipoUsuario).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
            }
            return tipoUsuario;        
        }

        public async Task<TipoUsuario> BuscarPorId(int id)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.TipoUsuario.FindAsync(id);
            }
        }

        public async Task<TipoUsuario> Excluir(TipoUsuario tipoUsuario)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                _context.TipoUsuario.Remove(tipoUsuario);
                await _context.SaveChangesAsync();
                return tipoUsuario;
            }
        }

        public async Task<List<TipoUsuario>> Listar()
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.TipoUsuario.ToListAsync();
            }
        }

        public async Task<TipoUsuario> Salvar(TipoUsuario tipoUsuario)
        {
            using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                await _context.AddAsync(tipoUsuario);
                await _context.SaveChangesAsync();
                return tipoUsuario;
            }
        }
    }
}