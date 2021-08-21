<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccPurchaseFor.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccPurchaseFor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style18 {
            width: 712px;
        }

        .style175 {
            width: 239px;
        }

        .style176 {
            width: 215px;
        }

        .style178 {
            width: 194px;
        }

        .style189 {
            width: 526px;
        }

        .style190 {
        }

        .style191 {
            width: 101px;
        }

        .style192 {
            width: 230px;
        }

        .style193 {
            width: 236px;
        }

        .style195 {
            height: 20px;
        }

        .style199 {
            width: 125px;
        }

        .style200 {
            width: 95px;
        }

        .style201 {
            height: 20px;
            width: 17px;
        }

        .style202 {
            width: 17px;
        }

        .style203 {
            width: 13px;
        }

        .style204 {
            width: 8px;
        }

        .style205 {
            width: 195px;
        }

        .style206 {
            height: 20px;
            width: 11px;
        }

        .style207 {
            width: 11px;
        }

        .style208 {
            width: 99px;
        }

        .style209 {
            width: 165px;
        }
    </style>

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    
   
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
                        <asp:Panel ID="Panel1" runat="server">

                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-5 pading5px">
                                        
                                        <asp:Label ID="lblDate" runat="server" CssClass="lblTxt lblName"> Voucher Date</asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputtextbox" ReadOnly="True"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy ddd" TargetControlID="txtdate">
                                        </cc1:CalendarExtender>
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>
                                    <div class="col-md-4">
                                        <asp:ImageButton ID="ibtnvounu" runat="server" Height="20px"
                                            ImageUrl="~/Image/movie_26.gif" OnClick="ibtnvounu_Click" Width="145px"
                                            Visible="False" />
                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName">Current Voucher No.</asp:Label>
                                        <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ToolTip="You Can Change Voucher Number." ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                            </asp:Panel>
                    </div>
                    <asp:Panel ID="pnlBill" runat="server" Visible="False">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblBillList" runat="server" CssClass="lblTxt lblName" Text="Bill List"></asp:Label>
                                            <asp:TextBox ID="txtBillno" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgSearchBillno" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchBillno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlBillList" runat="server" CssClass="form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">



                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnSelectBill" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectBill_Click">Select</asp:LinkButton>

                                            </div>



                                        </div>

                                    </div>
                                </div>
                            </fieldset>
                            <asp:GridView ID="gvPurFor" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL"
                                                Font-Names="Verdana" Font-Size="11px"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actcode").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" +                                                  
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") %>'
                                                Width="350px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                                        ItemStyle-Font-Size="11px">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Rate"
                                        ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="White" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvTDramt" runat="server" BackColor="Transparent" ReadOnly="true"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDramt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvTCramt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ReadOnly="true"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCramt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#F5F5F5" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                            </asp:GridView>

                            <div class="col-md-2 col-md-offset-2">
                             <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn"
                                OnClick="lnkFinalUpdate_Click"
                               >Final Update</asp:LinkButton>
                                </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>




            <%--<table style="width: 95%;">
                    <tr>
                        <td class="style199">
                            <asp:Label ID="lblBillList" runat="server" Style="text-align: right"
                                Text="Bill List" Width="90px" Font-Bold="True" Font-Size="14px"
                                ForeColor="White"></asp:Label>
                        </td>
                        <td class="style199">
                            <asp:TextBox ID="txtBillno" runat="server" BorderStyle="None"
                                ToolTip="You Can Change Voucher Number." Width="60px"></asp:TextBox>
                        </td>
                        <td class="style208">
                            <asp:ImageButton ID="imgSearchBillno" runat="server" Height="16px"
                                ImageUrl="~/Image/find_images.jpg" OnClick="imgSearchBillno_Click"
                                Style="margin-left: 0px" Width="16px" />
                        </td>
                        <td class="style176" colspan="3">
                            <asp:DropDownList ID="ddlBillList" runat="server" Width="600px">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:LinkButton ID="lbtnSelectBill" runat="server" CssClass="button"
                                Font-Bold="True" Font-Size="12px" ForeColor="White"
                                OnClick="lbtnSelectBill_Click" Style="text-align: center;" Width="97px">Select Bill</asp:LinkButton>
                        </td>
                        <td class="style178">&nbsp;</td>
                        <td class="style192">&nbsp;</td>
                        <td class="style193">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <asp:GridView ID="gvPurFor" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#7FBF41" BorderStyle="Solid" BorderWidth="2px"
                                ShowFooter="True" Width="689px">
                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL"
                                                Font-Names="Verdana" Font-Size="11px"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actcode").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" +                                                  
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") %>'
                                                Width="350px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Quantity"
                                        ItemStyle-Font-Size="11px">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvqty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Size="12px" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField FooterText="Total" HeaderText="Rate"
                                        ItemStyle-Font-Size="11px">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle ForeColor="White" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvTDramt" runat="server" BackColor="Transparent" ReadOnly="true"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDramt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvTCramt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" ReadOnly="true"
                                                CssClass="GridTextbox" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCramt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox" Height="22px" ReadOnly="True" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#333333" />
                                <PagerStyle HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                            </asp:GridView>
                        </td>
                    </tr>


                    <tr>
                        <td class="style199" colspan="3">&nbsp;</td>
                        <td class="style190">&nbsp;</td>
                        <td class="style191">&nbsp;</td>
                        <td class="style209">&nbsp;</td>
                        <td>
                           
                        </td>
                        <td class="style178">&nbsp;</td>
                        <td class="style192">&nbsp;</td>
                        <td class="style193">&nbsp;</td>
                    </tr>




                </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

