using ControleDeProdutos_View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControleDeProdutos.View
{
    /// <summary>
    /// Lógica interna para TelaLogin.xaml
    /// </summary>
    public partial class TelaLogin : Window
    {
        public TelaLogin()
        {
            InitializeComponent();
        }

        private void Entrar(object sender, RoutedEventArgs e)
        {
            if(txtLogin.Text == "" || txtSenha.Password == "")
            {
                MessageBoxResult mensagem = MessageBox.Show("Preencha todos os campos!","Atenção!",MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                string login = txtLogin.Text;
                string senha = txtSenha.Password;
                Usuario usuario = cUsuario.ObterUsuarioPeloLoginSenha(login, senha);
                if(usuario != null)
                {
                    LimpaCampos();
                    MainWindow janelaSistema = new MainWindow(usuario,this);
                    janelaSistema.Show();
                    Hide();
                }
                else
                {
                    MessageBoxResult mensagem = MessageBox.Show("Usuário ou senha inválidos!","Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LimpaCampos()
        {
            txtLogin.Text = "";
            txtSenha.Password = "";
        }
    }
}
