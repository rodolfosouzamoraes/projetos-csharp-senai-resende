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
    /// Lógica interna para CadastrarUsuario.xaml
    /// </summary>
    public partial class CadastrarUsuario : Window
    {
        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        private void CadastrarNovoUsuario(object sender, RoutedEventArgs e)
        {
            if(txtLogin.Text == "" || txtSenha.Password == "" || txtConfirmaSenha.Password == "")
            {
                MessageBoxResult result = MessageBox.Show(
                    "Preencha todos os campos!", 
                    "Atenção", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Exclamation
                );
            }
            else
            {
                if(txtSenha.Password != txtConfirmaSenha.Password)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "As senhas não batem!",
                        "Atenção",
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation
                    );
                }
                else
                {
                    // Cadastra o Usuario
                    string login = txtLogin.Text;
                    string senha = txtSenha.Password;
                    bool foiInserido = cUsuario.NovoUsuario(login, senha);
                    if(foiInserido == true)
                    {
                        MessageBoxResult result = MessageBox.Show(
                            "Usuario cadastrado!",
                            "Atenção",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information
                        );
                        TelaLogin janelaLogin = new TelaLogin();
                        janelaLogin.Show();
                        Close();
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show(
                               "Não foi possível cadastrar o usuário!",
                               "Atenção",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error
                           );
                    }
                }
            }
        }
    }
}
