
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BgdEstStdAna.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdEstStdAna" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName">Work Name</asp:Label>

                                            <asp:TextBox ID="txtSrcWrk" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindWork" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindWork_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblWrkdesc" runat="server" CssClass=" form-control inputTxt" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlWork" runat="server" CssClass=" form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-3 pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>
                    <div class="row">
                        <asp:Panel ID="PanelSub" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Item Code</asp:Label>

                                            <asp:TextBox ID="txtSrcItem" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindItem" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindItem_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-3 pading5px">

                                            <asp:DropDownList ID="ddlItem" runat="server" CssClass=" form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Resource Code</asp:Label>

                                            <asp:TextBox ID="txtSrcResouce" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindResource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindResource_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:DropDownList ID="ddlResource" runat="server" CssClass=" form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lbtnSelect" runat="server" OnClick="lbtnSelect_Click" CssClass="btn btn-primary primaryBtn">Add</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>




                            <%-- <table style="width:100%;">
                                            <tr>
                                                <td class="style14">
                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: left;" Text="Item Code:" Width="80px"></asp:Label>
                                                </td>
                                                <td class="style15">
                                                    <asp:TextBox ID="txtSrcItem" runat="server" BorderStyle="None" 
                                                        CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style17">
                                                    <asp:ImageButton ID="ibtnFindItem" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindItem_Click" />
                                                </td>
                                                <td class="style26">
                                                    <asp:DropDownList ID="ddlItem" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        Width="300px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style24">
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
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style14">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="12px" 
                                                        style="color: #FFFFFF; text-align: left;" Text="Resource Code:" Width="90px"></asp:Label>
                                                </td>
                                                <td class="style15">
                                                    <asp:TextBox ID="txtSrcResouce" runat="server" BorderStyle="None" 
                                                        CssClass="txtboxformat" Width="80px"></asp:TextBox>
                                                </td>
                                                <td class="style17">
                                                    <asp:ImageButton ID="ibtnFindResource" runat="server" Height="18px" 
                                                        ImageUrl="~/Image/find_images.jpg" onclick="ibtnFindResource_Click" />
                                                </td>
                                                <td class="style26">
                                                    <asp:DropDownList ID="ddlResource" runat="server" Font-Bold="True" 
                                                        Font-Size="12px" Width="300px">
                                                    </asp:DropDownList>
                                                    <asp:LinkButton ID="lbtnSelect" runat="server" BackColor="#003366" 
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                        Font-Size="12px" Height="16px" onclick="lbtnSelect_Click" 
                                                        style="text-align: center; color: #FFFFFF; font-size: 14px;" Width="40px">Add</asp:LinkButton>
                                                </td>
                                                <td class="style24">
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
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>--%>
                        </asp:Panel>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvEstAna" runat="server" AutoGenerateColumns="False"
                            CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="margin-right: 0px" Width="430px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkcode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "wrkdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                            Width="170px">
                                                                            
                                                                            
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lUpdate" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lUpdate_Click" Style="text-decaration: none;">Update </asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtbasenumber" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bnumber"))  %>'
                                            Width="120px"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnTotql" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="#000" OnClick="lnkbtnTotql_Click"
                                            Style="text-decaration: none;">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvunit" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length &lt;br /&gt; 1">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvlength" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnght")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity  &lt;br /&gt; 2">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvqty" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight  &lt;br /&gt; 3">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvweight" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "weight")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFWeight" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Weight  &lt;br /&gt; 4=1*2*3 ">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotalweight" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toweight")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtoWeight" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Number  &lt;br /&gt; 5">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtobase" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbase")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Qty   &lt;br /&gt; 6=4*5">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotalqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toqty")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFtotalqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Wastage (%)  &lt;br /&gt; 7">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvwastage" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wastage")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                                <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>--%>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Qty  &lt;br /&gt; 8=6+(7*6)">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgtotalqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFgtotalqty" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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





