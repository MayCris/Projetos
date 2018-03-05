<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaContatos.aspx.cs" Inherits="AgendaTelefonica.Web.ListaContatos" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="scripts/jquery.js"></script>

    <script lang='JavaScript' type="text/javascript">
        function SomenteNumero(e) {
            var tecla = (window.event) ? event.keyCode : e.which;
            if ((tecla > 47 && tecla < 58)) return true;
            else {
                if (tecla == 8 || tecla == 0) return true;
                else return false;
            }
        }
</script

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Agenda Telefônica</title>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <style type="text/css">
        .auto-style3 {
            width: 145px;
        }

        </style>
</head>
<body runat="server" style="background-color: darkgray; height: 498px;">
    <div runat="server" style="width: 100%; margin-left:45%; font-size:18px; font-weight:bold; color:darkslateblue">
        Agenda Telefônica
    </div>
    <form id="formContatos" runat="server">
        <div style="width: 100%">
            <table style="width: 100%" runat="server">
                <tr>
                    <td style="width: 25%"></td>
                    <td style="width: 50%">
                        <div id="divPesquisa" runat="server">
                            <table style="width: 100%; border: solid;" runat="server">
                                <tr>
                                    <td style="width: 25%">Nome:</td>
                                    <td style="width: 50%">
                                        <asp:TextBox ID="txtPesqNome" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td style="width: 25%"></td>
                                </tr>
                                <tr>
                                    <td>Telefone:</td>
                                    <td>
                                        <asp:TextBox ID="txtPesqTelefone" runat="server" Width="80%" MaxLength="9" onkeypress='return SomenteNumero(event)'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email:</td>
                                    <td>
                                        <asp:TextBox ID="txtPesqEmail" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnNovo" runat="server" Text="Novo" OnClick="btnNovo_Click" Width="38%" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divGrid" runat="server" style="border:solid;">

                            <asp:GridView ID="gridContatos" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="IdContato" OnRowDataBound="gridContatos_RowDataBound" OnRowCommand="gridContatos_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="IdContato" Visible="false"></asp:BoundField>
                                    <asp:BoundField DataField="Nome" HeaderText="Contato" ItemStyle-Width="30%"></asp:BoundField>
                                    <asp:BoundField DataField="Telefone" HeaderText="Telefone" ItemStyle-Width="10%"></asp:BoundField>
                                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="30%"></asp:BoundField>
                                    <asp:TemplateField  ItemStyle-Width="5%">                                        
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="Visualizar" runat="server" CommandArgument='<%# Eval("IdContato") %>' Text="Visualizar"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="Editar" runat="server" CommandArgument='<%# Eval("IdContato") %>' Text="Editar"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="Excluir" runat="server" CommandArgument='<%# Eval("IdContato") %>' Text="Excluir"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </td>
                    <td style="width: 25%"></td>
                </tr>
                <tr>
                    <td style="width: 25%"></td>
                    <td style="width: 50%">
                        <div id="divCadastro" runat="server" style="display:block;">
                            <table id="tblCadastro" runat="server" style="width: 100%; border: solid;">
                                <tr>
                                    <td style="width: 15%">Nome:</td>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtNome" runat="server" Width="95%" MaxLength="100"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator runat="server" id="reqNome" controltovalidate="txtNome" errormessage="Nome obrigatório!" />--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">Empresa:</td>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtEmpresa" runat="server" Width="95%" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">Endereço:</td>
                                    <td style="width: 85%">
                                        <asp:TextBox ID="txtEndereco" runat="server" Width="95%" MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 20px"></td>
                                </tr>
                                <tr>
                                    <td>Email:</td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width:85%">
                                                    <asp:TextBox ID="txtEmail" runat="server" Width="97%" MaxLength="100"></asp:TextBox>
                                                </td>
                                                <td>

                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                                <tr>
                                    <td>Email:</td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td  style="width:85%">
                                                    <asp:TextBox ID="txtEmail2" runat="server" Width="97%" MaxLength="100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlClassEmail2" runat="server">
                                                        <asp:ListItem Value="2" Text="Trabalho"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Casa"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Outros"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email:</td>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td  style="width:85%">
                                                    <asp:TextBox ID="txtEmail3" runat="server" Width="97%" MaxLength="100"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlClassEmail3" runat="server">
                                                        <asp:ListItem Value="2" Text="Trabalho"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Casa"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Outros"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 20px"></td>
                                </tr>
                                <tr>
                                    <td>Telefone:</td>
                                    <td>
                                        <table style="width: 100%; border: 0">
                                            <tr>
                                                <td class="auto-style3">
                                                    <asp:TextBox ID="txtTelefone" runat="server" Width="131px" MaxLength="9" onkeypress='return SomenteNumero(event)'></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator runat="server" id="reqTelefone" controltovalidate="txtTelefone" errormessage="Telefone obrigatório!" />--%>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Telefone:</td>
                                    <td>
                                        <table style="width: 100%; border: 0">
                                            <tr>
                                                <td class="auto-style3">
                                                    <asp:TextBox ID="txtTelefone2" runat="server" Width="131px" MaxLength="9" onkeypress='return SomenteNumero(event)'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlClassTel2" runat="server">
                                                        <asp:ListItem Value="2" Text="Trabalho"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Casa"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Outros"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Telefone:</td>
                                    <td>
                                        <table style="width: 100%; border: 0">
                                            <tr>
                                                <td class="auto-style3">
                                                    <asp:TextBox ID="txtTelefone3" runat="server" Width="131px" MaxLength="9" onkeypress='return SomenteNumero(event)'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlClassTel3" runat="server">
                                                        <asp:ListItem Value="2" Text="Trabalho"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Casa"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Outros"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="height: 20px"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" Width="15%" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="width: 25%"></td>
                </tr>
            </table>
        </div>

    </form>


</body>
</html>
