<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccFincStatmnt.aspx.cs" Inherits="RealERPWEB.F_17_Acc.AccFincStatmnt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <script type="text/javascript">
            function addplug(plug) {
               // alert(plug);

                $('#<%=this.txtflag.ClientID %>').val(plug);
                     
                switch (plug) {
                    case "IS":
                    case "CSHFLW":
                    case "CSHFLW2":
                        $("#OpDate").show();
                        break;
                    default:
                        $("#OpDate").hide();
                        break;
                }
                
            }

            function activetab() {
                             
                var plug = $('#<%=this.txtflag.ClientID %>').val();
               // alert(plug);
                switch (plug) {
                    case "IS":
                        $('.nav-tabs a[href="#tab1primary"]').tab('show');                      
                        break;
                    case "BS":
                        $("#OpDate").hide();
                        $('.nav-tabs a[href="#tab0primary"]').tab('show');
                        break;
                    case "SHEQUITY":
                        $("#OpDate").hide();
                        $('.nav-tabs a[href="#tab2primary"]').tab('show');
                        break;
                    case "CSHFLW":
                        $("#OpDate").show();
                        $('.nav-tabs a[href="#tab3primary"]').tab('show');
                        break;
                    case "CSHFLW2":
                        $("#OpDate").show();
                        $('.nav-tabs a[href="#tab4primary"]').tab('show');
                        break;

                    case "Prjwiseres":
                        $("#OpDate").show();
                        $('.nav-tabs a[href="#tab5primary"]').tab('show');
                        break;
                }
           
                
                
            }
            $(document).ready(function () {
                $("#OpDate").hide();
                   $('#<%=this.txtflag.ClientID %>').val("BS");
            });
        </script>
    <style>
           .panel.with-nav-tabs .panel-heading {
            padding: 5px 5px 0 5px;
        }

        .panel.with-nav-tabs .nav-tabs {
            border-bottom: none;
        }

        .panel.with-nav-tabs .nav-justified {
            margin-bottom: -1px;
        }
        /*** PANEL DEFAULT ***/
.with-nav-tabs.panel-default .nav-tabs > li > a,
.with-nav-tabs.panel-default .nav-tabs > li > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li > a:focus {
    color: #777;
}
.with-nav-tabs.panel-default .nav-tabs > .open > a,
.with-nav-tabs.panel-default .nav-tabs > .open > a:hover,
.with-nav-tabs.panel-default .nav-tabs > .open > a:focus,
.with-nav-tabs.panel-default .nav-tabs > li > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li > a:focus {
    color: #777;
	background-color: #ddd;
	border-color: transparent;
}
.with-nav-tabs.panel-default .nav-tabs > li.active > a,
.with-nav-tabs.panel-default .nav-tabs > li.active > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li.active > a:focus {
	color: #555;
	background-color: #fff;
	border-color: #ddd;
	border-bottom-color: transparent;
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu {
    background-color: #f5f5f5;
    border-color: #ddd;
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > li > a {
    color: #777;   
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
    background-color: #ddd;
}
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > .active > a,
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
.with-nav-tabs.panel-default .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
    color: #fff;
    background-color: #555;
}
    </style>
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
             <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <%--  <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                                    Text="Please Wait.........."></asp:Label>--%>

                        <%--  <div class="loader"></div> --%>
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
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName" Text="From"></asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDatefrom" Enabled="true">
                                        </cc1:CalendarExtender>
                                        <asp:Label ID="lbldateto" runat="server" CssClass="smLbl_to" Text="To"></asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtDateto" Enabled="true">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblrptlbl" runat="server" CssClass="smLbl_to" Text="Report Level"></asp:Label>
                                        <asp:DropDownList ID="DDListLevels" runat="server" CssClass="smDropDown inputTxt"
                                            TabIndex="6">
                                            <asp:ListItem Value="1">Level-1</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="2">Level-2</asp:ListItem>
                                            <asp:ListItem Value="3">Level-3</asp:ListItem>
                                            <asp:ListItem  Value="4">Level-4</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                 <div class="col-md-2 pading5px asitCol2">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        <asp:CheckBox ID="ChkTopHead" runat="server" Text="Print top heads" CssClass="btn btn-primary checkBox" />
                                     <asp:Label ID="Label1" runat="server"></asp:Label> 
                                     <asp:TextBox ID="txtflag" style="display:none;" runat="server"></asp:TextBox> 
                                    </div>
                                    <div class="col-md-3 pading5px pull-right">
                                        
                                    </div>
                                </div>
                                <div class="form-group" id="OpDate">
                                 
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:Label ID="lblOpeningDate" runat="server" CssClass="lblTxt lblName" Text="Opening Date"
                                            ></asp:Label>
                                        <asp:TextBox ID="txtOpeningDate" runat="server" AutoCompleteType="Disabled" CssClass="inputtextbox"
                                            ></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtOpeningDate" Enabled="true">
                                        </cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="row">

                       <div class="col-md-12">
                            <div class="panel with-nav-tabs panel-default">
                            <div class="panel-heading">
                                <ul class="nav nav-tabs" id="tabsbar" runat="server">
                                    <li class="active"><a href="#tab0primary" class="tablink" onclick="addplug('BS');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-usd"></span> FInancial Position</a></li>
                                    <li id="is"><a href="#tab1primary" class="tablink" onclick="addplug('IS');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-stats"></span> Comprehensive Income</a></li>
<%--                                    <li><a href="#tab2primary" class="tablink" onclick="addplug('SHEQUITY')" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-bitcoin"></span> Share Holder's Equity</a></li>--%>
                                   <%-- <li><a href="#tab3primary" class="tablink" onclick="addplug('CSHFLW')" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-euro"></span> Cash of Flow (Direct) </a></li>--%>
                                    <li><a href="#tab3primary" class="tablink" onclick="addplug('CSHFLW');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-list"></span>  Cash of Flow (Direct) </a></li>
                                    <li><a href="#tab5primary" class="tablink" onclick="addplug('Prjwiseres');" data-toggle="tab" style="font-size: 16px"><span class="glyphicon glyphicon-list"></span> Project Cost </a></li>

                                </ul>
                            </div>
                            <div class="panel-body">
                                <div class="tab-content">
                                    <div class="tab-pane fade in active" id="tab0primary">
                                                    <asp:GridView ID="dgvBS" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="dgvBS_RowDataBound" Width="640px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of accounts">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label3122" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnDetailsbs" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="90px">Next</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                             <ItemTemplate>
                                                <asp:HyperLink ID="HLgvBSDesc" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Changes During the Period"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
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
                                    <div class="tab-pane fade" id="tab1primary">
                                  <asp:GridView ID="dgvIS" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="dgvIS_RowDataBound" Width="647px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode6" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            HeaderText="Description Of accounts">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnDetails" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Target="_blank" Width="90px">Next</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvISDesc" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Current Period"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Previous Period"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel"
                                                    Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current %" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCPar" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percentcu")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total %" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPar" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpercent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
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
                                    <div class="tab-pane fade " id="tab2primary">
                                      <asp:GridView ID="gvsequ" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                Width="519px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Description">
                                        <FooterTemplate>
                                            <asp:Label ID="lblftotalse" runat="server" Text="Total Equity" Font-Bold="True" Font-Size="12px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lgcActDescse" runat="server" Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))
                                                                        
                                                                         
                                                                    %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvopnamse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFopnamse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Increased">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcramtse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFcramtse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Decreased">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdramtse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFdramtse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvclosamse" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFclosamse" runat="server" Font-Bold="True" Font-Size="12px"
                                                Style="text-align: right" Width="75px"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                 <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView> 

                                    </div>
                                    <div class="tab-pane fade " id="tab3primary">
                               <asp:GridView ID="grvCashFlow" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            OnRowDataBound="grvCashFlow_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="326px" >
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center"  Font-Bold="true"/>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvactcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                

                                    <asp:TemplateField HeaderText="Description">
                                               
                                                
                                                    <HeaderTemplate>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblheader" runat="server" Text="Description"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtntbCdataExelsp" runat="server"   CssClass=" btn btn-danger btn-xs"   Font-Bold="true"  BackColor="#000066" BorderColor="White" BorderWidth="1px" BorderStyle="None" Text="Export to Excel"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                
                                                
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnactDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                     
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="350px" Font-Underline="False" Style="color: Black"
                                            OnClick="lbtnactDesc_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>








                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Change During The Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcuamtcf" runat="server" Target="_blank" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink></ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnamcf" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclosamcf" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="90px"></asp:Label></ItemTemplate>
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
                                      <div class="tab-pane fade " id="tab4primary">
                                      <asp:GridView ID="grvCashFlow02" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvCashFlow02_RowDataBound"
                            ShowFooter="True" Width="520px" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle Font-Size="11px" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Accountcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvactcode0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesccf02" runat="server" Font-Underline="False" Style="color: Black"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="280px">
                                   
                                                                    


                                     
                                                                    
                                                                    
                                                                    
                                                                    
                                        </asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                    HeaderText="Change During The Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvcuamtcf02" runat="server" Font-Size="10px" Target="_blank"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:HyperLink></ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Period" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopnamcf02" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvclosamcf02" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label></ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>

                                    </div>
                                     <div class="tab-pane fade " id="tab5primary">
                                    <div class="row  table-responsive">


                                <asp:GridView ID="gvprowres" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="gvprowres_RowDataBound" OnRowCreated="gvprowres_RowCreated">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnopwres" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgrppwres" runat="server" Font-Size="11PX" Font-Bold="false"
                                                    Font-Underline="false" ForeColor="Black" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catmdesc")).Trim()%>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Description">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescpwres" runat="server" Font-Size="11PX" Target="_blank" Font-Bold="false"
                                                    Font-Underline="false" ForeColor="Black" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim()%>'
                                                    Width="130px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Opening">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFiopnam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvviopnam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iopnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Addition During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFiaddam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgviaddam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iaddam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Adjustment During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFiadjam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvviadjam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iadjam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFiclsam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvviclsam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iclsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Opening">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFlopnam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvlopnam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lopnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Addition During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFladdam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladdam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "laddam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Adjustment During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFladjam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvladjam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "iadjam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFlclsam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvlclsam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lclsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Opening">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmopnam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvmopnam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mopnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Addition During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmaddam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmaddam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maddam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Adjustment During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmadjam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgviadjam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "madjam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFmclsam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmclsam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mclsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Opening">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsopnam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsopnam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sopnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Addition During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsaddam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsaddam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saddam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Adjustment During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsadjam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsadjam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sadjam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Closing">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFsclsam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsclsam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sclsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Opening">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtopnam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtopnam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "topnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Addition During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtaddam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtaddam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taddam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Adjustment During the Period">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtadjam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvtadjam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tadjam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Closing">
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtclsam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="80px">
                                                </asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvtclsam" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tclsam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
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
                                         </div>

                            

                                </div>
                            </div>
                        </div>
                       </div>
                               
                            </div>
                      
                    
               
                 
                    </div>

                </div>
         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



