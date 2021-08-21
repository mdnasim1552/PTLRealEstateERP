<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CodeLinkCoReBa.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.CodeLinkCoReBa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('#<%=this.grvacc.ClientID%>').tblScrollable();

            $('.chzn-select').chosen({ search_contains: true });

        };
    </script>

    <style>
        .moduleItemWrpper{
            overflow:visible;
        }
    </style>

    





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <div class="card card-fluid" style="min-height:550px;">
                <div class="card-body">

                    <div class="row">


                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label lblmargin-top9px" >Page Size</label>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                               
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="custom-select"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" 
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        </div>


                    <div class="row">

                        

                        
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-condensed table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15"
                            OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True" BorderStyle="None" Width="724px">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle />
                                    <ItemStyle ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" Account Code" HeaderStyle-Width="100px">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvactcode" runat="server" Style="text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Accounts">

                                    <HeaderTemplate>
                                        <table class="table-responsive">
                                            <tr>
                                                <td class="style63">
                                                    <asp:Label ID="Label8" runat="server" Text="Head of Accounts"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="style61">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <HeaderStyle />
                                </asp:TemplateField>







                                <asp:TemplateField HeaderText="Cost Code Description">
                                    <EditItemTemplate>
                                        <asp:Panel ID="pnlTeam" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                            BorderWidth="1px">


                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSearchteam" runat="server" Visible="false" CssClass=" inputtextbox" TabIndex="4" Width="50px"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="ibtnSrchteam" Visible="false" runat="server" OnClick="ibtnSrchteam_Click" CssClass="btn btn-success srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlteam" runat="server" CssClass="chzn-select form-control  inputTxt" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcatdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acgdesc")) %>'
                                            Width="320px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>







                            </Columns>

                             <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                        </asp:GridView>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

