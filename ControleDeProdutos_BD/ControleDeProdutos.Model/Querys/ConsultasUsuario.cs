using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Data;

public class ConsultasUsuario
{
    public static Usuario ObterUsuarioPeloLoginSenha(string login, string senha)
    {
        var conexao = new MySqlConnection(ConnectionBD.Connection.ConnectionString);
        Usuario usuario = null;
        string senhaCriptografada = CriptografiaMD5.CriptografarSenha(senha);
        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"SELECT * FROM Usuario WHERE login = @login AND senha = @senha;";
            comando.Parameters.AddWithValue("@login", login);
            comando.Parameters.AddWithValue("@senha", senhaCriptografada);
            var leitura = comando.ExecuteReader();
            while (leitura.Read())
            {
                usuario = new Usuario();
                usuario.Id = leitura.GetInt32("id");
                usuario.Login = leitura.GetString("login");
                usuario.Senha = leitura.GetString("senha");
                usuario.Perfil = leitura.GetString("perfil");
                break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            usuario = null;
        }
        finally
        {
            if(conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return usuario;
    }

    public static bool NovoUsuario(string login, string senha)
    {
        var conexao = new MySqlConnection(ConnectionBD.Connection.ConnectionString);
        bool foiInserido = false;
        string senhaCriptografada = CriptografiaMD5.CriptografarSenha(senha);
        try
        {
            conexao.Open();
            var comando = conexao.CreateCommand();
            comando.CommandText = @"Insert Into Usuario (login, senha, perfil) values (@login, @senha, @perfil)";
            comando.Parameters.AddWithValue("@login", login);
            comando.Parameters.AddWithValue("@senha", senhaCriptografada);
            comando.Parameters.AddWithValue("@perfil", "Comum");
            var leitura = comando.ExecuteReader();
            foiInserido = true;
        }
        catch(Exception ex)
        {
            Console.WriteLine (ex.Message);
        }
        finally
        {
            if(conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        return foiInserido;
    }
}
