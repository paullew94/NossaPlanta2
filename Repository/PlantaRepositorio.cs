﻿using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository
{
    public class PlantaRepositorio
    {
        public string CadeiaConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=T:\Documentos\nossaplanta.mdf;Integrated Security=True;Connect Timeout=30";
        public void Inserir(Planta planta)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiaConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = @"INSERT INTO plantas(nome,
peso,altura,carnivora) VALUES (@NOME,@PESO,@ALTURA,@CARNIVORA)";
            comando.Parameters.AddWithValue("@NOME", planta.Nome);
            comando.Parameters.AddWithValue("@PESO", planta.Peso);
            comando.Parameters.AddWithValue("@ALTURA", planta.Altura);
            comando.Parameters.AddWithValue("@CARNIVORA", planta.Carnivora);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public List<Planta> ObterTodos(string busca)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiaConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand(); 
            comando.Connection = conexao;
            comando.CommandText = @"SELECT * FROM plantas WHERE nome LIKE @NOME";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@NOME", busca);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            conexao.Close();

            List<Planta> plantas = new List<Planta>();
            for (int i = 0; i <tabela.Rows.Count; i++)

            {
                DataRow linha = tabela.Rows[i];
                Planta planta = new Planta();
                planta.Id = Convert.ToInt32(linha["id"]);
                planta.Carnivora = Convert.ToBoolean(linha["carnivora"]);
                planta.Nome = linha["nome"].ToString();
                planta.Altura = Convert.ToDecimal(linha["altura"]);
                planta.Peso = Convert.ToDecimal(linha["peso"]);
                plantas.Add(planta);
            }
            return plantas;
        }
        public void Apagar(int id )
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiaConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "DELETE FROM plantas WHERE ID =@ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public Planta ObterPeloId(int id)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiaConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT *FROM plantas WHERE id=@ID";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count==0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Planta planta = new Planta();
            planta.Id = id;
            planta.Nome = linha["nome"].ToString();
            planta.Peso = Convert.ToDecimal(linha["peso"]);
            planta.Altura = Convert.ToDecimal(linha["altura"]);
            planta.Carnivora = Convert.ToBoolean(linha["carnivora"]);
            return planta;


        }


        public void Alterar(Planta planta)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiaConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = @"UPDATE plantas SET nome=@NOME, altura=@ALTURA, peso=@PESO, carnivora=@CARNIVORA WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", planta.Nome);
            comando.Parameters.AddWithValue("@ALTURA", planta.Altura);
            comando.Parameters.AddWithValue("@PESO", planta.Peso);
            comando.Parameters.AddWithValue("@CARNIVORA", planta.Carnivora);
            comando.Parameters.AddWithValue("@ID", planta.Id);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

    }

}
