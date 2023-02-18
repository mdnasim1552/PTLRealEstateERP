<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="FxtAssetRegister.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.FxtAssetRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            $('.chzn-select').chosen({ search_contains: true });


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            var gvFixAsset = $('#<%=this.gvFixAsset.ClientID%>');
            gvFixAsset.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

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
                    <div class="row mb-2">
                        <div class="col-md-2.5 col-sm-2.5 col-lg-2.5">
                            <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Asset Type</asp:Label>
                            <asp:DropDownList ID="ddlProjectName" runat="server" Width="270" CssClass="chzn-select form-control form-control-sm" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:Label ID="lblDeptDesc" runat="server" CssClass="dataLblview" Visible="False" Width="250px"></asp:Label>
                        </div>    
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" TabIndex="4" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>  
                        <div class="col-md-2 col-sm-2 col-lg-2">                            
                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Resource "></asp:Label>                            
                            <asp:TextBox ID="txtSrchMat" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>                                  
                        </div>  
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary btn-sm srearchBtn" runat="server" TabIndex="2" OnClick="ibtnFindProject_Click"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row mb-2">
                        <asp:GridView ID="gvFixAsset" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-bordered grvContentarea" OnRowDataBound="gvFixAsset_RowDataBound" Width="461px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: center"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                        Width="400px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lgcUnit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="50px"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText=" Qty">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkqty" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent;" Font-Bold="true" Font-Underline="false" Target="_blank"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString() %>'
                                        Width="50px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />                     
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>



                             <asp:TemplateField HeaderText="Total Qty">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnktqty" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Bold="true" Font-Underline="false"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString() %>'
                                        Width="80px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Allocated Qty">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkqty22" runat="server"  BorderColor="#99CCFF" BorderStyle="none" 
                                        Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Bold="true" Font-Underline="false"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aqty")).ToString() %>'
                                        Width="60px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <itemstyle horizontalalign="center" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="Free Qty">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkqty" runat="server"  BorderColor="#99CCFF" BorderStyle="none" Target="_blank"
                                        Font-Size="11px" Style="background-color: Transparent; margin-right:30px " Font-Bold="true" Font-Underline="false"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "frqty")).ToString() %>'
                                        Width="45px">
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooterNew" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPaginationNew" />
                        <HeaderStyle CssClass="grvHeaderNew" />
                    </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

