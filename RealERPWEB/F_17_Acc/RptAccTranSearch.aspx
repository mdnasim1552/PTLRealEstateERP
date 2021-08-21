<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptAccTranSearch.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptAccTranSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="Label12" runat="server" CssClass="btn btn-success primaryBtn" Text="Field Information:"></asp:Label>
                                       
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:CheckBox ID="chkallTransList" runat="server" AutoPostBack="True" CssClass="chkBoxControl"
                                                OnCheckedChanged="chkallTransList_CheckedChanged" Text="Check All" />
                                        </div>
                                        <div class="col-md-5 pading5px asitCol5">
                                            <asp:Label ID="Label17" runat="server" CssClass=" smLbl_to">From</asp:Label>
                                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                            <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">To</asp:Label>
                                            <asp:TextBox ID="txttodate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " ToolTip="(dd.mm.yyyy)"></asp:TextBox>

                                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:CheckBox ID="chkneNar" runat="server" TabIndex="10"
                                                Text="Without Narration &amp; Total" CssClass="chkBoxControl" />
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                <div class="form-group">
                                    <asp:CheckBoxList ID="cblTransList" runat="server" CssClass="rbtnList1 rptBankcheque margin5px" RepeatColumns="12"
                                        OnSelectedIndexChanged="cblTransList_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem>aa</asp:ListItem>
                                        <asp:ListItem>bb</asp:ListItem>
                                        <asp:ListItem>cc</asp:ListItem>
                                        <asp:ListItem>dd</asp:ListItem>
                                        <asp:ListItem>ee</asp:ListItem>
                                        <asp:ListItem>ff</asp:ListItem>
                                        <asp:ListItem>gg</asp:ListItem>
                                        <asp:ListItem>hh</asp:ListItem>
                                        <asp:ListItem>mm</asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                                        </div>
                                    </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class=" form-group">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblSearchlist" runat="server" CssClass="lblTxt lblName"
                                                Text="Search List"></asp:Label>

                                            <asp:DropDownList ID="ddlFieldList1" runat="server" CssClass=" ddlPage inputTxt" AutoPostBack="true" >
                                            </asp:DropDownList>

                                            <div class="clearfix"></div>
                                            <div class="form-group">

                                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName"
                                                    Text=""></asp:Label>

                                                <asp:DropDownList ID="ddlFieldList2" runat="server" AutoPostBack="true" CssClass=" ddlPage inputTxt">
                                                </asp:DropDownList>


                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName"
                                                    Text=""></asp:Label>
                                                <asp:DropDownList ID="ddlFieldList3" runat="server"  AutoPostBack="true" CssClass=" ddlPage inputTxt">
                                                </asp:DropDownList>


                                            </div>



                                        </div>

                                        <div class="col-md-1 pading5px">
                                            <div>
                                                <asp:DropDownList ID="ddlSrch1" runat="server" Width="90px" CssClass=" ddlistPull">
                                                    <asp:ListItem Value="like">Like</asp:ListItem>
                                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                    <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                </asp:DropDownList>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="form-group">

                                                <asp:DropDownList ID="ddlSrch2" runat="server" Width="90px" CssClass=" ddlistPull">
                                                    <asp:ListItem Value="like">Like</asp:ListItem>
                                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                    <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSrch3" runat="server" Width="90px" AutoPostBack="True"
                                                    CssClass=" ddlistPull">
                                                    <asp:ListItem Value="like">Like</asp:ListItem>
                                                    <asp:ListItem Value="=">Equal</asp:ListItem>
                                                    <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                    <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                    <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                    <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>


                                        <div class="col-md-2 pading5px asitCol2">
                                            <asp:Label ID="lbland1" runat="server" CssClass="lblTxt lblName" Text="And" Visible="False"
                                                Width="25px"></asp:Label>


                                            <asp:TextBox ID="txtSearch1" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                            <asp:DropDownList ID="ddlOperator1" runat="server" CssClass="ddlPage62 inputTxt ">
                                                <asp:ListItem Value="and">And</asp:ListItem>
                                                <asp:ListItem Value="or">Or</asp:ListItem>
                                            </asp:DropDownList>


                                            
                                           
                                            <div class="clearfix"></div>

                                            <asp:TextBox ID="txtSearch2" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                          

                                            <asp:Label ID="lbland2" runat="server" Text="And" Visible="False"
                                                CssClass="lblTxt lblName"></asp:Label>

                                          
                                            <asp:DropDownList ID="ddlOperator2" runat="server" CssClass="ddlPage62 inputTxt ">
                                                <asp:ListItem Value="and">And</asp:ListItem>
                                                <asp:ListItem Value="or">Or</asp:ListItem>
                                            </asp:DropDownList>



                                          

                                            <div class="clearfix"></div>

                                            <asp:TextBox ID="txtSearch3" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                          
                                        </div>

                                        <div class="col-md-5 pading5px">
                                            <asp:Panel ID="Panel5" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOrderList" runat="server" CssClass=" smLbl_to"
                                                                Text="Order Field:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrder1" runat="server" CssClass="ddlistPull" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                                CssClass="ddlPage62">
                                                                <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                <asp:ListItem Value="desc">Des</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>

                                                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSearch_Click">Ok</asp:LinkButton>

                                                        </td>
                                                        <td>
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                <ProgressTemplate>
                                                                    <asp:Label ID="Label3U" runat="server" CssClass="text-danger" Text="Please wait . . . . . . ."></asp:Label>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="ddlistPull" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                                CssClass="ddlPage62">
                                                                <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                <asp:ListItem Value="desc">Des</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td class="style115">
                                                            <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="ddlistPull" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style116">
                                                            <asp:DropDownList ID="ddlOrderad3" runat="server"
                                                                CssClass="ddlPage62">
                                                                <asp:ListItem Value="asc">Asc</asp:ListItem>
                                                                <asp:ListItem Value="desc">Des</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>


                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </div>


                                    </div>


                                </div>
                            </fieldset>

                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName"
                                            Text="Page size:" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" CssClass="ddlPage62" Visible="False">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                              
                            </div>

                    <asp:Panel ID="Panel10" runat="server">
                        <div class="table table-responsive">
                            <asp:GridView ID="gvAcTransSearch" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" Width="616px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                OnRowDataBound="gvAcTransSearch_RowDataBound" AllowPaging="True"
                                OnPageIndexChanging="gvAcTransSearch_PageIndexChanging">
                                <RowStyle />
                                <Columns>


                                    <asp:TemplateField HeaderText="Project Description">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HLgvVouNo" runat="server" Font-Size="11PX" Target="_blank" Font-Bold="false" Font-Underline="false" ForeColor="Black"
                                                Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>'
                                                Width="150px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Voucher Date">

                                        <ItemTemplate>
                                            <asp:Label ID="lgvVouDate" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAcode" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acrescode")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDesc" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acresdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Narration">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvVnar" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venarr")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Res Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvResamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resamt")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvDamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "debit")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Credit Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvCamt" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "credit")).ToString("#,##0;(#,##0); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chque No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpc3" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cheqrefno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Other ref.">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvotherref" runat="server" Style="text-align: Left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "otherref")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Left" />

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvVouNo02" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum2")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter"/>
<EditRowStyle />
<AlternatingRowStyle />
<PagerStyle CssClass="gvPagination" />
<HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            </div>
                        </asp:Panel>
                </div>
            </div>



            <table style="width: 100%;">
                <%--<tr>
                    <td>
                        <asp:Panel ID="Panel8" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style41">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14px"
                                            ForeColor="Yellow"
                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;"
                                            Text="Field Information:"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="Label22" runat="server" CssClass="style40" Font-Bold="True"
                                            Height="16px" Style="text-align: left" Text="From:" Width="50px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfromdate" runat="server" BorderStyle="None"
                                            Font-Bold="True" Height="16px" Width="87px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                    </td>
                                    <td class="style83">
                                        <asp:Label ID="Label23" runat="server" CssClass="style40" Font-Bold="True"
                                            Height="16px" Style="text-align: right" Text="To:" Width="40px"></asp:Label>
                                    </td>
                                    <td class="style83">
                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Font-Bold="True"
                                            Height="16px" Width="87px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>
                                    </td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83"></td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                    <td class="style83">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style83" colspan="65">
                                       
                                    </td>
                                    <td class="style43" valign="bottom">&nbsp;</td>
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
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <%--<asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                            BorderWidth="1px" Width="1074px">
                            <table style="width: 87%;">
                                <tr>
                                    <td class="style110">
                                        <asp:Label ID="lblSearchlist" runat="server" Font-Bold="True" Font-Size="14px"
                                            ForeColor="Yellow"
                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;"
                                            Text="Search List:" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlFieldList1" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">
                                        <asp:DropDownList ID="ddlSrch1" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="100px">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style112">
                                        <asp:TextBox ID="txtSearch1" runat="server" CssClass="txtboxformat"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style113">
                                        <asp:DropDownList ID="ddlOperator1" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="60px">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style46" rowspan="3">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style114">
                                                        <asp:Label ID="lblOrderList" runat="server" Font-Bold="True" Font-Size="14px"
                                                            ForeColor="Yellow"
                                                            Style="border-top: 1px solid yellow; border-bottom: 1px solid yellow;"
                                                            Text="Order Field:" Width="80px"></asp:Label>
                                                    </td>
                                                    <td class="style115">
                                                        <asp:DropDownList ID="ddlOrder1" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style116">
                                                        <asp:DropDownList ID="ddlOrderad1" runat="server"
                                                            Font-Bold="True" Font-Size="12px" Width="90px">
                                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style117">
                                                        <asp:LinkButton ID="lbtnSearch" runat="server" BackColor="#003366"
                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                            Font-Size="12px" Height="16px" OnClick="lbtnSearch_Click"
                                                            Style="text-align: center; color: #FFFFFF; width: 19px;">Ok</asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                            <ProgressTemplate>
                                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White"
                                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="Yellow" Style="text-align: left" Text="Please wait . . . . . . ."
                                                                    Width="120px"></asp:Label>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style114">&nbsp;</td>
                                                    <td class="style115">
                                                        <asp:DropDownList ID="ddlOrder2" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style116">
                                                        <asp:DropDownList ID="ddlOrderad2" runat="server"
                                                            Font-Bold="True" Font-Size="12px" Width="90px">
                                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style117">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style114">&nbsp;</td>
                                                    <td class="style115">
                                                        <asp:DropDownList ID="ddlOrder3" runat="server" Font-Bold="True"
                                                            Font-Size="12px" Width="150px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style116">
                                                        <asp:DropDownList ID="ddlOrderad3" runat="server"
                                                            Font-Bold="True" Font-Size="12px" Width="90px">
                                                            <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                            <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style117">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style110">&nbsp;</td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlFieldList2" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">
                                        <asp:DropDownList ID="ddlSrch2" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="100px">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style112">
                                        <asp:TextBox ID="txtSearch2" runat="server" CssClass="txtboxformat"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style113">
                                        <asp:DropDownList ID="ddlOperator2" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="60px">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style110">&nbsp;</td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlFieldList3" runat="server"
                                            Font-Bold="True" Font-Size="12px" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">
                                        <asp:DropDownList ID="ddlSrch3" runat="server" Font-Bold="True"
                                            Font-Size="12px" Width="100px">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then  Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style112">
                                        <asp:TextBox ID="txtSearch3" runat="server" CssClass="txtboxformat"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="style113">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style110">
                                        <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px"
                                            Style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False"
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td class="style109">
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                            Width="120px">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style111">&nbsp;</td>
                                    <td class="style112">&nbsp;</td>
                                    <td class="style113">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>--%>
                    </td>
                </tr>

            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


