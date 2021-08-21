<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="BgdMasterLand.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.BgdMasterLand" %>

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

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPrjName" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;" Text="Budget Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                    </div>
                                    <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Style="font-size: 11px;"></asp:Label>

                                        <asp:RadioButtonList ID="rblbudgt" runat="server" AutoPostBack="True" CssClass="rbtnList1 chkBoxControl" OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" RepeatColumns="6"
                                            RepeatDirection="Horizontal" Width="250px">
                                            <asp:ListItem>Master Budget</asp:ListItem>
                                            <asp:ListItem>Details Budget</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlAccountDesc" runat="server" Visible="False">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">


                            <div class="form-group">

                                <div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblacccode1" runat="server" CssClass="lblTxt lblName">Accounts Code</asp:Label>

                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                    <asp:LinkButton ID="ImageButton1" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImageButton1_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>
                                <div class="clearfix"></div>

                            </div>
                            <div>
                                <asp:GridView ID="gvAcc" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    PageSize="15" ShowFooter="True" Width="841px" OnRowDataBound="gvAcc_RowDataBound">
                                    <PagerSettings Visible="False" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Accounts">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccDesc" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" AutoPostBack="True"
                                                    Font-Bold="True" Font-Size="14px" OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                    Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level" ItemStyle-HorizontalAlign="Center">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click" Font-Bold="True"
                                                    Font-Size="12px" ForeColor="#000">Total :</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Height="22px" ReadOnly="true"
                                                    Width="103px" Font-Bold="True" Font-Size="12px" ForeColor="#000"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="103px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Height="22px" ReadOnly="true"
                                                    Width="103px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="103px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRmRk" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="ddl" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bgdrmrk")) %>'
                                                    Width="120px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click">Update Main</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>


                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <asp:Panel ID="Panel2" runat="server">
                                <div class="form-group">

                                    <div class="col-md-4 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Account Head</asp:Label>
                                        <asp:Label ID="lblAccHead" runat="server" CssClass="smLbl_to"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblTxt lblName">Page Size</asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>600</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                        </asp:DropDownList>


                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkHome_Click" CssClass="btn btn-primary primaryBtn">Home</asp:LinkButton>
                                       
                                         
                                    </div>
                                     <div class="col-xs-1 col-md-1 col-lg-1">
                                   <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/F_01_LPA/EntryLandRegProcess.aspx")%>' target="_blank" style=" line-height: 18px;">Procurement Status</a>

                                </div>
                                    <div class="clearfix"></div>

                                </div>
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName">Resource Code</asp:Label>

                                        <asp:TextBox ID="txtResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>


                                        <asp:LinkButton ID="ibtnDetailsCode" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnDetailsCode_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </asp:Panel>
                            <div class="row">
                                <asp:GridView ID="gvRes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    BackColor="#F0F0F0" PageSize="15" ShowFooter="True" Width="540px"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvRes_PageIndexChanging" OnRowDeleting="gvRes_RowDeleting" OnRowDataBound="gvRes_RowDataBound">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" />
                                        <asp:TemplateField HeaderText="Rescode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblrescode" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Description of Subsidiary Accounts">
                                            <FooterTemplate>
                                                <asp:CheckBox ID="chklkrate" runat="server" AutoPostBack="True" OnCheckedChanged="chklkrate_CheckedChanged"
                                                    Text="Lock" Font-Bold="True" Font-Italic="False" Font-Size="12px" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="12px" ForeColor="Black" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="250px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                       
                                       <%-- <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvStatus" runat="server" Target="_blank" Width="50px">STATUS</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Specification">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfDesc" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblresunit" runat="server" CssClass="GridLebel" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dhag No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtRmRk" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" BorderWidth="1px" CssClass="ddl" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bgdrmrk")) %>'
                                                    Width="120px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkSubmit" runat="server" OnClick="lnkSubmit_Click"
                                                    CssClass="btn btn-danger primaryBtn">Update Details</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    Style="text-align: right;" BorderWidth="1px"  CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtRate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    Style="text-align: right;" BorderWidth="1px" CssClass="GridTextbox" ReadOnly="true" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="gvlnkFTotal" runat="server" Font-Bold="True" ForeColor="#000"
                                                    OnClick="gvlnkFTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total:</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Dr. Amount" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    Style="text-align: right;" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" CssClass="GridTextbox" Font-Bold="true" Font-Size="12px" ReadOnly="true"
                                                    ForeColor="Black" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px" FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Cr. Amount" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    Style="text-align: right;" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    BorderStyle="None" CssClass="GridTextbox" Font-Bold="true" Font-Size="12px" ReadOnly="true"
                                                    ForeColor="Black" Width="80px"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Registration<br> cost" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtrgamt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    Style="text-align: right;" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rgamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>

                                             <FooterTemplate>
                                                <asp:TextBox ID="gvtxtrgtotal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                   Style="text-align: right;" CssClass="GridTextbox" Font-Bold="true" Font-Size="12px" ReadOnly="true"
                                                    ForeColor="Black" Width="70px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Other<br> cost" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvothamt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                    Style="text-align: right;" BorderWidth="1px" CssClass="GridTextbox" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>

                                             <FooterTemplate>
                                                <asp:Label ID="lblgvFother" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                   Style="text-align: right;" CssClass="GridTextbox" Font-Bold="true" Font-Size="12px" ReadOnly="true"
                                                    ForeColor="Black" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                             <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                    </Columns>

                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>



                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>




            <%--            <table style="width: 100%;">
                <tr>
                    <td class="style60">
                        <asp:Label ID="Label2" runat="server" CssClass="label2" Text="Budget Date :" Width="80px"
                            Font-Bold="True" Font-Size="12px" ForeColor="Black"></asp:Label>
                    </td>
                    <td class="style61">
                        <asp:TextBox ID="txtdate" runat="server" Style="border-style: solid; border-width: 1px"
                            Width="105px" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td></td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style60">&nbsp;
                    </td>
                    <td class="style61">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td></td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style60">&nbsp;
                    </td>
                    <td class="style61">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccountDesc" runat="server" Font-Size="12px" Visible="False"
                            Width="250px">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
