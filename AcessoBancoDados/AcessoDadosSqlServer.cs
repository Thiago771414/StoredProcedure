using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

//namespace que contém classes que manipulam dados
using System.Data;
//para trabalhar com sqlserver
using System.Data.SqlClient;
//adiciona a configuração do settings
using AcessoBancoDados.Properties;

namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //criar a conexão
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //parâmetros que vão para o banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void Limparparametros()
        {
            sqlParameterCollection.Clear();
        }

        public void AdicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persistência - Inserir, Alterar, Excluir
        public object ExecutarManipulacao(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //criar conexao
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco.
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexao)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; // em segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }
                
                //Executar o comando, ou seja, mandar o comando ir até o banco de dados.
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //consultar registros do banco de dados
        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoredProcedureOuTextoSql)
        {
            try
            {
                //criar conexao
                SqlConnection sqlConnection = CriarConexao();
                //Abrir conexão
                sqlConnection.Open();
                //Criar o comando que vai levar a informação para o banco.
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexao)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; // em segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Criar um adaptador.
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                //DataTable = Tabela de dados vazia onde vou colocar os dados que vem do banco
                DataTable dataTable = new DataTable();
                //Mandar o comando ir até o banco buscar os dados e o adaptador preencher o datatble
                sqlDataAdapter.Fill(dataTable);
                
                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
