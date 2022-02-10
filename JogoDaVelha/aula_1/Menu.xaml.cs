using System.Windows;

namespace aula_1
{
    /// <summary>
    /// Lógica da tela de menu do jogo
    /// </summary>
    public partial class Menu : Window
    {
        string nomeJogadorX;
        string nomeJogadorO;
        public Menu()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Método Verificação do Jogador
        /// </summary>
        private void Jogar(object sender, RoutedEventArgs e)
        {
            nomeJogadorX = txtJogadorX.Text;
            nomeJogadorO = txtJogadorO.Text;
            if(nomeJogadorO == "" || nomeJogadorX == "") // Verificando se o nome dos jogadores estão vazios
            {
                txtAvisoDeErro.Text = "Preencha todos os campos";
            }
            else
            {
                IniciarJogo();
            }
        }

        /// <summary>
        /// Inicia o jogo em outra tela
        /// </summary>
        private void IniciarJogo()
        {
            LimpaCampos();
            MainWindow janelaJogo = new MainWindow(nomeJogadorX, nomeJogadorO, this);
            janelaJogo.Show(); // Exibe a tela do jogo
            Hide(); // Esconde a tela de menu
        }

        private void LimpaCampos()
        {
            txtJogadorX.Text = "";
            txtJogadorO.Text = "";
            txtAvisoDeErro.Text = "";
        }

        private void Sair(object sender, RoutedEventArgs e)
        {
            Close(); // Fecha a tela
        }
    }
}
