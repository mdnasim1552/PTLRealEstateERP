<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FxtAssetRegister.aspx.cs" Inherits="RealERPWEB.F_29_Fxt.FxtAssetRegister" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);



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

                                <div class="form-group" >
                                    <div class="col-md-4 pading5px" >
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Asset Type</asp:Label>
                                       <asp:DropDownList ID="ddlProjectName" runat="server" Width="270" CssClass="chzn-select ddlPage" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblDeptDesc" runat="server" CssClass="dataLblview" Visible="False" Width="250px"></asp:Label>

                                    </div>
                                   <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" TabIndex="4" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                    </div>
                                        <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName" Text="Resource "></asp:Label>
                                        <asp:TextBox ID="txtSrchMat" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                   

                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <asp:GridView ID="gvFixAsset" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" AllowPaging="false" CssClass=" table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvFixAsset_RowDataBound" Width="461px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

