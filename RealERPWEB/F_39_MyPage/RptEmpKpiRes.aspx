
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpKpiRes.aspx.cs" Inherits="RealERPWEB.F_39_MyPage.RptEmpKpiRes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../js/bootstrap.js"></script>
    <script src="../Scripts/ScrollableGridPlugin.js"></script>
    <script src="../Scripts/KeyPress.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_B">

                    <div class="form-horizontal">


                        <div class="form-group">
                            <div class="col-md-2 pading5px  asitCol1">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblNameSm" Text="Month:"></asp:Label>

                                <asp:DropDownList ID="ddlyearmon" runat="server" AutoPostBack="True"
                                    TabIndex="11" CssClass="ddlPage125 inputTxt">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 pading5px asitCol3">
                                <asp:Label ID="lblSalesTeam" runat="server" CssClass="lblTxt lblName" Text="Employee Name:"></asp:Label>



                                <asp:TextBox ID="txtSrchSalesTeam" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                <div class="colMdbtn">
                                    <asp:LinkButton ID="imgSearchSalesTeam" runat="server" OnClick="imgSearchSalesTeam_Click" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                </div>
                            </div>



                            <div class="col-md-3 pading5px asitCol3">
                                <asp:DropDownList ID="ddlEmpid" runat="server" AutoPostBack="True" CssClass="ddlPage235 inputTxt"
                                    TabIndex="5">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 pading5px">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn" TabIndex="9"></asp:LinkButton>


                            </div>

                            <div class="col-md-2 pading5px asitCol3">
                                <div class="msgHandSt">
                                    <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn disabled" Visible="false"></asp:Label>
                                </div>

                            </div>



                        </div>

                    </div>
                </fieldset>

                <div class="table-responsive">
                    <asp:GridView ID="gvEmpKpiResult" runat="server" AllowPaging="True" Width="200px"
                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                        CssClass="table table-striped table-hover table-bordered grvContentarea">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                            Mode="NumericFirstLast" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                </ItemTemplate>
                                <ControlStyle Width="10px" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Activity">

                                <ItemTemplate>
                                    <asp:Label ID="lblAct" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"kpigrpdesc")) %>'
                                        Width="100px"
                                        Style="text-align: left" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFTotal" runat="server" Style="text-align: right; color: black;" Width="55px">Total: </asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="KPI Index">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvKpiVal" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"stdkpival")).ToString("#,##0;(#,##0); ") %>'
                                        Width="55px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFKpival" runat="server" Style="text-align: right; color: black;" Width="55px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvTarget" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"stdtarget")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFTarget" runat="server" Style="text-align: right; color: black;" Width="55px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Actual">
                                <ItemTemplate>
                                    <asp:Label ID="lblActual" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"actual")).ToString("#,##0;(#,##0); ") %>'
                                        Width="90px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Actual Index">
                                <ItemTemplate>
                                    <asp:Label ID="lblMparcnt" runat="server"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,"mparcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="55px"
                                        Style="text-align: right" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblFMparcnt" runat="server" Style="text-align: right; color: black;" Width="55px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>




                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


