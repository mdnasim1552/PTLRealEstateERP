<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="RptDailyTransCashBank.aspx.cs" Inherits="RealERPWEB.F_17_Acc.RptDailyTransCashBank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


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
            <div class="container moduleItemWrpper minheight">
                <div class="contentPart">
                    <div class="row">


                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-6 pading5px ">
                                        <asp:Label ID="Label22" runat="server" CssClass="lblTxt lblName"> From:</asp:Label>


                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat=""></cc1:CalendarExtender>
                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to"> To:</asp:Label>

                                        <asp:TextBox ID="txttodate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txttodate" TodaysDateFormat=""></cc1:CalendarExtender>

                                        <asp:RadioButtonList ID="rbtnGroup" runat="server"
                                                RepeatColumns="6" RepeatDirection="Horizontal"
                                                Width="235px">

                                                <asp:ListItem>Cash</asp:ListItem>
                                                <asp:ListItem>Bank</asp:ListItem>
                                                <asp:ListItem Selected="True">Both</asp:ListItem>

                                            </asp:RadioButtonList>
                                         

                                      


                                    </div>

                                      <div class="col-md-1  pading5px">

                                           <div class="colMdbtn pading5px">
                                             <asp:LinkButton ID="lbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnShow_Click">Ok</asp:LinkButton>

                                        </div
                                          

                                        </div>

                                </div>



                            </div>
                        </fieldset>


                        <asp:GridView ID="gvcashbook" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            OnRowDataBound="gvcashbook_RowDataBound" CssClass="table table-striped table-hover table-bordered grvContentarea" Width="571px" >
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                 <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkvounum" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                                Width="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Cheque/Ref #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                 <asp:TemplateField HeaderText="Cash/Bank Name">
                                    <HeaderTemplate>
                                        <table style="width: 23%;">
                                            <tr>
                                                <td class="style58">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Cash/Bank Name"
                                                        Width="108px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" CssClass="btn  btn-success btn-xs fa fa-file-excel-o" Style="text-align: center"  ToolTip="Export to Excel"> </asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Accounts Head">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              

                                
                                <asp:TemplateField HeaderText="Voucher #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cheque Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate" runat="server" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chequedat")).ToString("dd-MMM-yyyy")) %>'                                       
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               



                               


                                <asp:TemplateField HeaderText="Pay To">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Narration">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                            Width="170px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Paid Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvDpAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                            Width="65px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                <!-- End of contentpart-->
            </div>
            <!-- End of Container-->




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
