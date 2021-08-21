<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptGrpAccDailyTransaction.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.RptGrpAccDailyTransaction" %>

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
                                        <div class="col-md-4 pading5px asitCol4">
                                            <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputtextbox "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtfromdate" Enabled="true"></cc1:CalendarExtender>


                                            <asp:Label ID="lblDateTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>

                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:LinkButton ID="lbtnShow" runat="server" OnClick="lbtnShow_Click" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>


                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <asp:Label ID="Label3" runat="server" CssClass="lblProgressBar" Text="Please wait . . . . . . ."
                                                               Width="218px"></asp:Label>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>

                                    </div>




                                </asp:Panel>
                            </div>
                    </div>
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewDailyTransaction" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <asp:Label ID="lblTransactionTitle" runat="server" CssClass="lblTxt lblName text-danger"
                                            Text="Transaction Listing:-" Visible="False"></asp:Label>
                                        <div class="clearfix"></div>
                                        <div class="form-group">

                                            <asp:Label ID="lblPage" runat="server" Text="Page Size :" Visible="False" CssClass="lblTxt lblName"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage"
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                <asp:ListItem>15</asp:ListItem>
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



                                            <div class="col-md-4 pading5px asitCol4">
                                                <asp:Label ID="lblVoucher" runat="server" CssClass="lblTxt lblName" Text="Vouchar Type"
                                                    Visible="False"></asp:Label>
                                                <asp:DropDownList ID="ddlVouchar" runat="server" CssClass="ddlPage margin5px" Visible="False">
                                                    <asp:ListItem>BC</asp:ListItem>
                                                    <asp:ListItem>BD</asp:ListItem>
                                                    <asp:ListItem>CC</asp:ListItem>
                                                    <asp:ListItem>CD</asp:ListItem>
                                                    <asp:ListItem>CT</asp:ListItem>
                                                    <asp:ListItem>JV</asp:ListItem>
                                                    <asp:ListItem Selected="True">ALL Voucher</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:LinkButton ID="imgbtnSearchVoucher" runat="server" CssClass="btn btn-primary srearchBtn" Visible="false" OnClick="imgbtnSearchVoucher_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>



                                        </div>
                                    </div>
                                </fieldset>
                            </div>

                            <div class="row table-responsive">
                                <asp:GridView ID="gvtranlsit" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvtranlsit_PageIndexChanging"
                                    OnRowDataBound="gvtranlsit_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    PageSize="15" ShowFooter="True" Width="931px">
                                    <PagerSettings Position="TopAndBottom" />
                                    <PagerStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompany" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsnam")) %>'
                                                    Width="120px" Font-Bold="true" Font-Size="16px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDate1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher #" FooterText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvnum1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAcRsCode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAcRsDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Res. Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvInAmt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inneram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDram" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDram" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvCram" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="txtgvFCram" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque/Ref #">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRefnum" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpostusername" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbydesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
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
                        <asp:View ID="ViewBgsVsAchievement" runat="server">

                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-4 pading5px asitCol5">
                                                <asp:Label ID="lblVoucher0" runat="server" CssClass="lblTxt lblName"
                                                    Text="Company Name"></asp:Label>
                                                <div class="col-md-5 pading5px">
                                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control inputTxt">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:Label ID="lblCompanyDesc" runat="server" CssClass="smLbl_to" Visible="False"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="row table-responsive">
                                <asp:GridView ID="gvbgdvse" runat="server" AllowPaging="false"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvbgdvse_RowDataBound" ShowFooter="True" Width="501px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo8" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAcDesc" runat="server"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="300px">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budget">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbgdamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvacamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Difference">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvdiffamt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="right" />
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


                        <asp:View ID="ViewProjectTransaction" runat="server">
                            <asp:Panel ID="Panel4" runat="server">

                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblName lblTxt"
                                                    Text="Get Acc. Heads:"></asp:Label>
                                                <asp:TextBox ID="txtAccSearch" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                                <asp:LinkButton ID="IbtnSearchAcc" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="IbtnSearchAcc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>

                            </asp:Panel>

                            <asp:GridView ID="gvPtotranlsit" runat="server" AutoGenerateColumns="False"
                                PageSize="15" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                Width="931px">
                                <PagerSettings Position="TopAndBottom" />
                                <PagerStyle ForeColor="#000" HorizontalAlign="Left" VerticalAlign="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNop" runat="server" Font-Bold="True"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDatep" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                Width="65px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvnump" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <HeaderTemplate>
                                            <table style="width: 47%;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label30" runat="server" Font-Bold="True" Text="Description"
                                                            Width="180px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtnbtbCdataExelp" runat="server" BackColor="#000066"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            ForeColor="#000" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAcRsDescp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))%>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Debit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDramp" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFDramp" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvCramp" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFCramp" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#000" Style="text-align: right" Width="100px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRefnump" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Other Ref #">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOthRefnump" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvParynamep" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

