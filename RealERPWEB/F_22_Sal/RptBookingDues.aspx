<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptBookingDues.aspx.cs" Inherits="RealERPWEB.F_22_Sal.RptBookingDues" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                 
                                <div class="form-group">
                                    <div class="col-md-3 asitCol3 pading5px">

                                        <asp:Label ID="lblProjectname" runat="server" CssClass="lblTxt lblName"
                                            Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcProject" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" CssClass="ddlPage"
                                            Width="250px">
                                        </asp:DropDownList>
                                        

                                        <asp:LinkButton ID="lbtnOk" runat="server" BorderStyle="Solid" CssClass="btn btn-primary primaryBtn"
                                            OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>

                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                            DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" Style="text-align: center"
                                                    Text="Please wait . . . . . . ." Width="218px"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-3  pading5px">

                                        <asp:Label ID="Label7" runat="server" CssClass="lblTxt lblName"
                                            Text="Date:"></asp:Label>

                                        <asp:TextBox ID="txtfrmdate" runat="server" AutoCompleteType="Disabled"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                                        <asp:Label ID="Label8" runat="server" CssClass=" smLbl_to"
                                            Text="To:"></asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" AutoCompleteType="Disabled" BorderStyle="None"
                                            CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                       
                                    </div>
                                    
                                    <div class="col-md-3 asitCol3 pading5px">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt smLbl_to " 
                                            Text="Size :"></asp:Label>

                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                            Width="113px">
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
                                <div class="form-group">
                                    <div class="col-md-4  pading5px">

                                        <asp:Label ID="lblAmount0" runat="server" Font-Bold="True" CssClass="lblTxt lblName" Text="Amount:"></asp:Label>

                                        <asp:DropDownList ID="ddlSrchCash" runat="server" AutoPostBack="True" Font-Bold="True"
                                            CssClass="ddlPage" OnSelectedIndexChanged="ddlSrchCash_SelectedIndexChanged" Width="200px">
                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then&nbsp; Equal</asp:ListItem>
                                            <asp:ListItem Value="between">Between</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:TextBox ID="txtAmountC1" runat="server" BorderStyle="None" CssClass="inputtextbox"></asp:TextBox>

                                        <asp:Label ID="lblToCash" runat="server" Font-Bold="True" CssClass="lblTxt lblName" Text="To:" Visible="False"></asp:Label>

                                        <asp:TextBox ID="txtAmountC2" runat="server" BorderStyle="None" CssClass="inputtextbox" Visible="False"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                                <asp:GridView ID="dgvAccRec" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="dgvAccRec_PageIndexChanging" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pr.Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAccCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cutomer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgacuname" runat="server" Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))+"</b>"+"<br>"+
                                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgudesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgudesc0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "salsdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person">
                                            <FooterTemplate>
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Black" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgCper" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cteam")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvAcAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "uamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvRecAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvBalAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTIns" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toinstall")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To be Paid Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTBPaid" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPaid" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "paidins")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues Inst.">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueins")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="35px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFDueAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcurinsAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcurinsamt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "procolam")).ToString("#,##0;(#,##0); ") %>'
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
                            </asp:View>
                            <asp:View ID="ViewRecList02" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="dgvAccRec02" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        OnPageIndexChanging="dgvAccRec02_PageIndexChanging" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgactdesc02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cutomer Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgacuname02" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname"))
                                                                       %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgudesc01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flat Size">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFunitsize" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitsize" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFavgrate" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apartment Price">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFaptcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvaptcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Car Parking">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFcpcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcpcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Utility ">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFutilitycost" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvutilitycost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utltycost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Others">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFothcost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvothescost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Value">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtocost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtocsot" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Encash">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgFEncash" runat="server" ForeColor="Black"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEncash" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Returned">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtretamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtretamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Today's">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtframt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtframt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Post Dated">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtpdamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtpdamt" runat="server" Font-Size="11PX" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Received">
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="hlnkgvFtoreceived" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Target="_blank" Style="text-align: right" Width="100px"></asp:HyperLink>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtotreceived" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Previous Booking">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpbooking" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtodues0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Current Booking ">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCbooking" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCbooking" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Total Due">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtbdues" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtbdues" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tbdues")).ToString("#,##0;(#,##0); ") %>'
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

                                <div class="row">
                                    <asp:Label runat="server"
                                        CssClass="lblTxt lblName">Note</asp:Label>
                                </div>



                               
                                    <asp:GridView ID="gvinpro" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid0" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Decription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesinpro" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtounit" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unumber")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                    <itemstyle horizontalalign="Right" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtounsize" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtourate" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtouamt" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Precentate">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoupercent" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                
                            </asp:View>
                            <asp:View ID="ViewallProDues" runat="server">
                               
                                    <asp:GridView ID="dgvAccRec03" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        OnPageIndexChanging="dgvAccRec03_PageIndexChanging" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HLgvDesc" runat="server" Font-Size="11PX" Font-Underline="false"
                                                        ForeColor="Black" Target="_blank" Style="text-align: left" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "pactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+ 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim(): "")  %>'
                                                        Width="250px"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flat Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvunitsizeal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Avg. Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvrateal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptrate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apartment Price">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFaptcostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvaptcostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aptcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Car Parking">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFcpcostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvcpcostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Utility ">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFutilitycostal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvutilitycostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utltycost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Others">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFothcostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvothescostal" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Value">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtocostal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtocsotal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Encash">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgFEncashal" runat="server" ForeColor="Black"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvEncashal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        Font-Size="11px" Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reconamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" Font-Size="11px" Font-Bold="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Returned">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtretamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtretamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retcheque")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Today's">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtframtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtframtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcheque")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Post Dated">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtpdamtal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtpdamtal" runat="server" Font-Size="11PX" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pcheque")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Received">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtoreceivedal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtotreceivedal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Dues">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFatoduesal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtatoduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dues Upto Dec">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtotalduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtotalduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dues Balance">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtoduesal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Previous Booking">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpbookingal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpbduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbookam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Previous Installment">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvpinsduesall" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pinsam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amt">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFpretoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoduesal0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Booking ">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCbookingal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCbookingal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbookam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Installment ">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFCinstallmental" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCinstallmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cinsam")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total ">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFtoCInstalmental" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvCoCInstalmental" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ctodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Due">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFvtoduesal" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                        Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvvtoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vtodues")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delay Charge">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFdelchargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdelchargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cdelay")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return Cheque">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFdischargeal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvdischargeal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "discharge")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Total Dues">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFnettoduesal" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="Black" Style="text-align: right"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvnettoduesal" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ntodues")).ToString("#,##0;(#,##0); ") %>'
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
                               
                                <div class="row">
                                    <asp:Label runat="server" BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid"
                                        BorderWidth="1px" Font-Bold="True" Font-Size="12px" ForeColor="Yellow" Width="300px">Note</asp:Label>
                                </div>

                                
                                    <asp:GridView ID="gvinproal" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid1" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                        Width="10px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Decription">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesinproal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtounital" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unumber")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                    <itemstyle horizontalalign="Right" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Unit Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtounsizeal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "usize")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtourateal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtouamtal" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Precentate">
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvtoupercental" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" Font-Size="12px" ForeColor="Black" TabIndex="76" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                    </asp:GridView>
                                


                            </asp:View>
                            <asp:View ID="ViewYearLyCollection" runat="server">
                                <asp:GridView ID="gvyCollection" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvyCollection_PageIndexChanging" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name">
                                            <FooterTemplate>
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgactdescyc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoBgdCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtobgdcost" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdcost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sold Value">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtsoldcalue" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtosoldvalue" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocost")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Received">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtoreceivedyc" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="Black" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtotreceivedyc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFpduesyc" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpredueayc" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pdueam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues1">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam1")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues2">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam2")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues3">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam3")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues4">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam4" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam4")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues5">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam5" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam5")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues6">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam6" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam6")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues7">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam7" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam7")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues8">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam8" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam8")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues9">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam9" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam9")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues10">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam10" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam10")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues11">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam11" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam11")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dues12">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFdueam12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdueam12" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam12")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Dues">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtdueam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtdueam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "todueam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grand Total">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFgtdueam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                    Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgtdueam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gtodueam")).ToString("#,##0;(#,##0); ") %>'
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
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


