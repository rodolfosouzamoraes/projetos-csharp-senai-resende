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
using System.Text.RegularExpressions;
using ControleDeProdutos.View;

namespace ControleDeProdutos_View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TelaLogin telaLogin;
        Usuario usuario;
        public MainWindow(Usuario _usuario, TelaLogin _telaLogin)
        {
            InitializeComponent();
            AtualizaDataGrid();
            telaLogin = _telaLogin;
            usuario = _usuario;
            if(usuario.Perfil == "COM")
            {
                btnAtualizar.Visibility = Visibility.Hidden;
                btnExcluir.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Método acionado pelo botão Limpar
        /// </summary>
        private void LimparCampos(object sender, RoutedEventArgs e)
        {
            LimpaTodosCampos();
        }

        /// <summary>
        /// Limpa todos os campos do formulário
        /// </summary>
        private void LimpaTodosCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtFabricante.Text = "";
            txtQtd.Text = "";
        }

        /// <summary>
        /// Método acionado pelo botão Novo
        /// </summary>
        private void NovoProduto(object sender, RoutedEventArgs e)
        {
            if(txtNome.Text == "" || txtDescricao.Text == "" || txtFabricante.Text == "" || txtQtd.Text == "")
            {
                MessageBoxResult messageBox = MessageBox.Show("Preencha todos os campos obrigatórios!","Atenção",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
            else
            {
                if(txtId.Text == "")
                {
                    bool foiInserido = cProduto.NovoProduto(txtNome.Text, txtDescricao.Text, txtFabricante.Text, int.Parse(txtQtd.Text));
                    InformaUsuario(foiInserido, "Produto inserido com sucesso!", "Erro ao inserir produto");
                }
                else
                {
                    MessageBoxResult message = MessageBox.Show("Há um produto selecionado, por favor, limpe todos os campos antes de continuar!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }                
            }
        }

        /// <summary>
        /// Atualiza os dados dentro do datagrid
        /// </summary>
        private void AtualizaDataGrid()
        {
            List<Produto> listaProdutos = cProduto.SelecionaTodosProdutos();
            dgvProdutos.ItemsSource = listaProdutos;
            dgvProdutos.Items.Refresh();
        }

        /// <summary>
        /// Método acionado ao realizar o double click na linha do datagrid
        /// </summary>
        private void PegarItemNoGrid(object sender, MouseButtonEventArgs e)
        {
            if(usuario.Perfil == "ADM")
            {
                Produto produto = (Produto)dgvProdutos.SelectedItem;
                txtId.Text = "" + produto.Id;
                txtNome.Text = produto.Nome;
                txtQtd.Text = "" + produto.Qtd;
                txtDescricao.Text = produto.Descricao;
                txtFabricante.Text = produto.Fabricante;
            }            
        }

        /// <summary>
        /// Método acionado pelo botão excluir
        /// </summary>
        private void ExcluirProduto(object sender, RoutedEventArgs e)
        {
            if(txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                MessageBoxResult message = MessageBox.Show($"Deseja excluir o produto?: {txtId.Text}", "Exlcuir Produto",MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(message == MessageBoxResult.Yes)
                {
                    bool foiExcluido = cProduto.ExcluirProduto(id);
                    InformaUsuario(foiExcluido, "O produto foi excluído com sucesso!", "Excluir Produto");
                }
            }
        }

        /// <summary>
        /// Método acionado pelo botão Atualizar
        /// </summary>
        private void AtualizarProduto(object sender, RoutedEventArgs e)
        {
            if(txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                MessageBoxResult message = MessageBox.Show($"Deseja atualizar o produto?: {txtId.Text}", "Atualizar Produto", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (message == MessageBoxResult.Yes)
                {
                    int quantidade = int.Parse(txtQtd.Text);
                    bool foiAlterado = cProduto.AtualizaProduto(id, txtNome.Text, txtDescricao.Text, txtFabricante.Text, quantidade);
                    InformaUsuario(foiAlterado, "Produto atualizado com sucesso!", "Atualizar Produto");
                }
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Selecione um produto para atualizar!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Método acionado ao tentar escrever um valor no textbox Quantidade, permitindo apenas o uso de números.
        /// </summary>
        private void txtQtd_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        /// <summary>
        /// Informa ao usuário uma mensagem e atualiza os campos.
        /// </summary>
        /// <param name="foiVerdadeiro">Verifição positiva ou negativa</param>
        /// <param name="mensagemInformativa">Mensagem informada pelo MessageBox</param>
        /// <param name="tituloDaBox">Titulo da MessageBox</param>
        private void InformaUsuario(bool foiVerdadeiro, string mensagemInformativa, string tituloDaBox)
        {
            if (foiVerdadeiro)
            {
                AtualizaDataGrid();
                LimpaTodosCampos();
                MessageBoxResult messageInformacao = MessageBox.Show(mensagemInformativa, tituloDaBox, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult messageErro = MessageBox.Show("Ocorreu um erro, por favor, tente novamente mais tarde!", "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Sair(object sender, RoutedEventArgs e)
        {
            telaLogin.Show();
            Close();
        }
    }
}
