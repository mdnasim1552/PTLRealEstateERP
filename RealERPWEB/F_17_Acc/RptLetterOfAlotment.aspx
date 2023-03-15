<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptLetterOfAlotment.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptLetterOfAlotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .chzn-select {
            width: 100%;
        }

        .chzn-container {
            width: 100% !important;
        }

        .gview tr td {
            border: 0;
        }

        .gview .form-control {
            height: 25px;
            line-height: 25px;
            padding: 0 12px;
            border-style: solid !important;
            border-color: #c6c9d5 !important;
        }

        #cardstyle {
            background-color: #E8E3E3;
            padding: 0 !important;
        }

        .mt20 {
            margin-top: 20px;
        }
        .chzn-container-single .chzn-single {
            height: 29px !important;
            line-height: 28px !important;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            //<a href="RptSalInterest.aspx">RptSalInterest.aspx</a>
            $('.chzn-select').chosen({ search_contains: true });


        });

        function pageLoaded() {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });

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

            <div class="card mt-3 mb-1">
                <div class="card-body p-1">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblprjname">Project Name</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlprjname" AutoPostBack="True" OnSelectedIndexChanged="ddlprjname_SelectedIndexChanged" CssClass="form-control chzn-select form-control-sm "></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label1">Customer Name</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlcustomerName" AutoPostBack="True" CssClass="form-control chzn-select form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="btnok" OnClick="btnok_Click" runat="server" CssClass="btn btn-primary btn-sm mt20">OK</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid mt-0" style="min-height: 550px;">
                <div class="card-body">
                       <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="View1" runat="server">
                    
                                </asp:View>
                           <asp:View ID="View2" runat="server">
                               <div class="row" id="divdate" runat="server" visible="false" >
                        <div class="col-sm-1 col-md-1  col-lg-1">                           
                            <asp:Label ID="lblFdate" runat="server">Date</asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" autocomplete="off" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="csefdate" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>                            
                        </div>
                        <div class="col-sm-1 col-md-1  col-lg-1"> 
                         
                            <asp:Label ID="lblrefno" runat="server">Ref No</asp:Label>
                           
                            <asp:TextBox ID="txtrefno" runat="server" autocomplete="off"  Enabled="false" CssClass="form-control form-control-sm"></asp:TextBox>                    
                        </div>
                        <div class="col-sm-2 col-md-2  col-lg-2">                           
                            <asp:Label ID="lblrefdesc" runat="server">Ref Desc</asp:Label>
                            <asp:TextBox ID="txtrefdesc" runat="server" autocomplete="off" Enabled="false" CssClass="form-control form-control-sm"></asp:TextBox>                    
                        </div>
                    </div>
                    <div class="row mt-2">
                        <asp:GridView ID="gvcustsettlement" runat="server" AllowPaging="false" CssClass="table-condensed table-bordered grvContentarea" AutoGenerateColumns="False"
                            ShowFooter="True" BorderStyle="None" Width="600px">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcostdesc" runat="server" Style="font-size: 12px" 
                                            CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "code")).Substring(2,3)=="AAA" ? "font-weight-bold":"" %>'
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblamt" runat="server" Style="font-size: 12px"
                                            CssClass='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "code")).Substring(2,3)=="AAA" ? "font-weight-bold":"" %>'
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="Right"/>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooterNew" />
                            <EditRowStyle />
                            <AlternatingRowStyle />                            
                            <HeaderStyle CssClass="grvHeaderNew" />
                            <RowStyle CssClass="grvRowsNew" />
                        </asp:GridView>
                    </div>
                               </asp:View>
                                 </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
