<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccBankCheque.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccBankCheque" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">


  
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">

         $(document).ready(function () {
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

         }); 
         function pageLoaded() {


             $("input, select").bind("keydown", function (event) {
                 var k1 = new KeyPress();
                 k1.textBoxHandler(event);

             });



             var gv = $('#<%=this.gvCheque.ClientID %>');
            gv.Scrollable();
        }
     </script>



     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Bank Name"></asp:Label>
                                        <asp:TextBox ID="txtAccSearch" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="IbtnSearchAcc" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="IbtnSearchAcc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click" CssClass="btn btn-primary primaryBtn">Ok</asp:LinkButton>

                                        </div>
                                    </div>
                                    <div class="colMdbtn pading5px">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" Visible="false"></asp:Label>

                                    </div>

                                </div>
                                </div>
                        </fieldset>
                        <fieldset class="scheduler-border fieldset_B">
                            <asp:Panel ID="Gengroup" runat="server">
                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol3">
                                        <asp:CheckBox ID="chqBank" runat="server" AutoPostBack="True" Style="margin-left: 0;" Text="Generate" CssClass="btn btn-primary primaryBtn chkBoxControl " OnCheckedChanged="chqBank_CheckedChanged" />
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="from-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblChequeno1" runat="server" CssClass="lblTxt lblName" Text="First Cheque No"></asp:Label>
                                        <asp:TextBox ID="txtFirstChq" runat="server" CssClass=" inputtextbox" Width="90px"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblChequeno2" runat="server" CssClass="lblTxt lblName" Text="Last Cheque No"></asp:Label>
                                        <asp:TextBox ID="txtFirstChq1" runat="server" CssClass=" inputtextbox" Width="90px"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="btnGenerate" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="btnGenerate_Click">Generate</asp:LinkButton>

                                        </div>
                                        <div class="clearfix"></div>
                                    </div>


                                </div>
                            </asp:Panel>
                        </fieldset>



                        <asp:GridView ID="gvCheque" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Chequeno">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAsst" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:LinkButton ID="lUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Issued">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="checkIssued" runat="server" Style="text-align: center"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flag"))=="True" %>'
                                            Enabled='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "flag")) == "True") ? false : true %>'
                                            Width="50px" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vounum">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvou" runat="server" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtStatus" runat="server" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bstatus")) %>'
                                            Enabled='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "bstatus")) == "Cancel") ? false : true %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                    </div>
            </div>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


