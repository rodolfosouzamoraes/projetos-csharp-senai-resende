﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

public class ConsultasProduto
{
    public static List<Produto> ObterTodosProdutos()
    {
        var conexao = new MySqlConnection(ConnectionBD.Connection.ConnectionString);
        List<Produto> listaProduto = new List<Produto>();

        try
        {
            // Vou tentar realizar tudo que tiver no meu bloco
            
            //Primeira coisa a se fazer, abrir a conexão
            conexao.Open();

            //Criar uma variavel que permita criar comandos sql
            var comando = conexao.CreateCommand();

            //Adicionar o comando sql no atributo de 'comando'
            comando.CommandText = @"SELECT * FROM Produto";

            //Executar o comando SQL e trazer o valor lido por esse comando.
            var leitura = comando.ExecuteReader();

            //Percorrer a 'leitura' para poder armazenar na minha lista de produtos
            while (leitura.Read())
            {
                //Criar um objeto do tipo Produto
                Produto produto = new Produto();
                produto.Id = leitura.GetInt32("Id");
                produto.Nome = leitura.GetString("Nome");
                produto.Descricao = leitura.GetString("Descricao");
                produto.Fabricante = leitura.GetString("Fabricante");
                produto.Qtd = leitura.GetInt32("Quantidade");

                //Adicionar na lista o produto
                listaProduto.Add(produto);
            }
        }
        catch (Exception ex)
        {
            // não conseguiu finalizar o try, gerou um erro onde eu posso tratar esse erro.
            Console.WriteLine(ex.Message);
        }
        finally
        {
            // Finzaliza após o try e catch

            //Verifica se a conexão está aberta com o banco de dados
            if(conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return listaProduto;
    }

    public static bool InserirProduto(string nome, string descricao, string fabricante, int quantidade)
    {
        var conexao = new MySqlConnection(ConnectionBD.Connection.ConnectionString);
        bool foiInserido = false;

        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"INSERT INTO Produto (Nome, Descricao, Fabricante, Quantidade) values (@nome, @descricao, @fabricante, @qtd)";
            comando.Parameters.AddWithValue("@nome", nome);
            comando.Parameters.AddWithValue("@descricao", descricao);
            comando.Parameters.AddWithValue("@fabricante", fabricante);
            comando.Parameters.AddWithValue("@qtd", quantidade);
            var leitura = comando.ExecuteReader();
            foiInserido = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiInserido;

    }
}