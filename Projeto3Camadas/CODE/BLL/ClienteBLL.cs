using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto3Camadas.CODE.DTO;
using Projeto3Camadas.CODE.DAL;

namespace Projeto3Camadas.CODE.BLL
{
    class ClienteBLL
    {
        AcessoBancoDados bd = new AcessoBancoDados();
        public Boolean inserir(ClienteDTO dto)
        {
            bd.Conectar();
            string comando = $"call inserir_cliente('{dto.Nome}','{dto.Email}'); ";
            return bd.ExecutarComandoSQL(comando);
        }
        public Boolean alterar(ClienteDTO dto)
        {
            bd.Conectar();
            string comando = $"call alterar_cliente('{dto.Id}','{dto.Nome}','{dto.Email}'); ";
            return bd.ExecutarComandoSQL(comando);
        }
        public DataTable selecionar()
        {
            DataTable dataTable = new DataTable();
            bd = new AcessoBancoDados();
            bd.Conectar();
            string comando = $"call selecionar_todos_cliente();";
            dataTable = bd.RetDataTable(comando);
            return dataTable;
        }

        public DataTable pesquisa_cliente(String pesquisa)
        {
            DataTable dataTable = new DataTable();
            bd = new AcessoBancoDados();
            bd.Conectar();
            string comando = $"call pesquisa_cliente('{pesquisa}');";
            dataTable = bd.RetDataTable(comando);
            return dataTable;
        }

        public void excluir(ClienteDTO dto)
        {
            bd.Conectar();
            string comando = $"call excluir_cliente('{dto.Id}');";
            bd.ExecutarComandoSQL(comando);

        }
        
    }
}
