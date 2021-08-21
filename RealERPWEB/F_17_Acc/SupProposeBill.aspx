
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="SupProposeBill.aspx.cs" Inherits="RealERPWEB.F_17_Acc.SupProposeBill" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

  
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

        };
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
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="Label9" runat="server" CssClass="lblName lblTxt" Text="Supplier:"></asp:Label>

                                            <asp:TextBox ID="txtSearchSupplier" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:LinkButton ID="imgbtnFindSupplier" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnFindSupplier_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass=" ddlPage" Width="300px" TabIndex="2"></asp:DropDownList>

                                            <asp:Label ID="lblSupplierName" runat="server" Visible="False" Width="300px" CssClass="inputtextbox"></asp:Label>

                                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>


                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="Label5" runat="server" CssClass="lblName lblTxt" Text="From:"></asp:Label>

                                            <asp:TextBox ID="txtFDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label6" runat="server" CssClass=" smLbl_to" Text="To:"></asp:Label>

                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>


                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-8 pading5px asitCol8">

                                            <asp:Label ID="lblPage" runat="server" CssClass="lblName lblTxt" Text="Page Size:" Visible="False"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False" TabIndex="5">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>


                                            <asp:Label ID="Label8" runat="server" CssClass="lblName lblTxt" Text="Pro. Date:"></asp:Label>

                                            <asp:TextBox ID="txtProDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtProDate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtProDate"></cc1:CalendarExtender>


                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>


                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                          <asp:GridView ID="gvProBill" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"


                AutoGenerateColumns="False"
                OnPageIndexChanging="gvReqStatus_PageIndexChanging"
                ShowFooter="True">
                <PagerSettings Position="Top" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No.">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lblCodeo1" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                Width="75px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ActCode" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblactcode" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                Width="75px"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Supplier Name">

                        <ItemTemplate>
                            <asp:Label ID="lblgvAcDesc" runat="server"
                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                Width="300px">
                            </asp:Label>
                        </ItemTemplate>

                       <%-- <FooterTemplate>
                            <asp:LinkButton ID="lFinalUpdate" runat="server" OnClick="lFinalUpdate_Click"  CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>
                        </FooterTemplate>--%>
                        <FooterStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="Left" />

                    </asp:TemplateField>







                    <asp:TemplateField HeaderText="Closing Bill">
                        <ItemTemplate>
                            <asp:Label ID="lblgvClsAmt" runat="server" Font-Size="11px"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsbill")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblgvFClsAmt" runat="server" Font-Size="11px" Height="16px"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Closing Adv">
                        <ItemTemplate>
                            <asp:Label ID="lblgvClsAdv" runat="server" Font-Size="11px"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsadv")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px"></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblgvFClsAdv" runat="server" Font-Size="11px" Height="16px"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proposal Amt.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvProamt" runat="server" Font-Size="11px"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblgvFProamt" runat="server" Font-Size="11px" Height="16px"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Approved Amt.">
                        <ItemTemplate>
                            <asp:TextBox ID="txtgvAproamt" runat="server" Font-Size="11px"
                                Style="text-align: right"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apramt")).ToString("#,##0;(#,##0); ") %>'
                                Width="70px" BorderStyle="None"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblgvFAproamt" runat="server" Font-Size="11px" Height="16px"
                                Style="text-align: right" Width="70px"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle Font-Bold="True" ForeColor="#000" HorizontalAlign="right" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Right" />
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




