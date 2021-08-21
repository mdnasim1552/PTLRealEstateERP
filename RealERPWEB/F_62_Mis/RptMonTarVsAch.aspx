<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptMonTarVsAch.aspx.cs" Inherits="RealERPWEB.F_62_Mis.RptMonTarVsAch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                          <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass=" smLbl_to">From</asp:Label>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>


                                         <asp:Label ID="lbltodate" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <div class="colMdbtn pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                            <asp:CheckBox ID="chkwithoutrep" runat="server"  Text="Without Replacement" Visible="False"
                                             CssClass=" btn btn-primary checkbox"/>





                                    </div>
                                   



                                </div>
                               
                            </div>




                        </fieldset>

                           <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="sal" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvSalSummery" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="931px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "thead1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtSh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "thead2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtCs" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "thead3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtApt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "thead4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtOt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bhead1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBSh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bhead2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBCs" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bhead3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBApt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bhead4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBOt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chead1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCSh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chead2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCCs" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chead3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCApt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chead4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCOt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TS.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shead1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtSSh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TS.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shead2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtSCs" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TS.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shead3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtSApt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TS.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shead4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtSOt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ahead1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFASh" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ahead2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFACS" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ahead3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAApt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ahead4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="35px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAot" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="35px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
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
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="SalAmt" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvSalAmt" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="931px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc0" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate0" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salval1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtShA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate1" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salval2")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtCsA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate2" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salval3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtAptA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate3" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salval4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtOtA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate4" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brecvam1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBShA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate5" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brecvam2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBCsA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate6" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brecvam3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBAptA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate7" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "brecvam4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBOtA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate8" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crecvam1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCShA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate9" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crecvam2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCCsA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate10" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crecvam3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCAptA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="C.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate11" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "crecvam4")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFCOtA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TR.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate12" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecvam1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtRShA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TR.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate13" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecvam2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtRCsA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TR.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate14" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecvam3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtRAptA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TR.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate15" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trecvam4")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFtROtA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R.Shop">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate16" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvbal1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFRShA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R.CS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate17" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvbal2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFRCSA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R.Apt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate18" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvbal3")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFRAptA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="R.Others">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate19" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvbal4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFRotA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="White" Width="60px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
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
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewDCollASale" runat="server">
                                <div class=" table-responsive">
                                <asp:GridView ID="gvSalVsColl" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="gvSalVsColl_RowDataBound" ShowFooter="True" Width="531px"    CssClass="table table-striped table-hover table-bordered grvContentarea" >
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptcode" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Team">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDepartment" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mdeptname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "deptname").ToString().Trim().Length > 0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mdeptname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="180px">
                                                                     
                                                                     
                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Sale Target &lt;br&gt; Monthly">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmonsalamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "msaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Actual  &lt;br&gt; Monthly ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvuatsalamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uasaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Short Fall">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsalsfall" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salsfall")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Achieved in  &lt;br&gt; % ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperontsale" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontsale")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Last Month Position &lt;br&gt; Sales">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpmonsalamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pmonsaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Target  &lt;br&gt; Today's">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtsalamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tsaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Sales Actual  &lt;br&gt; Today's">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtatsaleamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tasaleamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvseprationsale" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Bold="true" Font-Size="11px"
                                                    Style="background-color: Transparent" Text=":" Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection Target  &lt;br&gt; Monthly">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmoncollamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mcollamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection Actual &lt;br&gt; Monthly ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvuatcollamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uacollamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Short Fall">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcollsfall" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collsfall")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Achieved in  &lt;br&gt; % ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperontcoll" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perontcoll")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Collection Target  &lt;br&gt; Today's">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtcollamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tcollamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Collection Actual  &lt;br&gt; Today's  ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtatcollamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tacollamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Month Position &lt;br&gt; Collection">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpmoncollamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pmoncollamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                     <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                    </div>

                            </asp:View>
                            <asp:View ID="ViewSaleRegister" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvSalReg" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="478px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialnoidsg" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales No" FooterText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvsalesno" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "salesno")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White" />
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sale Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvsaledate" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "saledate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Client Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvcustnamesg" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Flat No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvunitname" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvProjectname" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Dept Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvdeptnamesg" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Executive Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvexucutivesg" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px"
                                                                Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "executive")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Sale Amount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="txtFTotal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsalamt" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saleamt")).ToString("#,##0;-#,##0; ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right"
                                                            VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>



                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="12">
                                            <asp:GridView ID="gvTransSum" runat="server"
                                                AutoGenerateColumns="False"
                                                ShowFooter="True" Width="616px">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="amt1">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt1" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt2" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt3" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt4" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt5">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt5" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt6" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt6")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt7" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt7")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt8">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt8" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt8")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt9">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt9" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt9")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt10" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt10")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt11">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt11" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt11")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt12">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt12" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt12")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt13">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt13" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt13")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt14">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt14" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt14")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt15">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt15" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt15")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt16">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt16" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt16")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt17">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt17" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt17")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt18">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt18" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt18")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt19">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt19" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt19")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="amt20">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt20" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt20")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="amt21">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt21" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt21")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt21" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFnetamt" runat="server" Font-Size="11px" ForeColor="White"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewRealCollection" runat="server">
                                <asp:GridView ID="gvrcoll" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="gvrcoll_RowDataBound" ShowFooter="True" Width="531px">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoidre" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptcoderc" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDepartmentrc" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Collection">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtocollection" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Clearance">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreconamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Cheque Deposited">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvchqdep" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depchq")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="In Hand(Returned Cheque)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvinhrchq" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhrchq")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="In Hand(Fresh Cheque)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvinhfchq" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhfchq")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="In Hand(Post Dated Cheque)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvinhpchq" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inhpchq")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Replacement Cheque">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrepchq" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repchq")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Net Collection(Without Replacement)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvncollamt" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ncollamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>




                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>
                            <asp:View ID="ViewreconSummary" runat="server">
                                <asp:GridView ID="gvbrecon" runat="server" AutoGenerateColumns="False"
                                    OnRowDataBound="gvbrecon_RowDataBound" ShowFooter="True" Width="300px">
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvslbrec" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdeptcodebrec" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptcode")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDepartmentbrec" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: Left; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Clearance">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreconamtbrec" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Adjustment">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvadjsutment" runat="server" BorderColor="#99CCFF"
                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                    Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjamt")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#333333" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </asp:View>

                        </asp:MultiView>

                    </div>
                </div>
            </div>

    
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

