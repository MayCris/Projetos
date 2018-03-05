using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgendaTelefonica.Web.Dados
{
    public class Contatos
    {
        public int IdContato { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Empresa { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public List<Telefone> Telefones { get; set; }
        public List<Email> Emails { get; set; }

        static List<Contatos> ListaContatos { get; set; }

        public Contatos()
        {
            this.IdContato = 0;
            this.Nome = string.Empty;
            this.Endereco = string.Empty;
            this.Empresa = string.Empty;
            this.Telefones = new List<Telefone>();
            this.Emails = new List<Email>();
        }

        public void AdicionarContato(Contatos contato)
        {
            if (ListaContatos == null)
                ListaContatos = new List<Contatos>();
            
            contato.IdContato = ListaContatos.Count > 0 ? ListaContatos.Max(c => c.IdContato) : 0;
            contato.IdContato++;
            contato.Telefone = contato.Telefones[0].Numero.ToString();
            contato.Email = contato.Emails[0].DescricaoEmail;

            ListaContatos.Add(contato);
        }

        public void AtualizarContato(Contatos contato)
        {
            contato.Telefone = contato.Telefones[0].Numero.ToString();
            contato.Email = contato.Emails[0].DescricaoEmail;

            ListaContatos.Remove(SelecionarContato(contato.IdContato));
            ListaContatos.Add(contato);
        }

        public void ExcluirContato(Contatos contato)
        {
            ListaContatos.Remove(SelecionarContato(contato.IdContato));
        }
        public Contatos SelecionarContato(int idContato)
        {
            foreach (Contatos item in ListaContatos)
            {
                if (item.IdContato == idContato)
                {
                    return item;
                }
            }
            return null;
        }

        public List<Contatos> ListarContatos()
        {
            if(ListaContatos != null)
            {
                ListaContatos= ListaContatos.OrderBy(c => c.Nome).ToList();
            }
            return ListaContatos;
        }
    }
}