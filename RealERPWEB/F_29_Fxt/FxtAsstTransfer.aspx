<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="FxtAsstTransfer.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.FxtAsstTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
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
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row mb-0">
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <asp:Label ID="Label7" runat="server" CssClass="lblName lblTxt" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                Format="dd.MM.yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1.5 col-sm-1.5 col-lg-1.5">
                            <div class="form-group">
                                <asp:Label ID="Label12" runat="server" CssClass=" lblName lblTxt" Text="Trans No:"></asp:Label>
                                <asp:Label ID="lblCurTransNo1" runat="server" CssClass=" inputtextbox"></asp:Label>
                                <div class="form-group">                                
                                    <asp:TextBox ID="txtCurTransNo2" runat="server" CssClass="form-control form-control-sm">00001</asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblProjectFromList0" runat="server" CssClass=" lblName lblTxt" Text="From Project List"></asp:Label>
                                <asp:TextBox ID="txtProjectSearchF" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindProject" runat="server" OnClick="ImgbtnFindProject_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>                                 
                                <asp:DropDownList ID="ddlprjlistfrom" runat="server"
                                    Width="280px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlprjlistfrom_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select ">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" CssClass=" lblName lblTxt" Text="Project List"></asp:Label>
                                <asp:TextBox ID="txtProjectSearchT" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindProject0" runat="server" OnClick="ImgbtnFindProject0_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlprjlistto" runat="server"
                                    CssClass=" form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divPrevTrans" runat="server" visible="true">
                            <asp:Label ID="lblPreList" runat="server" CssClass=" lblName lblTxt" Text="Prev. Trans List"></asp:Label>
                            <asp:TextBox ID="txtPreTrnsSearch" runat="server" Visible="False" CssClass="inputtextbox"></asp:TextBox>
                            <asp:LinkButton ID="lbtnPrevVOUList" runat="server" OnClick="lbtnPrevVOUList_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevISSList" runat="server"
                                CssClass="chzn-select form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row mb-2 mt-0" id="divresource" runat="server"  Visible="False">
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <asp:Label ID="lblResList" runat="server" CssClass=" lblName lblTxt" Text="Resource List"></asp:Label>
                            <asp:DropDownList ID="ddlreslist" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" style="margin-top: 20px;">
                             <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkselect_Click">Select</asp:LinkButton>
                        </div>                       
                    </div>
                </div>
            </div>

            <div class="card card-fluid mb-1">
                <div class="card-body" style="min-height:500px;">
                    <div class="row ">
                        <asp:Panel ID="pnlgrd" runat="server" Visible="False">                      
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-bordered grvContentarea"
                                ShowFooter="True" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid0" runat="server" Style="text-align: center"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrescode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Resource Description" >
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" Width="320px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvunit" runat="server"
                                                Style="font-size: 12px; text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="left" Width="40px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Bal. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalqty" runat="server"
                                                Style="font-size: 12px; text-align: right;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" Width="80px"/>
                                        <ItemStyle HorizontalAlign="right" Width="60px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtqty" runat="server" BackColor="Transparent" BorderStyle="Solid"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" BorderColor="#660033" BorderWidth="1px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkupdate" runat="server"  OnClick="lnkupdate_Click"  CssClass="btn btn-danger btn-sm primaryBtn" Visible="false">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" Width="100px"/>
                                        <ItemStyle Width="80px" HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="right"/>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooterNew" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPaginationNew" />
                                <HeaderStyle CssClass="grvHeaderNew" />

                            </asp:GridView>
                    </asp:Panel>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


