using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class cUsuario
{
    public static Usuario ObterUsuarioPeloLoginSenha(string login, string senha)
    {
        return ConsultasUsuario.ObterUsuarioPeloLoginSenha(login,senha);
    }

    public static bool NovoUsuario(string login, string senha)
    {
        return ConsultasUsuario.NovoUsuario(login,senha);
    }
}
