<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PayrollLink.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.PayrollLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        div#ContentPlaceHolder1_ddlUserList_chzn {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".select2").select2();

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


            $('.select2').each(function () {
                var select = $(this);
                select.select2({
                    placeholder: 'Select an option',
                    width: '100%',
                    allowClear: !select.prop('required'),
                    language: {
                        noResults: function () {
                            return "{{ __('No results found') }}";
                        }
                    }
                });
            });
            $('.chzn-select').chosen({ search_contains: true });
        }


        function Search_Gridview(strKey) {

            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvPayrollLinkInfo.ClientID %>");
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {

                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
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

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">

                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">User</asp:Label>
                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass="form-control chzn-select" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-3 col-sm-6">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-lg-3 col-md-3 col-sm-6">
                                <div class="form-group">
                                    <asp:Label ID="lblConTrolCode" runat="server">Company Code  
                                        <asp:LinkButton ID="ImgbtnFindComp" runat="server" OnClick="ImgbtnFindComp_Click"><i class="fas fa-search "></i></asp:LinkButton>

                                    </asp:Label>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server">Branch  
                                        <asp:LinkButton ID="lnkBranch" runat="server" OnClick="lnkBranch_Click"><i class="fas fa-search "></i></asp:LinkButton>

                                    </asp:Label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server">Department  
                                        <asp:LinkButton ID="lnkdpt" runat="server" OnClick="lnkdpt_Click"><i class="fas fa-search "></i></asp:LinkButton>

                                    </asp:Label>
                                    <asp:DropDownList ID="ddlDptList" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDptList_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server">Section  
                                        <asp:LinkButton ID="lnkSection" runat="server" OnClick="lnkSection_Click"><i class="fas fa-search "></i></asp:LinkButton>

                                    </asp:Label>
                                    <asp:DropDownList ID="ddlSectionList" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group text-right">
                                    <asp:LinkButton ID="lnkAddList" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkAddList_Click">Add</asp:LinkButton>

                                </div>

                            </div>
                            <div class="col-lg-9 col-md-9 col-sm-12">
                                <div class="row">
                                    <div class="col-3">
                                        <div class="input-group input-group-alt">
                                            <div class="input-group-prepend ">
                                                <asp:Label ID="Label5" runat="server" CssClass="btn btn-secondary btn-sm">Search</asp:Label>
                                            </div>
                                            <asp:TextBox ID="inputtextbox" Style="height: 29px" runat="server" CssClass="form-control" placeholder="Search here..." onkeyup="Search_Gridview(this)"></asp:TextBox>

                                        </div>
                                    </div>

                                    <div class="col-12">

                                        <asp:GridView ID="gvPayrollLinkInfo" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" ShowFooter="True"
                                            OnRowDeleting="gvPayrollLinkInfo_RowDeleting">
                                            <PagerSettings Visible="False" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField ShowDeleteButton="True" />

                                                <asp:TemplateField HeaderText="Company Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCompCod" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCompDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                            Width="350px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                            Font-Size="13px" Height="16px" OnClick="lbtnDeleteAll_Click"
                                                            Width="100px">Delete All</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn btn-success btn-sm">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                                            Width="150px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="User Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCompusrid" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                                            Width="80px"></asp:Label>
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
                                </div>
                            </div>

                        </div>


                    </asp:Panel>

                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

