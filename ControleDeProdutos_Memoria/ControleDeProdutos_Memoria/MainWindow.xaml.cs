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

namespace ControleDeProdutos_Memoria
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Produto> listaProdutos = new List<Produto>(); // Nosso "Banco de dados" na memória
        int proximoId = 0;
        public MainWindow()
        {
            InitializeComponent();
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
                    proximoId++;
                    Produto produto = new Produto();
                    produto.Nome = txtNome.Text;
                    produto.Descricao = txtDescricao.Text;
                    produto.Fabricante = txtFabricante.Text;
                    produto.Qtd = int.Parse(txtQtd.Text);
                    produto.Id = proximoId;
                    listaProdutos.Add(produto);
                    LimpaTodosCampos();
                    AtualizaDataGrid();
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
            dgvProdutos.ItemsSource = listaProdutos;
            dgvProdutos.Items.Refresh();
        }

        /// <summary>
        /// Método acionado ao realizar o double click na linha do datagrid
        /// </summary>
        private void PegarItemNoGrid(object sender, MouseButtonEventArgs e)
        {
            Produto produto = (Produto)dgvProdutos.SelectedItem;
            txtId.Text = ""+produto.Id;
            txtNome.Text = produto.Nome;
            txtQtd.Text = ""+produto.Qtd;
            txtDescricao.Text = produto.Descricao;
            txtFabricante.Text = produto.Fabricante;
        }

        /// <summary>
        /// Método acionado pelo botão excluir
        /// </summary>
        private void ExcluirProduto(object sender, RoutedEventArgs e)
        {
            if(txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                Produto produto = ObterProdutoPeloId(id);
                if(produto != null)
                {
                    MessageBoxResult message = MessageBox.Show($"Deseja excluir o produto: {produto.Nome}?", "Excluir Produto", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if(message == MessageBoxResult.Yes)
                    {
                        listaProdutos.Remove(produto);
                        AtualizaDataGrid();
                        LimpaTodosCampos();
                    }
                }
            }
        }

        /// <summary>
        /// Obtém o produto com base no id informado
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>O produto encontrado</returns>
        private Produto ObterProdutoPeloId(int id)
        {
            foreach(Produto produto in listaProdutos)
            {
                if(produto.Id == id)
                {
                    return produto;
                }
            }
            return new Produto();
        }

        /// <summary>
        /// Método acionado pelo botão Atualizar
        /// </summary>
        private void AtualizarProduto(object sender, RoutedEventArgs e)
        {
            if(txtId.Text != "")
            {
                Produto produtoAtualizado = new Produto();
                produtoAtualizado.Id = int.Parse(txtId.Text);
                produtoAtualizado.Nome = txtNome.Text;
                produtoAtualizado.Descricao = txtDescricao.Text;
                produtoAtualizado.Qtd = int.Parse(txtQtd.Text);
                produtoAtualizado.Fabricante = txtFabricante.Text;

                for(int i = 0; i < listaProdutos.Count; i++)
                {
                    if (listaProdutos[i].Id == produtoAtualizado.Id)
                    {
                        listaProdutos[i] = produtoAtualizado;
                        break;
                    }
                }

                AtualizaDataGrid();
                
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
    }
}
