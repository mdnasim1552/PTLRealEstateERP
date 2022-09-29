<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptUserLogStatus.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.RptUserLogStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
            var gv = $('#<%=this.gvUserLog.ClientID %>');
           gv.Scrollable();
       }
    </script>


    
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
            <div class="card mt-4 pb-4">
                <div class="card-body">
                    <div class="row">

                               
                                    <div class="col-md-3">
                                        <asp:Label ID="Label15" runat="server" CssClass="form-label">From</asp:Label>


                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                       

                                    </div>   
                                    <div class="col-md-3">
                                         <asp:Label ID="lbltodate" runat="server" CssClass="lblTxt smLbl_to">To</asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate">
                                                </cc1:CalendarExtender>
                                    </div>

                    </div>
                    </div>
                </div>
            <div class="card" style="min-height:480px;">
                <div class="card-body">
                    <div class="row">
                        
                                    <div class="col-md-3 d-none">
                                        
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>


                                    <div class="col-md-3">
                                        <asp:Label ID="Label4" runat="server" CssClass="form-label">Name</asp:Label>
                                        <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control form-control-sm chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                      <div class="col-md-1 ml-3" style="margin-top:22px;">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>
                                
                                    <div class="col-md-1">
                                        <asp:Label ID="lblPage" runat="server" CssClass="form-label" Visible="false" >Page</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"                                               
                                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                >
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                    </div>
                    <div class="row mt-4" style="margin-left:10px;">
                        <asp:Panel ID="PanelVou" runat="server">
                   <asp:GridView ID="gvUserLog" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvLogType_PageIndexChanging">
                                    <RowStyle/>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryuser" runat="server" Font-Bold="true"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryDat" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eventdate")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntryTime" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eventtime")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                      
                                        <asp:TemplateField HeaderText="Entry Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntrydesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eventdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Details Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="entDetails" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eventdesc2")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Trm ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvTrmid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "logintrmid")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry Session">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEntSession" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sessionid")) %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                                </asp:GridView>
                </asp:Panel>
                    </div>
                </div>
                    
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>



