<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="AccLandPaySlip.aspx.cs" Inherits="RealERPWEB.F_51_LBgd.AccLandPaySlip" %>

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
        }

    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName"  Text="Accounts Head:"></asp:Label>
                                        <asp:TextBox ID="txtAccSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="IbtnSearchAcc" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="IbtnSearchAcc_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlConAccHead_SelectedIndexChanged" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>

                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkShowLedger" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkShowLedger_Click" >Show</asp:LinkButton>

                                    </div>
                                    <div class="col-md-3 pading5px asitCol3">
                                            <div class="msgHandSt">


                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                    DisplayAfter="30">
                                                    <ProgressTemplate>
                                                        <asp:Label ID="Label2" runat="server" CssClass="lblProgressBar"
                                                            Text="Please Wait.........."></asp:Label>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                            </div>
                                        </div>

                                </div>
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDetailsHead" runat="server" CssClass="lblTxt lblName"  Text="Details Head::"></asp:Label>
                                        <asp:TextBox ID="txtSearchRes" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="IbtnResource" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="IbtnResource_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlResource" runat="server" CssClass="form-control inputTxt" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>



                                </div>
                                <div class="form-group">
                                    <div class="col-md-4 pading5px asitCol4">


                                    </div>

                                </div>
                            </div>
                        </fieldset>


                         <asp:Repeater ID="rppayslip" runat="server" >

                            <HeaderTemplate>
                                <table  id="tblrppayslip"   class=" table-striped table-hover table-bordered grvContentarea">
                                    <tr>
                                        <th rowspan="2">SL</th>
                                        <th rowspan="2">Name of Donor</th>
                                        <th colspan="2">Details of Plot</th>
                                       
                                        <th rowspan="2">Purchaseable Area of Land</th>
                                        <th rowspan="2">Nature of Deed</th>
                                        <th rowspan="2">Rate</th>
                                        <th colspan="6">Details of Expenditure</th>
                                        
                                    </tr>

                                     <tr>
                                        
                                        <th>Plot No</th>
                                        <th>Total Land </th>
                                       
                                        <th>Total Price</th>
                                        <th>Payment</th>
                                        <th>Registration Cost</th>
                                        <th>Other Expenditure</th>
                                        <th>Total Cost</th>
                                        <th>Net  Payable</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lrpnameofdonor" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lownname")) %>'
                                            Width="150px"></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lrpplotno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "plotno")) %>'
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lrptolarea" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tolarea")) %>'
                                            Width="70px"></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lrplarea" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "larea")) %>'
                                            Width="80px"></asp:Label>
                                    </td>

                                     <td>
                                        <asp:Label ID="lrpnofdeed" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nofdeed")) %>'
                                            Width="100px"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lblgvrate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px">
                                        </asp:Label>


                                    </td>
                                     <td>
                                        <asp:Label ID="lrpcost" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px">
                                        </asp:Label>


                                    </td>
                                     <td>
                                        <asp:Label ID="lrppayam" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px">
                                        </asp:Label>


                                    </td>
                                     <td>
                                        <asp:Label ID="lrpregam" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rgamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px">
                                        </asp:Label>


                                    </td>
                                     <td>
                                        <asp:Label ID="lrpotham" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px">
                                        </asp:Label>


                                    </td>
                                     <td>
                                        <asp:Label ID="lrptocost" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tocostam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px">
                                        </asp:Label>


                                    </td>
                                     <td>
                                        <asp:Label ID="lrpnepayable" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right; font-size: 11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpayable")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px">
                                        </asp:Label>


                                    </td>
                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>


                                </table>
                            </FooterTemplate>





                        </asp:Repeater>
                    </div>


                   



           

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

