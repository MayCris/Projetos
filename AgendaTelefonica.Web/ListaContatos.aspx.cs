using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgendaTelefonica.Web.Dados;


namespace AgendaTelefonica.Web
{
    public partial class ListaContatos : System.Web.UI.Page
    {
        Contatos contato = new Contatos();

        //Identifica operações Incluir, Alterar, Excluir e Visualizar
        static string operacao;

        //Mantém o código do Contato a ser Alterado ou excluído ou visualizado
        static int idContato;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                HabilitarCampos(false);
            }
        }
                
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Executar();

            if (string.IsNullOrEmpty(txtPesqNome.Text) && string.IsNullOrEmpty(txtPesqTelefone.Text) && string.IsNullOrEmpty(txtPesqEmail.Text))
            {
                gridContatos.DataSource = contato.ListarContatos();
            }
            else
            {
                List<Contatos>lista = contato.ListarContatos();
                List<Contatos> listaExibir = new List<Contatos>();

                //Busca das informações na Lista de Contatos, de acordo com o fitro informado
                for (int i = 0; i < lista.Count; i++)
                {
                    if (!string.IsNullOrEmpty(txtPesqNome.Text) && lista[i].Nome.ToLower().Contains(txtPesqNome.Text.ToLower()))
                    {
                        listaExibir.Add(lista[i]);
                    }

                    else if (!string.IsNullOrEmpty(txtPesqTelefone.Text))
                    {
                        for (int j = 0; j < lista[i].Telefones.Count; j++)
                        {
                            if(lista[i].Telefones[j].Numero.Contains(txtPesqTelefone.Text))
                            {
                                listaExibir.Add(lista[i]);
                                break;
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(txtPesqEmail.Text))
                    {
                        for (int j = 0; j < lista[i].Emails.Count; j++)
                        {
                            if (lista[i].Emails[j].DescricaoEmail.ToLower().Contains(txtPesqEmail.Text.ToLower()))
                            {
                                listaExibir.Add(lista[i]);
                                break;
                            }
                        }
                    }
                }

                gridContatos.DataSource = listaExibir.OrderBy(c => c.Nome).ToList();
            }

            gridContatos.DataBind();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            divCadastro.Style.Add("display", "block");
            LimparCampos();
            HabilitarCampos(true);
            operacao = "I";
            idContato = 0;
            txtNome.Focus();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarDados())
                return;

            contato.IdContato = idContato;
            contato.Nome = this.txtNome.Text;
            contato.Empresa = this.txtEmpresa.Text;
            contato.Endereco = this.txtEndereco.Text;

            Telefone telefone = new Telefone();
            telefone.Numero = this.txtTelefone.Text;
            telefone.Classificacao = 1;
            contato.Telefones.Add(telefone);

            telefone = new Telefone();
            telefone.Numero = this.txtTelefone2.Text;
            telefone.Classificacao = int.Parse(ddlClassTel2.SelectedValue);
            contato.Telefones.Add(telefone);

            telefone = new Telefone();
            telefone.Numero = this.txtTelefone3.Text;
            telefone.Classificacao = int.Parse(ddlClassTel3.SelectedValue);
            contato.Telefones.Add(telefone);

            Email email = new Email();
            email.DescricaoEmail = this.txtEmail.Text;
            email.Classificacao = 1;
            contato.Emails.Add(email);

            if (!string.IsNullOrEmpty(this.txtEmail2.Text))
            {
                email = new Email();
                email.DescricaoEmail = this.txtEmail2.Text;
                email.Classificacao = int.Parse(ddlClassEmail2.SelectedValue);
                contato.Emails.Add(email);
            }

            if (!string.IsNullOrEmpty(this.txtEmail3.Text))
            {
                email = new Email();
                email.DescricaoEmail = this.txtEmail3.Text;
                email.Classificacao = int.Parse(ddlClassEmail3.SelectedValue);
                contato.Emails.Add(email);
            }

            if (operacao == "I")
            {
                //Incusão de novo Contato
                contato.AdicionarContato(contato);
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Contato Adicionado !')</script>");
            }
            else
            {
                //Alterar dados de um Contato já cadastrado
                contato.AtualizarContato(contato);
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Contato Atualizado !')</script>");
            }

            this.Executar();
        }

        protected void gridContatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Célula de armazenamento do IdContato
            e.Row.Cells[0].Visible = false;
        }

        protected void gridContatos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            divCadastro.Style.Add("display", "block");

            //Identifica o registro selecionado na lista
            List<Contatos> listaCompleta = contato.ListarContatos();
            var contatoSelecionado = (
                                        from c in listaCompleta
                                        where c.IdContato == int.Parse(e.CommandArgument.ToString())
                                        select c
                                     ).First();

            idContato = contatoSelecionado.IdContato;
            txtNome.Text = contatoSelecionado.Nome;
            txtEmpresa.Text = contatoSelecionado.Empresa;
            txtEndereco.Text = contatoSelecionado.Endereco;

            contatoSelecionado.Telefones.OrderBy(t => t.Classificacao).ToList();
            contatoSelecionado.Emails.OrderBy(email => email.Classificacao).ToList();

            txtEmail.Text = contatoSelecionado.Emails[0].DescricaoEmail;
            txtTelefone.Text = contatoSelecionado.Telefones[0].Numero.ToString();

            if (contatoSelecionado.Telefones.Count > 1)
            {
                txtTelefone2.Text = contatoSelecionado.Telefones[1].Numero.ToString();
                ddlClassTel2.SelectedValue = contatoSelecionado.Telefones[1].Classificacao.ToString();

                if (contatoSelecionado.Telefones.Count > 2)
                {
                    txtTelefone3.Text = contatoSelecionado.Telefones[2].Numero.ToString();
                    ddlClassTel3.SelectedValue = contatoSelecionado.Telefones[2].Classificacao.ToString();
                }
            }

            txtEmail.Text = contatoSelecionado.Emails[0].DescricaoEmail;
            if (contatoSelecionado.Emails.Count > 1)
            {
                txtEmail2.Text = contatoSelecionado.Emails[1].DescricaoEmail;
                ddlClassEmail2.SelectedValue = contatoSelecionado.Emails[1].Classificacao.ToString();

                if (contatoSelecionado.Emails.Count > 2)
                {
                    txtEmail3.Text = contatoSelecionado.Emails[2].DescricaoEmail;
                    ddlClassEmail3.SelectedValue = contatoSelecionado.Emails[2].Classificacao.ToString();
                }
            }

            if (e.CommandName == "Editar")
            {
                HabilitarCampos(true);
                operacao = "A";
            }
            else if (e.CommandName == "Excluir")
            {
                //Exclusão de um Contato
                HabilitarCampos(false);
                contato.ExcluirContato(contatoSelecionado);
                Executar();
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Contato Excluído !')</script>");
            }
            else
            {
                HabilitarCampos(false);
                operacao = "V";
            }
        }

        protected void HabilitarCampos(bool habilita)
        {
            this.txtNome.Enabled = habilita;
            this.txtEmpresa.Enabled = habilita;
            this.txtEndereco.Enabled = habilita;
            this.txtTelefone.Enabled = habilita;
            this.txtEmail.Enabled = habilita;
            this.txtTelefone2.Enabled = habilita;
            this.txtEmail2.Enabled = habilita;
            this.txtTelefone3.Enabled = habilita;
            this.txtEmail3.Enabled = habilita;

            this.ddlClassTel2.Enabled = habilita;
            this.ddlClassTel3.Enabled = habilita;
            this.ddlClassEmail2.Enabled = habilita;
            this.ddlClassEmail3.Enabled = habilita;
        }
        
        protected void CarregarGrid()
        {
            gridContatos.DataSource = contato.ListarContatos();
            gridContatos.DataBind();
        }

        //Execução de ações de limpeza de informações para uma nova operação em tela
        protected void Executar()
        {
            CarregarGrid();
            LimparCampos();

            operacao = string.Empty;
            idContato = 0;

            divCadastro.Style.Add("display", "none");
        }

        protected void LimparCampos()
        {
            this.txtNome.Text = string.Empty;
            this.txtEmpresa.Text = string.Empty;
            this.txtEndereco.Text = string.Empty;
            this.txtTelefone.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtTelefone2.Text = string.Empty;
            this.txtEmail2.Text = string.Empty;
            this.txtTelefone3.Text = string.Empty;
            this.txtEmail3.Text = string.Empty;
        }

        /// <summary>
        /// Validação das informações obrigatórias e da informação digitada 
        /// </summary>
        /// <returns></returns>
        protected bool ValidarDados()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                txtNome.Focus();
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Nome é Obrigatóio!')</script>");
                return false;
            }

            if (string.IsNullOrEmpty(txtTelefone.Text))
            {
                txtTelefone.Focus();
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Telefone é Obrigatório!')</script>");
                return false;
            }
                        
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (!string.IsNullOrEmpty(txtEmail.Text) && !rg.IsMatch(txtEmail.Text))
            {
                txtEmail.Focus();
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Email inválido !')</script>");
                return false;
            }
            if (!string.IsNullOrEmpty(txtEmail2.Text) && !rg.IsMatch(txtEmail2.Text))
            {
                txtEmail2.Focus();
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Email inválido !')</script>");
                return false;
            }
            if (!string.IsNullOrEmpty(txtEmail3.Text) && !rg.IsMatch(txtEmail3.Text))
            {
                txtEmail3.Focus();
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Message Box", "<script language = 'javascript'>alert('Email inválido !')</script>");
                return false;
            }

            return true;
        }
    }
}