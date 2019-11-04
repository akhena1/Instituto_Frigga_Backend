using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domains;
using Backend.Interfaces;
using Microsoft.Data.SqlClient;

namespace Backend.Repositories
{
    public class FiltroRepository : IFiltro
    {
        
        SqlConnection con = new SqlConnection();
        public SqlConnection Conectar()
        {
            // Verifica se a conexão está fechada para conectar ao banco
            if(con.State == System.Data.ConnectionState.Closed){
                con.Open();
            }
            return con;
        }

        public void Conexao()
        {
            // Define os dados de conexão com meu servidor SQL
            con.ConnectionString = @"Server=N-1S-DEV-06\SQLEXPRESS; Database=InstitutoFrigga; User Id=sa; Password=132";        
        }

        public void Desconectar()
        {
            // Verifica se a conexão está aberta para fechar a conexão
            if(con.State == System.Data.ConnectionState.Open){
                con.Close();
            }
        }

        public List<Oferta> FiltrarOferta(int id)
        {
            try
            { 
                Conexao();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conectar();

            cmd.CommandText = "SELECT Oferta.*,Produto.Tipo,Categoria_produto.Tipo_produto FROM Oferta INNER JOIN Produto ON Produto.Produto_id = Oferta.Produto_id INNER JOIN Categoria_produto ON Categoria_produto.Categoria_produto_id = Produto.Categoria_produto_id WHERE Produto.Categoria_produto_id = @param1";
            cmd.Parameters.AddWithValue("@param1" , id);

            cmd.ExecuteNonQuery();

            SqlDataReader dados = cmd.ExecuteReader();

            List<Oferta> oferta = new List<Oferta>();

            while(dados.Read())
            {
                oferta.Add(
                    new Oferta()
                    {
                        OfertaId      = dados.GetInt32(0),
                        Preco         = dados.GetDouble(1),
                        Peso          = dados.GetDouble(2),
                        ImagemProduto = dados.GetString(3),
                        Quantidade    = dados.GetInt32(4),
                        UsuarioId     = dados.GetInt32(5),
                        ProdutoId     = dados.GetInt32(6)
                    }
                );
            } 
            Desconectar();

            return oferta;
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }

        public List<Receita> FiltrarReceita(int id)
        {
            try
            { 
                Conexao();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conectar();

            cmd.CommandText = "SELECT Receita.*,Categoria_receita.Tipo_receita, Usuario.Nome FROM Receita INNER JOIN Categoria_receita ON Categoria_receita.Categoria_receita_id = Receita.Categoria_receita_id INNER JOIN Usuario ON Usuario.Usuario_id = Receita.Usuario_id WHERE Receita.Categoria_receita_id = @param1";
            cmd.Parameters.AddWithValue("@param1" , id);

            cmd.ExecuteNonQuery();

            SqlDataReader dados = cmd.ExecuteReader();

            List<Receita> receita = new List<Receita>();

            while(dados.Read())
            {
                receita.Add(
                    new Receita()
                    {
                        ReceitaId          = dados.GetInt32(0),
                        Nome               = dados.GetString(1),
                        Ingredientes       = dados.GetString(2),
                        ModoDePreparo      = dados.GetString(3),
                        ImagemReceita      = dados.GetString(4),
                        CategoriaReceitaId = dados.GetInt32(5),
                        UsuarioId          = dados.GetInt32(6)
                    }
                );
            } 
            Desconectar();

            return receita;
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        
        public async Task<CategoriaProduto> BuscarPorId(int id)
        {
           using(InstitutoFriggaContext _context = new InstitutoFriggaContext())
            {
                return await _context.CategoriaProduto.FindAsync(id);
            } 
        }
    }
}