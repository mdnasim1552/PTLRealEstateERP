<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptYearlyCollectionStatus.aspx.cs" Inherits="RealERPWEB.F_45_GrAcc.RptYearlyCollectionStatus" %>

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

                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtfrmdate" runat="server" CssClass="inputtextbox "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate" Enabled="true"></cc1:CalendarExtender>


                                        <asp:Label ID="lblDateTo" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" Enabled="true"></cc1:CalendarExtender>
                                        <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary primaryBtn">Show</asp:LinkButton>
                                    </div>

                                    <div class="col-md-5 pading5px asitCol3">
                                        
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName txtAlgLeft" Text="Page Size" ></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" smDropDown"
                                            BackColor="#CCFFCC" Font-Bold="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" 
                                            Width="70px">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                            <asp:ListItem Value="400">400</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-5 pading5px asitCol5">

                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="18px"
                                                    ForeColor="Yellow" Style="text-align: center" Text="Please wait . . . . . . ."
                                                    Width="218px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <div class="col-md-2 pull-right">
                                            <asp:Label ID="lblmsg01" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-4 pading5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Company Name"></asp:Label>
                                            <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                CssClass="chkBoxControl btn btn-primary primaryBtn margin5px"
                                                OnCheckedChanged="chkall_CheckedChanged" Text="Check All" />


                                        </div>


                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName"></asp:Label>

                                        <asp:CheckBoxList ID="cblCompany" runat="server" CellPadding="2" CellSpacing="0"
                                            CssClass=" rbtnList1 chkBoxControl" Font-Bold="True" Font-Size="14px"
                                            ForeColor="#000" Height="12px" RepeatColumns="6" Width="1000px"
                                            RepeatDirection="Horizontal">
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

                                </asp:Panel>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                        <asp:GridView ID="gvyCollection" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False"
                            OnPageIndexChanging="gvyCollection_PageIndexChanging" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="gvyCollection_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lgactdescyc" runat="server"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                            Width="150px"> 
                                            
                                            
                                            
                                            
                                            
                                            
                                        </asp:Label>




                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Value">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtobgdcost" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdcost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sold Value">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtosoldvalue" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Received">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtotreceivedyc" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Dues">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvpredueayc" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdueam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues1">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam1" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam1")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues2">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam2" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam2")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues3">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam3" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam3")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues4">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam4" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam4")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues5">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam5" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam5")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues6">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam6" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam6")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues7">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam7" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam7")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues8">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam8" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam8")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues9">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam9" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam9")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues10">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam10" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam10")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues11">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam11" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam11")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues12">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvdueam12" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam12")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Dues">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvtdueam" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todueam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grand Total">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvgtdueam" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtodueam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
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
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

