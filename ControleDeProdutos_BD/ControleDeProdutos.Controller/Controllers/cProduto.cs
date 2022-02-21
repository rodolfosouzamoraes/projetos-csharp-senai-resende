using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  class cProduto
{
    public static List<Produto> SelecionaTodosProdutos()
    {
        return ConsultasProduto.ObterTodosProdutos();
    }

    public static bool NovoProduto(string nome, string descricao, string fabricante, int quantidade)
    {
        return ConsultasProduto.InserirProduto(nome, descricao, fabricante, quantidade);
    }

    public static bool ExcluirProduto(int idProduto)
    {
        return ConsultasProduto.ExluirProdutoPeloID(idProduto);
    }
}
