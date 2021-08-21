<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptFixAssetStatus02.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.RptFixAssetStatus02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-10  pading5px  asitCol10">

                                            <asp:Label ID="lblEmployee" runat="server" CssClass=" lblName lblTxt" Text="Employee List:"></asp:Label>

                                            <asp:TextBox ID="txtSrchEmployeeorRes" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                            <asp:LinkButton ID="ImgbtnFindEmp" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindEmp_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlEmporResList" runat="server" AutoPostBack="True" Width="350" CssClass="ddlPage"></asp:DropDownList>

                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="Viewequipst" runat="server">
                                <asp:GridView ID="gvrecandref" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvrecandref_RowDataBound" ShowFooter="True" Width="522px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo5" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDesc" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefDesc" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefdate" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </asp:View>
                            <asp:View ID="ViewRecIssaRefund" runat="server">
                                <asp:GridView ID="gvrecissaref" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvrecissaref_RowDataBound" ShowFooter="True" Width="522px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo6" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDescres" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recpdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecdateres" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueDescres" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuedesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissuedateres" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuedate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefDescres" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefdateres" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refunddat")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Store Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbalqtyres" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>  
                            <asp:View ID="ViewSum" runat="server">
                                <asp:GridView ID="gvrecissarefsum" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="522px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo7" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecDescsum" runat="server" Font-Underline="False"
                                                    Style="color: Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Received">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrecqtysum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Issued">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueqtyres0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Refund">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefqtysum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "refundqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Store Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbalqtysum" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </asp:View>
                        </asp:MultiView>

                    </div>
                </div>
            </div>


        
                                <%--<tr>
                                    <td class="style61">
                                        <asp:Label ID="lblEmployee" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="TEXT-ALIGN: left; color: #FFFFFF;" Text="Employee List:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style62">
                                        <asp:TextBox ID="txtSrchEmployeeorRes" runat="server" BorderStyle="None" 
                                            Font-Bold="True" Font-Size="12px" TabIndex="15" Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style63">
                                        <asp:ImageButton ID="ImgbtnFindEmp" runat="server" BorderStyle="None" 
                                            Height="19px" ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindEmp_Click" 
                                            TabIndex="16" Width="16px" />
                                    </td>
                                    <td class="style64">
                                        <asp:DropDownList ID="ddlEmporResList" runat="server" AutoPostBack="True" 
                                            Font-Size="12px" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            TabIndex="17" Width="355px">
                                        </asp:DropDownList>
                                        <cc1:ListSearchExtender ID="ddlEmporResList_ListSearchExtender" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlEmporResList">
                                        </cc1:ListSearchExtender>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnOk" runat="server" BackColor="#000066" 
                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="White" onclick="lbtnOk_Click" 
                                            style="text-align: center" Width="52px">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>--%>
                      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

