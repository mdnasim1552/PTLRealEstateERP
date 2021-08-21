<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PriLandProposal.aspx.cs" Inherits="RealERPWEB.F_01_LPA.PriLandProposal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        }

    </script>
    <style>
        .txtBox{
           border-radius:0;
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProname" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-4 pading5px ">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" style="width:384px;" CssClass="chzn-select form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server"
                                            Visible="False" CssClass="form-control inputTxt" style="width:384px;"></asp:Label>

                                    </div>
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnSerOk_Click" TabIndex="4">Ok</asp:LinkButton>

                                  
                                    <div class="col-md-3 pull-right pading5px">
                                        <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>

                                </div>
                            </div>
                            </fieldset>
                        <div class="col-md-4">
                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Creation Date"></asp:Label>
                            <asp:TextBox ID="txtcdate" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="txtcdate"></cc1:CalendarExtender>
                        </div>
                        
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="772px"
                            OnRowDataBound="gvProjectInfo_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0"
                                            runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lgcResDesc1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True"
                                        HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lgp" runat="server"
                                            Font-Bold="True" Font-Size="12px" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                            Width="4px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Unit">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtResunit" runat="server" BackColor="Transparent"
                                            BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gunit")) %>'
                                            Width="50px" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value"  >
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lUpdatProInfo"
                                            runat="server" Font-Bold="True" CssClass="btn btn-danger primaryBtn"
                                            OnClick="lUpdatProInfo_Click">Update Information</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvVal" runat="server"  Height="20px"  BorderWidth="0" BorderStyle="none" CssClass="form-control inputTxt txtBox" Width="450px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>' ></asp:TextBox>

                                     <asp:DropDownList ID="ddlcataloc" runat="server" CssClass=" chzn-select form-control" Width="200px" AutoPostBack="true" TabIndex="2">
                                       </asp:DropDownList>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                              <%--  <asp:TemplateField Visible="false">

                                    <ItemTemplate>
                                        <asp:TextBox
                                            ID="txtgvVal2" runat="server" BackColor="Transparent"
                                            BorderStyle="None"  Height="20px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc2")) %>'
                                            Width="450px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">

                                    <ItemTemplate>
                                        <asp:TextBox
                                            ID="txtgvVal3" runat="server" BackColor="Transparent"
                                            BorderStyle="None"  Height="20px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc3")) %>'
                                            Width="200px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>
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


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

