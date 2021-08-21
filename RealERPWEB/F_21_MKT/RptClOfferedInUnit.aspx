<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptClOfferedInUnit.aspx.cs" Inherits="RealERPWEB.F_21_MKT.RptClOfferedInUnit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
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
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblProName" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcPro" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" OnClick="ibtnFindProject_OnClick" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="True" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"
                                            TabIndex="5">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn" TabIndex="9">Ok</asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblUnit" runat="server" CssClass="lblTxt lblName" Text="Unit Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcUnit" runat="server" TabIndex="3" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindUnit" runat="server" OnClick="ibtnFindUnit_OnClick" TabIndex="4" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:DropDownList ID="ddlUnitName" runat="server" CssClass="form-control inputTxt"
                                            TabIndex="5">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-1 pading5px">
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>

            </div>


            <asp:GridView ID="gvClientOff" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                CssClass="table table-striped table-hover table-bordered grvContentarea">
                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                    Mode="NumericFirstLast" />
                <%--<RowStyle BackColor="#D2FFF7" Font-Size="11px" />--%>
                <Columns>
                    <asp:TemplateField HeaderText="Sl">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                Style="text-align: right"
                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Client Name">
                        <ItemTemplate>
                            <asp:Label ID="lgClName" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) %>'
                                Width="150px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone No.">
                        <ItemTemplate>
                            <asp:Label ID="lgvPhone" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Offered Date">
                        <ItemTemplate>
                            <asp:Label ID="nappdat" runat="server"
                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Offered Price">
                        <ItemTemplate>
                            <asp:Label ID="lgvoffprice" runat="server"
                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0);") %>'
                                Width="80px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Discusstion">
                        <ItemTemplate>
                            <asp:Label ID="lgvoffprice" runat="server"
                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discus")) %>'
                                Width="280px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:TemplateField>




                </Columns>
                <%--<FooterStyle BackColor="#333333" />
                            <PagerStyle Font-Bold="True" Font-Size="12px" ForeColor="White"
                                HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />--%>

                <FooterStyle BackColor="#F5F5F5" />
                <EditRowStyle />
                <AlternatingRowStyle />
                <PagerStyle CssClass="gvPagination" />
                <HeaderStyle CssClass="grvHeader" />
            </asp:GridView>


            <asp:Label ID="lblUNitRate" runat="server" Visible="False"></asp:Label>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


