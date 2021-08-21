<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="LCCostingDetails.aspx.cs" Inherits="RealERPWEB.F_09_LCM.LCCostingDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="dchk1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
            $('#tblrpprodetails').Scrollable({
            });



        };

    </script>
    <style>
        .btnlgn {
            margin-bottom: 2px;
        }

        fieldset > legend:first-of-type {
            -webkit-margin-top-collapse: separate;
            margin-bottom: 20px;
        }
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                </div>


                <div class="row">

                    <fieldset class="scheduler-border fieldset_B">

                        <div class="form-group">
                            <div class="col-md-4 pading5px">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">L/C Number</asp:Label>

                                <div class="ddlListPart">
                                    <asp:DropDownList ID="ddlLcCode" runat="server" Width="315px" CssClass="inputTxt chzn-select" OnSelectedIndexChanged="ddlLcCode_SelectedIndexChanged" TabIndex="6" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3 pading5px">

                                <asp:Label ID="lblstorid" runat="server" CssClass="lblTxt lblName">Store Id:</asp:Label>
                                <div class="ddlListPart">
                                    <asp:DropDownList ID="ddlStorid" runat="server" AutoPostBack="True" Width="210px" CssClass=" inputTxt chzn-select" OnSelectedIndexChanged="ddlStorid_SelectedIndexChanged" TabIndex="16">
                                    </asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-md-3 pading5px">
                                <asp:Label ID="lblgrr" runat="server" CssClass="smLbl_to">GRN No.</asp:Label>

                                <div class="ddlListPart">
                                    <asp:DropDownList ID="ddlgenno" runat="server" Width="220" AutoPostBack="True" CssClass="inputTxt chzn-select" TabIndex="16"></asp:DropDownList>
                                </div>

                            </div>
                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkOk_Click">OK</asp:LinkButton>


                            <%--   <div class="col-md-1">
                                <asp:Label ID="Label3" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                            </div>--%>
                            <div class="col-md-1 pull-right">
                                <div class="msgHandSt">


                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="50">
                                        <ProgressTemplate>
                                            <asp:Label ID="Labelpro" runat="server" CssClass="lblProgressBar"
                                                Text="Please Wait.........."></asp:Label>

                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                </div>
                            </div>
                    </fieldset>

                    <div class="col-md-5">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <asp:Panel ID="pnlPro" runat="server" Visible="False" TabIndex="22">


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label3" runat="server" CssClass=" dataLblview" Text="Product Details"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                        <asp:GridView ID="dgvReceive" runat="server" AllowPaging="false"
                            AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />

                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                            Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                    ItemStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCode1" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Item Code"
                                    ItemStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvscode" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "scode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                    HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResdesc1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbTotalLcCost" runat="server" ForeColor="Black">Total</asp:Label>


                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Right"  Font-Bold="true" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Specification" HeaderStyle-Font-Size="12px"
                                    HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Right"  Font-Bold="true" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Received Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvordqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFordqty" runat="server" Font-Bold="True"
                                            Width="80px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>


                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="col-md-7">
                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <asp:Panel ID="pnlCosting" runat="server" Visible="False" TabIndex="22">


                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lbllcCost" runat="server" CssClass=" dataLblview" Text="Costing Details"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>

                        <asp:GridView ID="gvlccost" runat="server" AllowPaging="false"
                            AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />

                            <Columns>
                                <asp:TemplateField HeaderText="SL No." ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                            Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                    ItemStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCodelc" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                    HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <FooterTemplate>
                                        <asp:Label ID="lbTotalLcCost" runat="server" ForeColor="Black">Total</asp:Label>


                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResdesclc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total L/C Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgrvFtolcCost" runat="server" Font-Bold="True"
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtolcCost" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tolccost")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Previous Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgrvFprelcCost" runat="server" Font-Bold="True"
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprelcCost" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utorecamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Received Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="Red">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgrvFcurlcCost" runat="server" Font-Bold="True"
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcurlcCostt" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px" BorderStyle="None" Style="text-align: right; background-color: transparent;"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="Red" />
                                    <ItemStyle HorizontalAlign="Right"  ForeColor="Red"/>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Balance" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgrvFlcbalance" runat="server" Font-Bold="True"
                                            Width="70px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlcbalance" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

