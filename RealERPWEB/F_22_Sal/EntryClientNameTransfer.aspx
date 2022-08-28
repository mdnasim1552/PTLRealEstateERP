<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntryClientNameTransfer.aspx.cs" Inherits="RealERPWEB.F_22_Sal.EntryClientNameTransfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style>
        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }
    </style>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
        };
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

            <div class="card card-fluid mb-2">
                <div class="card-body">
                    
                        <div class="row">
                               <div class="col-md-2">
                                   <div class="form-group">
                                    <asp:Label ID="lblAddWrkDate" runat="server" class="control-label  lblmargin-top9px" Text="Date"></asp:Label>
                                    <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtCurTransDate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                                </div>

                               </div>

                        
                            <%--  <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblAddWorkNo" runat="server" class="control-label  lblmargin-top9px" Text="Trans. No."></asp:Label>
                                    <asp:Label ID="lblCurNo1" runat="server" class="control-label  lblmargin-top9px"></asp:Label>
                                    <asp:TextBox ID="lblCurNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="true">00000</asp:TextBox>
                                </div>
                            </div>--%>
                                                                             
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPreList" runat="server" class="control-label  lblmargin-top9px" Text="Project Name"></asp:Label>
                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblUnitName" runat="server" class="control-label  lblmargin-top9px" Text="Unit Name"></asp:Label>
                                    <asp:DropDownList ID="ddlUnitName" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-1">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm lblmargin-top20px" Style="margin-top: 20px;"></asp:LinkButton>
                            </div>
                             <asp:Label ID="lblcustomerid" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>
                             <asp:TextBox ID="Label1" runat="server" CssClass="form-control inputTxt" TextMode="Number"  ></asp:TextBox>
                          <%--  <asp:RequiredFieldValidator ID="rfvCat" runat="server" ControlToValidate="Label1"
                ErrorMessage="Field is blank">*</asp:RequiredFieldValidator> --%> 
                        </div>

                       <%-- <asp:Panel ID="PanelItem" runat="server">
                        <div class="row">
                             <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblPrjto" runat="server" class="control-label  lblmargin-top9px" Text="Project Name To"></asp:Label>
                                    <asp:DropDownList ID="ddlprjto" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblunitto" runat="server" class="control-label  lblmargin-top9px" Text="Unit Name To"></asp:Label>
                                    <asp:DropDownList ID="ddlUnitNameto" runat="server" CssClass="form-control chzn-select form-control-sm" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-1">
                                <asp:LinkButton ID="btnselect" runat="server" Text="Select" OnClick="btnselect_Click" CssClass="btn btn-primary btn-sm lblmargin-top20px"></asp:LinkButton>
                            </div>
                            
                            

                            

                        </div>
                    </asp:Panel>--%>
                </div>
            </div>

            <div class="card card-fluid">
                
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right; font-size: 12px;"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Width="49px" ForeColor="Black"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcResDesc1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                Width="220px" ForeColor="Black" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgval" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <FooterTemplate>

                                            <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-warning primaryBtn" OnClick="lUpdatPerInfo_Click">Update Personal Info</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" AutoCompleteType="Disabled"
                                                Width="130px" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                Height="20px" Font-Size="12px"></asp:TextBox>

                                            <asp:TextBox
                                                ID="txtgvdVal" runat="server" BackColor="Transparent" BorderStyle="None" AutoCompleteType="Disabled" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                Width="130px" Font-Size="12px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender_txtgvdVal" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>

                                            <asp:DropDownList ID="ddlFileNo" runat="server" CssClass="form-control inputTxt"></asp:DropDownList>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvValbn" runat="server" BackColor="Transparent"
                                                Width="130px" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc2")) %>'
                                                Height="20px" Font-Size="12px" ></asp:TextBox>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>


                                <%--<FooterStyle CssClass="grvFooter" />--%>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <FooterStyle BackColor="#23cc94" />
                                <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" Height="30px" />
                            </asp:GridView>
                    </div>

                    <asp:Panel ID="PnlNarration" runat="server" Visible="False">
                        <div class="col-sm-6 col-md-6 col-lg-6 mt-3" id="dNarr" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNarr" runat="server" CssClass="control-label  lblmargin-top9px" Font-Bold="true" Text="Narration:"></asp:Label>
                                <asp:TextBox ID="txtNarr" runat="server" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
