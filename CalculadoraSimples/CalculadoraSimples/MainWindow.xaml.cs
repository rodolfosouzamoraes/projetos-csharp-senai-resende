using System.Windows;
using System.Windows.Controls;

namespace CalculadoraSimples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ultimoNumero = 0;
        string ultimaOperacoes = "";
        int resultado = 0;
        public MainWindow()
        {
            InitializeComponent();
            txtResultado.Text = "0";
        }

        private void InformaNumero(object sender, RoutedEventArgs e)
        {
            txtEntrada.Text += ((Button)sender).Content.ToString();
        }

        private void InformaResultado(object sender, RoutedEventArgs e)
        {
            if(txtEntrada.Text == "")
            {
                switch (ultimaOperacoes)
                {
                    case "+":
                        resultado = resultado + ultimoNumero;
                        break;
                    case "-":
                        resultado = resultado - ultimoNumero;
                        break;
                    case "*":
                        resultado = resultado * ultimoNumero;
                        break;
                    case "/":
                        resultado = resultado / ultimoNumero;
                        break;
                }
                txtResultado.Text = "" + resultado;
            }            
        }

        private void LimpaResultado(object sender, RoutedEventArgs e)
        {
            txtEntrada.Text = "";
            txtResultado.Text = "0";
            resultado = 0;
            ultimoNumero = 0;
            ultimaOperacoes = "";
        }

        private void Menos(object sender, RoutedEventArgs e)
        {
            if (txtEntrada.Text != "")
            {
                ArmazenaNumerosOperacoes("-", int.Parse(txtEntrada.Text));
                resultado = resultado - int.Parse(txtEntrada.Text);
            }
            MostraResultado();
        }

        private void Mais(object sender, RoutedEventArgs e)
        {
            if (txtEntrada.Text != "")
            {
                ArmazenaNumerosOperacoes("+", int.Parse(txtEntrada.Text));
                resultado = resultado + int.Parse(txtEntrada.Text);
            }
            MostraResultado();
        }

        private void Vezes(object sender, RoutedEventArgs e)
        {
            if (txtEntrada.Text != "")
            {
                ArmazenaNumerosOperacoes("*", int.Parse(txtEntrada.Text));
                resultado = resultado * int.Parse(txtEntrada.Text);
            }
            MostraResultado();
        }

        private void Dividido(object sender, RoutedEventArgs e)
        {
            if (txtEntrada.Text != "")
            {
                ArmazenaNumerosOperacoes("/", int.Parse(txtEntrada.Text));
                resultado = resultado / int.Parse(txtEntrada.Text);
            }
            MostraResultado();
        }

        public void MostraResultado()
        {
            txtResultado.Text = "" + resultado;
            txtEntrada.Text = "";
        }

        public void ArmazenaNumerosOperacoes(string operacao, int numero)
        {
            ultimoNumero = numero;
            ultimaOperacoes = operacao;
        }
    }

    
}
