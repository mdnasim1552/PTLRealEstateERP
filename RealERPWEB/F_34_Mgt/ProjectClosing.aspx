<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ProjectClosing.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.ProjectClosing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });

            var gv = $('#<%=this.gvProLinkInfo.ClientID %>');
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

            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">

                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label class="control-label  lblmargin-top9px" for="imgProject" id="lblDaterange" runat="server">
                                            <asp:LinkButton ID="ImgbtnFindProject" runat="server" OnClick="ImgbtnFindProject_Click">Project Code</asp:LinkButton></label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" style="width:200px;" CssClass="chzn-select form-control inputTxt"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 ml-2">
                                    <div class="form-group">
                                        <label class="control-label  lblmargin-top9px" for="clsDate" id="lblClsDate" runat="server">Close Date</label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtCloseDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control flatpickr-input"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtCloseDate" Enabled="true"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnSelectPrj" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lbtnSelectPrj_Click">Select</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" ShowFooter="True" Width="16px">
                        <PagerSettings Visible="False" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText=" Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvBancCode" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtngvDelete" runat="server" Font-Bold="True" CssClass=" btn btn-xs" OnClick="lbtngvDelete_Click"><i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSuplDesc1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="350px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True" CssClass=" btn  btn-danger primarygrdBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                </FooterTemplate>
                                <FooterStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Close Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvCloseDate" runat="server" BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "clsdate")).ToString("dd-MMM-yyyy") %>' Width="150px"></asp:TextBox>
                                 <cc1:CalendarExtender ID="txtgvCloseDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtgvCloseDate" Enabled="true"></cc1:CalendarExtender>
                                </ItemTemplate>
                                 <%--<FooterTemplate>
                                    <asp:LinkButton ID="lbtngvSaveValue" runat="server" Visible="false" Font-Bold="True" CssClass=" btn  btn-primary primarygrdBtn" OnClick="lbtngvSaveValue_Click">Save Value</asp:LinkButton>
                                </FooterTemplate>--%>
                                <FooterStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


